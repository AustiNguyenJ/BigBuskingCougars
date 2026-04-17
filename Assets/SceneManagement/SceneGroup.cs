using System;
using System.Collections.Generic;
using System.Linq;
using Eflatun.SceneReference;

namespace Systems.SceneManagement {
    [Serializable]
    public class SceneGroup {
        public string GroupName => nameof(SceneGroup);
        public List<SceneData> Scenes;
        public bool IsActive = true;
        
        public string FindSceneNameByType(SceneType sceneType) {
            return Scenes.FirstOrDefault(scene => scene.SceneType == sceneType)?.Reference.Name;
        }

        public bool SceneGroupContainsScene(string sceneName)
        {
            foreach (SceneData sceneData in Scenes)
            {
                if (sceneData.Name == sceneName) return true;
            }

            return false;
        }

        public bool SceneGroupContainsType(SceneType sceneType)
        {
            foreach (SceneData sceneData in Scenes)
            {
                if (sceneData.SceneType == sceneType) return true;
            }
            
            return false;
        }
    }
    
    [Serializable]
    public class SceneData {
        public SceneReference Reference;
        public string Name => Reference.Name;
        public SceneType SceneType;
    }
    
    public enum SceneType { ActiveScene, MainMenu, UserInterface, HUD, Cinematic, Stage, Tooling }
}