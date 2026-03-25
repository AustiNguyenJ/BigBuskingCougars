using UnityEngine;
using System;

public class VisualResponseSystem : MonoBehaviour
{
    public static event Action<float, Vector3, DrumStickXR.StickHand> OneDrumHit;
    public static event Action<int> OnComboUpdated;

    public static void TriggerComboUpdate(int combo)
    {
        OnComboUpdated?.Invoke(combo);
    }

    public static void TriggerDrumHit(float intensity, Vector3 position, DrumStickXR.StickHand hand)
    {
        OneDrumHit?.Invoke(intensity, position, hand);
    }
    [Header("Left stick effect")]
    public ParticleSystem hitEffect_L;

    [Header("Right stick effect")]
    public ParticleSystem hitEffect_R;

    [Header("Colors Gradient")]
    public Gradient colorGradient;


    private void OnEnable()
    {
        OneDrumHit += HandleDrumHit;
    }

    private void OnDisable()
    {
        OneDrumHit -= HandleDrumHit;
    }

    private void HandleDrumHit(float velocity, Vector3 position, DrumStickXR.StickHand hand)
    {
        Debug.Log($"{hand} drum hit detected with velocity {velocity} at position {position}");

        ParticleSystem chosenEffect = null;

        if (hand == DrumStickXR.StickHand.Left)
            chosenEffect = hitEffect_L;
        else if (hand == DrumStickXR.StickHand.Right)
            chosenEffect = hitEffect_R;

        if (chosenEffect != null)
        {
            var main = chosenEffect.main;

            main.startSize = Mathf.Lerp(0.05f, 1.5f, velocity);
            main.startColor = colorGradient.Evaluate(velocity);

            chosenEffect.transform.position = position;
            chosenEffect.Stop();
            chosenEffect.Play();
        }
    }
}