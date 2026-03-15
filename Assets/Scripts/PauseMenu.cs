using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class PauseMenu : MonoBehaviour
{
    [Header("Menus")]
    public GameObject OptionsMenu;
    public GameObject SettingsPanel;

    [Header("Buttons")]
    public Button ResumeButton;
    public Button SettingsButton;
    public Button QuitButton;
    public Button BackButton;
    public Button RecenterButton;

    [Header("Sliders")]
    public Slider VolumeSlider;
    public Slider VFXSlider;

    [Header("Player Control Scripts")]
    public MonoBehaviour[] scriptsToDisableWhilePaused;

    private bool isPaused = false;

    void Start()
    {
        OptionsMenu.SetActive(false);
        SettingsPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        LoadSettings();

        VolumeSlider.onValueChanged.AddListener(SetVolume);
        VFXSlider.onValueChanged.AddListener(SetVFX);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // ==============================
    // Pause / Resume
    // ==============================

    public void PauseGame()
    {
        isPaused = true;

        Time.timeScale = 0f;

        OptionsMenu.SetActive(true);
        SettingsPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        ResumeButton.Select();

        DisablePlayerControls();
    }

    public void ResumeGame()
    {
        isPaused = false;

        Time.timeScale = 1f;

        OptionsMenu.SetActive(false);
        SettingsPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        EnablePlayerControls();
    }

    // ==============================
    // Navigation
    // ==============================

    public void OpenSettings()
    {
        OptionsMenu.SetActive(false);
        SettingsPanel.SetActive(true);

        VolumeSlider.Select();
    }

    public void BackToPauseMenu()
    {
        SettingsPanel.SetActive(false);
        OptionsMenu.SetActive(true);

        ResumeButton.Select();
    }

    // ==============================
    // Sliders
    // ==============================

    public void SetVolume(float value)
    {
        PlayerPrefs.SetFloat("volume", value);
        PlayerPrefs.Save();
    }

    public void SetVFX(float value)
    {
        PlayerPrefs.SetFloat("vfx", value);
        PlayerPrefs.Save();
    }

    void LoadSettings()
    {
        float volume = PlayerPrefs.GetFloat("volume", 1f);
        float vfx = PlayerPrefs.GetFloat("vfx", 1f);

        VolumeSlider.value = volume;
        VFXSlider.value = vfx;
    }

    // ==============================
    // Recenter
    // ==============================

    public void RecenterVR()
    {
        InputTracking.Recenter();
    }

    // ==============================
    // Quit Game
    // ==============================

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // ==============================
    // Disable / Enable Player Movement
    // ==============================

    void DisablePlayerControls()
    {
        foreach (MonoBehaviour script in scriptsToDisableWhilePaused)
        {
            if (script != null)
                script.enabled = false;
        }
    }

    void EnablePlayerControls()
    {
        foreach (MonoBehaviour script in scriptsToDisableWhilePaused)
        {
            if (script != null)
                script.enabled = true;
        }
    }
}