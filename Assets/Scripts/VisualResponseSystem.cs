using UnityEngine;
using System;

public class VisualResponseSystem : MonoBehaviour
{
    public static Action<float, Vector3> OneDrumHit;
    public static event Action<int> OnComboUpdated;

    public static void TriggerComboUpdate(int combo)
    {
        OnComboUpdated?.Invoke(combo);
    }

    public static void TriggerDrumHit(float velocity, Vector3 position)
    {
        OneDrumHit?.Invoke(velocity, position);
    }

    public ParticleSystem hitEffect;

    private void OnEnable()
    {
        OneDrumHit += HandleDrumHit;
    }

    private void OnDisable()
    {
        OneDrumHit -= HandleDrumHit;
    }

    private void HandleDrumHit(float velocity, Vector3 position)
    {
        Debug.Log($"Drum hit detected with velocity {velocity} at position {position}");

        if (hitEffect != null)
        {
            var main = hitEffect.main;

            main.startSize = Mathf.Lerp(0.5f, 3.0f, velocity);
            main.startColor = Color.Lerp(Color.cyan, Color.red, velocity);

            hitEffect.transform.position = position;
            hitEffect.Stop();
            hitEffect.Play();
        }
    }
}