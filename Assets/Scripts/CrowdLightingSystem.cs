using UnityEngine;
using System;

public class CrowdLightingSystem : MonoBehaviour
{
    [Header("Crowd Lights")]
    public Light[] crowdLights;

    [Header("Intensity Settings")]
    public float baseLightIntensity = 1f;
    public float maxLightIntensity = 3f;

    // color tiers based on combo
    private Color tierZero  = Color.white;                      // combo 0-9
    private Color tierOne   = Color.red;                        // combo 10-24
    private Color tierTwo   = new Color(1f, 0.5f, 0f);         // combo 25-49 orange
    private Color tierThree = new Color(0.5f, 0f, 1f);         // combo 50+ purple

    void OnEnable()
    {
        VisualResponseSystem.OnComboUpdated += HandleComboUpdate;
    }

    void OnDisable()
    {
        VisualResponseSystem.OnComboUpdated -= HandleComboUpdate;
    }

    private void HandleComboUpdate(int combo)
    {
        Debug.Log($"Combo updated: {combo}");

        Color targetColor = GetColorForCombo(combo);
        float targetIntensity = GetIntensityForCombo(combo);

        foreach (Light light in crowdLights)
        {
            if (light != null)
            {
                light.color = targetColor;
                light.intensity = targetIntensity;
            }
        }
    }

    private Color GetColorForCombo(int combo)
    {
        if (combo >= 50) return tierThree;
        if (combo >= 25) return tierTwo;
        if (combo >= 10) return tierOne;
        return tierZero;
    }

    private float GetIntensityForCombo(int combo)
    {
        float t = Mathf.Clamp01(combo / 100f);
        return Mathf.Lerp(baseLightIntensity, maxLightIntensity, t);
    }
}