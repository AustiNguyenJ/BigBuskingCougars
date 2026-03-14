using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider vfxSlider;

    void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
            volumeSlider.minValue = 0f;
            volumeSlider.maxValue = 1f;
            volumeSlider.value = AudioListener.volume;
        }

        if (vfxSlider != null)
        {
            vfxSlider.onValueChanged.AddListener(SetVFXIntensity);
            vfxSlider.minValue = 0f;
            vfxSlider.maxValue = 1f;
            vfxSlider.value = 1f;
        }
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            optionsMenu.SetActive(!optionsMenu.activeSelf);

            if (!optionsMenu.activeSelf)
            {
                settingsPanel.SetActive(false);
                resumeButton.SetActive(true);
                settingsButton.SetActive(true);
                quitButton.SetActive(true);
            }
        }
    }

    public void ResumeGame()
    {
        optionsMenu.SetActive(false);
        settingsPanel.SetActive(false);
        resumeButton.SetActive(true);
        settingsButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void OpenSettings()
    {
        resumeButton.SetActive(false);
        settingsButton.SetActive(false);
        quitButton.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        settingsPanel.SetActive(false);
        resumeButton.SetActive(true);
        settingsButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetVFXIntensity(float intensity)
    {
        Debug.Log("VFX Intensity: " + intensity);
    }

    public void RecenterView()
    {
        Debug.Log("Recenter View pressed");
        UnityEngine.XR.InputTracking.Recenter();
    }
}