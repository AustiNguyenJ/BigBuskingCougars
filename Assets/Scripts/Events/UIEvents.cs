using System;
using Sirenix.OdinInspector;

namespace Events.UI
{
    #region MainMenuEvents
    public struct OnPlayButtonSelected : IUiEvent { }

    [Serializable]
    public struct OnLocationSelected : IUiEvent
    {
        [InlineEditor] public SceneGroupSO locationSceneGroupAsset;
    }
    public struct SwitchSettingTabEvent : IUiEvent
    {
        public SettingTabType typeToSwitchTo;
    }
    #endregion

    #region UniversalUIEvents
    public struct OnBackButtonSelected : IUiEvent { }
    #endregion
    
    
    #region WristMenuEvents
    public struct WristMenuViewLocationsEvent : IUiEvent { }
    #endregion
}
