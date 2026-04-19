using System;
using System.Collections.Generic;
using Events.UI;
using UnityEngine;

public enum SettingTabType
{
    Audio,
    Graphics
}

[Serializable]
public class SettingTab
{
    public SettingTabType type;
    [SerializeField] GameObject scrollViewObject;
    
    public void EnableTab() => scrollViewObject.SetActive(true);
    public void DisableTab() => scrollViewObject.SetActive(false);
}

public class SettingsTabManager : MonoBehaviour
{
    [SerializeReference] List<SettingTab> tabs = new List<SettingTab>();
    SettingTab currentTab;

    public void Initialize()
    {
        if (tabs.Count > 0)
        {
            SwitchTab(tabs[0].type);
        }
    }

    void OnEnable()
    {
        GlobalEventAsset.Instance.StartListening<SwitchSettingTabEvent>(OnSwitchTabRequested);
    }

    void OnDisable()
    {
        GlobalEventAsset.Instance.StopListening<SwitchSettingTabEvent>(OnSwitchTabRequested);
    }

    void OnSwitchTabRequested(SwitchSettingTabEvent data) => SwitchTab(data.typeToSwitchTo);

    public void SwitchTab(SettingTabType type)
    {
        if (type == currentTab.type) return;
        
        foreach (var tab in tabs)
        {
            if (tab.type == type)
            {
                if (currentTab != null)
                    currentTab.DisableTab();
                currentTab = tab;
                currentTab.EnableTab();
                return;
            }
        }
    }
}
