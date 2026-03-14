using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider vfxSlider;

    private bool isPaused = false;

    void Start()
    {
        if (optionsMenu != null)
            optionsMenu.SetActive(false);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;

        if (volumeSlider != null)
        {
            volumeSlider.minValue = 0f;
            volumeSlider.maxValue = 1f;

            float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
            volumeSlider.value = savedVolume;
            AudioListener.volume = savedVolume;

            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        if (vfxSlider != null)
        {
            vfxSlider.minValue = 0f;
            vfxSlider.maxValue = 1f;
            vfxSlider.value = 1f;

            vfxSlider.onValueChanged.AddListener(SetVFXIntensity);
        }
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void PauseGame()
    {
        isPaused = true;

        optionsMenu.SetActive(true);
        settingsPanel.SetActive(false);

        resumeButton.SetActive(true);
        settingsButton.SetActive(true);
        quitButton.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resumeButton);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;

        optionsMenu.SetActive(false);
        settingsPanel.SetActive(false);

        resumeButton.SetActive(true);
        settingsButton.SetActive(true);
        quitButton.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;
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

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resumeButton);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
        Debug.Log("Volume: " + volume);
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