using UnityEngine;
//Attach this to each drum object
//Accuracy script is in this same script
public class DrumSurface : MonoBehaviour
{
    public int basePoints = 10;

    public float perfectRadius = 0.08f;
    public float goodRadius = 0.18f;

    float lastHitTime;

    private void OnTriggerEnter(Collider other)
    {
        DrumStickXR stick = other.GetComponent<DrumStickXR>();

        if (stick == null) return;

        if (Time.time - lastHitTime < 0.05f)
            return;

        lastHitTime = Time.time;

        Vector3 hitPosition = other.ClosestPoint(transform.position);

        RegisterHit(stick, hitPosition);
    }

    void RegisterHit(DrumStickXR stick, Vector3 hitPosition)
    {
        float velocity = stick.velocity;

        if (velocity < 0.4f)
            return;

        float power = Mathf.Clamp01(velocity / 4f);

        float accuracy = CalculateAccuracy(hitPosition);

        // HIT FEEDBACK
        if (accuracy >= 2f)
            Debug.Log("PERFECT HIT!");

        else if (accuracy >= 1.2f)
            Debug.Log("GOOD HIT");

        else
            Debug.Log("EDGE HIT");

        int score = Mathf.RoundToInt(basePoints * power * accuracy);

        ScoreManager.Instance.AddScore(score);
    }

    float CalculateAccuracy(Vector3 hitPosition)
    {
        Vector3 localHit = transform.InverseTransformPoint(hitPosition);

        float distance = new Vector2(localHit.x, localHit.z).magnitude;

        if (distance < perfectRadius)
            return 2.0f;   // perfect hit

        if (distance < goodRadius)
            return 1.2f;   // good hit

        return 0.6f; // edge hit
    }
}