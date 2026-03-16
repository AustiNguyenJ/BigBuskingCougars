using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    [Header("Menus")]
    public GameObject OptionsMenu;
    public GameObject SettingsPanel;

    [Header("Background Fade")]
    public Image DimBackground;
    public float fadeSpeed = 12f;
    public float pausedAlpha = 0.6f;

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
    private Coroutine fadeCoroutine;

    void Start()
    {
        OptionsMenu.SetActive(false);
        SettingsPanel.SetActive(false);

        LockCursor();
        SetupBackground();
        LoadSettings();
        SetupSliderListeners();
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

        UnlockCursor();
        ResumeButton.Select();

        DisablePlayerControls();
        ShowBackground();
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        LockCursor();
        EnablePlayerControls();

        OptionsMenu.SetActive(false);
        SettingsPanel.SetActive(false);

        FadeOutBackground();
    }

    // ==============================
    // Menu Navigation
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
    // Slider Settings
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

    void SetupSliderListeners()
    {
        VolumeSlider.onValueChanged.AddListener(SetVolume);
        VFXSlider.onValueChanged.AddListener(SetVFX);
    }

    // ==============================
    // VR / Quit
    // ==============================

    public void RecenterVR()
    {
        InputTracking.Recenter();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // ==============================
    // Player Controls
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

    // ==============================
    // Cursor Helpers
    // ==============================

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // ==============================
    // Background Helpers
    // ==============================

    void SetupBackground()
    {
        if (DimBackground != null)
        {
            Color color = DimBackground.color;
            color.a = 0f;
            DimBackground.color = color;
            DimBackground.gameObject.SetActive(false);
        }
    }

    void ShowBackground()
    {
        if (DimBackground == null)
            return;

        StopFadeCoroutine();

        DimBackground.gameObject.SetActive(true);

        Color color = DimBackground.color;
        color.a = pausedAlpha;
        DimBackground.color = color;
    }

    void FadeOutBackground()
    {
        if (DimBackground == null)
            return;

        StopFadeCoroutine();
        fadeCoroutine = StartCoroutine(FadeOutBackgroundCoroutine());
    }

    void StopFadeCoroutine()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }
    }

    IEnumerator FadeOutBackgroundCoroutine()
    {
        Color color = DimBackground.color;

        while (Mathf.Abs(color.a - 0f) > 0.01f)
        {
            color.a = Mathf.Lerp(color.a, 0f, Time.unscaledDeltaTime * fadeSpeed);
            DimBackground.color = color;
            yield return null;
        }

        color.a = 0f;
        DimBackground.color = color;
        DimBackground.gameObject.SetActive(false);
        fadeCoroutine = null;
    }
}