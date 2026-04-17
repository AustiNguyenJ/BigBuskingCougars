using UnityEditor;
using UnityEngine;

namespace Systems.SceneManagement.Editor {
    [CustomEditor(typeof(SceneLoader))]
    public class SceneLoaderEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            SceneLoader sceneLoader = (SceneLoader) target;

            if (EditorApplication.isPlaying && GUILayout.Button("Load First Scene Group")) {
                LoadSceneGroup(sceneLoader);
            }
            
            if (EditorApplication.isPlaying && GUILayout.Button("Load Second Scene Group")) {
                LoadSceneGroup(sceneLoader);
            }
        }

       static async void LoadSceneGroup(SceneLoader sceneLoader) {
            await sceneLoader.LoadSceneGroup(null);
       }
    }
    
    
}