using UnityEngine;
using UnityEngine.InputSystem;

public class DrumHitTester : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip drumHitClip;

    public DrumStickXR.StickHand testHand = DrumStickXR.StickHand.Left;

    private float[] testVelocity = { 0.1f, 0.3f, 0.5f, 0.7f, 0.9f };
    private int currIndex = 0;

    private int[] comboValues = { 0, 10, 25, 50 };
    private int comboIndex = 0;

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            PlayDrumHit();
        }

        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            SimComboUpdate();
        }
    }

    void PlayDrumHit()
    {
        if (audioSource != null && drumHitClip != null)
        {
            audioSource.PlayOneShot(drumHitClip);
        }

        float velocity = testVelocity[currIndex];
        Debug.Log($"Velocity: {velocity}, Hand: {testHand}");

        VisualResponseSystem.TriggerDrumHit(velocity, transform.position, testHand);

        currIndex = (currIndex + 1) % testVelocity.Length;
    }

    void SimComboUpdate()
    {
        int combo = comboValues[comboIndex];
        Debug.Log($"Simulating combo update: {combo}");
        VisualResponseSystem.TriggerComboUpdate(combo);

        comboIndex = (comboIndex + 1) % comboValues.Length;
    }
}