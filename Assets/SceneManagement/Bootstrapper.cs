using Systems.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public class Bootstrapper : PersistentSingleton<Bootstrapper> {
    static readonly int sceneIndex = 0;
    const string PreviousSceneKey = "PreviousScenePath";

#if UNITY_EDITOR
    [InitializeOnLoadMethod]
    static void EditorInit() {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    static void OnPlayModeStateChanged(PlayModeStateChange state) {
        if (state == PlayModeStateChange.ExitingEditMode) {
            EditorPrefs.SetString(PreviousSceneKey, EditorSceneManager.GetActiveScene().path);
        }
    }
#endif

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init() {
#if UNITY_EDITOR
        EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[sceneIndex].path);
#endif
    }

    void Start() {
#if UNITY_EDITOR
        string sceneToLoad = EditorPrefs.GetString(PreviousSceneKey, string.Empty);
        string bootstrapperPath = SceneUtility.GetScenePathByBuildIndex(sceneIndex);

        if (!string.IsNullOrEmpty(sceneToLoad) && sceneToLoad != bootstrapperPath) {
            SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
        }
#endif
    }
}