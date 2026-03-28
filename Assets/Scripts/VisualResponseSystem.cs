using UnityEngine;
using System;

public class VisualResponseSystem : MonoBehaviour
{
    public static event Action<float, Vector3, DrumStickXR.StickHand, Color> OneDrumHit;
    public static event Action<int> OnComboUpdated;

    public static void TriggerComboUpdate(int combo)
    {
        OnComboUpdated?.Invoke(combo);
    }

    public static void TriggerDrumHit(float intensity, Vector3 position, DrumStickXR.StickHand hand, Color color)
    {
        OneDrumHit?.Invoke(intensity, position, hand, color);
    }

    [Header("Left stick effect")]
    public ParticleSystem hitEffect_L;

    [Header("Right stick effect")]
    public ParticleSystem hitEffect_R;


    private void OnEnable()
    {
        OneDrumHit += HandleDrumHit;
    }

    private void OnDisable()
    {
        OneDrumHit -= HandleDrumHit;
    }

    private void HandleDrumHit(float intensity, Vector3 position, DrumStickXR.StickHand hand, Color color)
    {
        ParticleSystem chosenEffect = null;

        if (hand == DrumStickXR.StickHand.Left)
            chosenEffect = hitEffect_L;
        else if (hand == DrumStickXR.StickHand.Right)
            chosenEffect = hitEffect_R;

        if (chosenEffect != null)
        {
            var main = chosenEffect.main;

            main.startSize = Mathf.Lerp(0.05f, 1.5f, intensity);

            if (color != null)
                main.startColor = color;

            chosenEffect.transform.position = position;
            chosenEffect.Stop();
            chosenEffect.Play();
        }
    }

}