using UnityEngine;

public class DrumSurfaceZones : MonoBehaviour
{
    public enum DrumZone
    {
        Outside,
        Center,
        Mid,
        Edge
    }

    [System.Serializable]
    public struct ZoneResult
    {
        public DrumZone zone;
        public float distance;
        public float normalizedDistance;
        public Vector3 localHit;

        public ZoneResult(DrumZone zone, float distance, float normalizedDistance, Vector3 localHit)
        {
            this.zone = zone;
            this.distance = distance;
            this.normalizedDistance = normalizedDistance;
            this.localHit = localHit;
        }
    }

    [Header("Zone Percentages (0–1)")]
    [Range(0f, 1f)] public float centerPercent = 0.2f;
    [Range(0f, 1f)] public float midPercent = 0.5f;
    [Range(0f, 1f)] public float edgePercent = 1.0f;

    float drumRadius;

    void Awake()
    {
        CalculateDrumRadius();
    }

    void CalculateDrumRadius()
    {
        Renderer r = GetComponentInChildren<Renderer>();

        if (r != null)
            drumRadius = r.bounds.extents.x;
        else
        {
            Debug.LogWarning("No Renderer found for DrumSurfaceZones.");
            drumRadius = 0.5f;
        }
    }

    public ZoneResult EvaluateZone(Vector3 worldHitPosition)
    {
        Vector3 localHit = transform.InverseTransformPoint(worldHitPosition);
        localHit.y = 0f;

        float distance = localHit.magnitude;
        float normalizedDistance = drumRadius > 0f ? distance / drumRadius : 0f;

        if (normalizedDistance < centerPercent)
            return new ZoneResult(DrumZone.Center, distance, normalizedDistance, localHit);

        if (normalizedDistance < midPercent)
            return new ZoneResult(DrumZone.Mid, distance, normalizedDistance, localHit);

        if (normalizedDistance <= edgePercent)
            return new ZoneResult(DrumZone.Edge, distance, normalizedDistance, localHit);

        return new ZoneResult(DrumZone.Outside, distance, normalizedDistance, localHit);
    }
}