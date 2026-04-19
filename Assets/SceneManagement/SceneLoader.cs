using System;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

// event used for requesting scene transitions
public struct RequestSceneLoadEvent
{
    public SceneGroupSO sceneGroupToLoad;
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
            if (Validate.AnyNull(startingSceneAsset) || !LoadGroupOnStart) return;
            try
            {
                await LoadSceneGroup(startingSceneAsset);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            //GlobalEventAsset.Instance.TriggerEvent(new SceneGroupLoadedEvent {  loadedSceneGroup = startingSceneAsset });    
        }


        void OnEnable()
        {
           GlobalEventAsset.Instance.StartListening<RequestSceneLoadEvent>(Load);
        }

        void OnDisable()
        {
           GlobalEventAsset.Instance.StopListening<RequestSceneLoadEvent>(Load);
        }

        void Update() {
            if (!isLoading) return;
            
           // float currentFillAmount = loadingBar.fillAmount;
           // float progressDifference = Mathf.Abs(currentFillAmount - targetProgress);

          //  float dynamicFillSpeed = progressDifference * fillSpeed;
    
           // loadingBar.fillAmount = Mathf.Lerp(currentFillAmount, targetProgress, Time.deltaTime * dynamicFillSpeed);
        }

        async void Load(RequestSceneLoadEvent requestSceneLoadEvent)
        {
            try
            {
                await LoadSceneGroup(requestSceneLoadEvent.sceneGroupToLoad);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

       public async Task LoadSceneGroup(SceneGroupSO sceneGroupSO) {
           
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