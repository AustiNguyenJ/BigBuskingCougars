using System;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct RequestSceneLoadEvent
{
    public SceneGroupSO sceneGroupToLoad;
}

public struct OnSceneGroupLoadedEvent
{
    public SceneGroupSO loadedGroup;
}

namespace Systems.SceneManagement {
    public class SceneLoader : MonoBehaviour { 
       // [SerializeField] Image loadingBar;
        [SerializeField] float fillSpeed = 0.5f;
       // [SerializeField] Canvas loadingCanvas;
       // [SerializeField] Camera loadingCamera;
       [SerializeField, Required] GameObject testVariable;

        [Header("Developer Settings")] 
        public SceneGroupSO startingSceneAsset;
        public bool LoadGroupOnStart = false;
        
        float targetProgress;
        bool isLoading;

        public readonly SceneGroupManager manager = new SceneGroupManager();

        async void Start()
        {
            if (LoadGroupOnStart)
                Load(startingSceneAsset);
        }

        void OnEnable()
        {
           GlobalEventAsset.Instance.StartListening<RequestSceneLoadEvent>(OnLoadSceneRequested);
        }

        void OnDisable()
        {
           GlobalEventAsset.Instance.StopListening<RequestSceneLoadEvent>(OnLoadSceneRequested);
        }
        
        void OnLoadSceneRequested(RequestSceneLoadEvent data) => Load(data.sceneGroupToLoad);

        void Update() {
            if (!isLoading) return;
            
           // float currentFillAmount = loadingBar.fillAmount;
           // float progressDifference = Mathf.Abs(currentFillAmount - targetProgress);

          //  float dynamicFillSpeed = progressDifference * fillSpeed;
    
           // loadingBar.fillAmount = Mathf.Lerp(currentFillAmount, targetProgress, Time.deltaTime * dynamicFillSpeed);
        }

        async void Load(SceneGroupSO sceneGroupToLoad)
        {
            if (isLoading)
            {
                Debug.LogWarning($"Load request for {sceneGroupToLoad.name} canceled. A scene group is already loading.");
                return;
            }

            try
            {
                await LoadSceneGroup(sceneGroupToLoad);
                GlobalEventAsset.Instance.TriggerEvent(new OnSceneGroupLoadedEvent { loadedGroup = sceneGroupToLoad });
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                EnableLoadingCanvas(false); 
            }
        }

       public async Task LoadSceneGroup(SceneGroupSO sceneGroupSO) {
            if (isLoading) return;
           
          //  loadingBar.fillAmount = 0f;
            targetProgress = 1f;

            SceneGroup sceneGroupToLoad = sceneGroupSO.sceneGroup;

            LoadingProgress progress = new LoadingProgress();
            progress.Progressed += target => targetProgress = Mathf.Max(target, targetProgress);
            
            EnableLoadingCanvas();
            
            await manager.LoadScenes(sceneGroupToLoad, progress);
            EnableLoadingCanvas(false);
        }
    
        void EnableLoadingCanvas(bool enable = true) {
            isLoading = enable;
          //  loadingCanvas.gameObject.SetActive(enable);
            //loadingCamera.gameObject.SetActive(enable);
        }
        
        public async Task LoadDependenciesAdditive(SceneGroupSO sceneGroupSO) {
            if (isLoading) return;

           // loadingBar.fillAmount = 0f;
            targetProgress = 1f;

            SceneGroup sceneGroupToLoad = sceneGroupSO.sceneGroup;

            LoadingProgress progress = new LoadingProgress();
            progress.Progressed += target => targetProgress = Mathf.Max(target, targetProgress);
            
            EnableLoadingCanvas();
            await manager.LoadDependencies(sceneGroupToLoad, progress);
            EnableLoadingCanvas(false);
        }
    }
    
    public class LoadingProgress : IProgress<float> {
        public event Action<float> Progressed;

        const float ratio = 1f;

        public void Report(float value) {
            Progressed?.Invoke(value / ratio);
        }
    }
}