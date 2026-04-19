using System;
using Sirenix.OdinInspector;

namespace Events.UI
{
    // Main Menu Events
    public struct OnPlayButtonSelected : IMainMenuEvent { }

    [Serializable]
    public struct OnLocationSelected : IMainMenuEvent
    {
        [InlineEditor] public SceneGroupSO locationSceneGroupAsset;
    }
    
    
    public struct SwitchSettingTabEvent
    {
        public SettingTabType typeToSwitchTo;
    }
}