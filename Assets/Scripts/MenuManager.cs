using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] SceneGroupSO sceneGroupToLoad;
    
    public void StartGame()
    {
        GlobalEventAsset.Instance.TriggerEvent(new RequestSceneLoadEvent { sceneGroupToLoad = sceneGroupToLoad });
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}