using UnityEngine;

// Attach this to each drum object
public class DrumSurface : MonoBehaviour
{
    float lastHitTime;

    DrumFeedback feedback;
    DrumSurfaceZones zones;

    void Awake()
    {
        feedback = GetComponent<DrumFeedback>();
        zones = GetComponent<DrumSurfaceZones>();
    }

    private void OnTriggerEnter(Collider other)
    {
        DrumStickXR stick = other.GetComponent<DrumStickXR>();

        if (stick == null) return;

        if (Time.time - lastHitTime < 0.05f)
            return;

        lastHitTime = Time.time;

        Vector3 hitPosition = other.transform.position;

        RegisterHit(stick, hitPosition);
    }

    void RegisterHit(DrumStickXR stick, Vector3 hitPosition)
    {
        float velocity = stick.velocity;

        if (velocity < 0.4f)
            return;

        DrumSurfaceZones.ZoneResult zoneResult = zones.EvaluateZone(hitPosition);

        if (feedback != null)
            feedback.Hit(velocity, zoneResult);

        float power = Mathf.Clamp01(velocity / 4f);
        var hand = stick.hand;

        Debug.Log($"Zone: {zoneResult.zone} | Dist: {zoneResult.normalizedDistance:F2}");


        VisualResponseSystem.TriggerDrumHit(power, hitPosition, hand);
    }
}