using System.Collections;
using UnityEngine;

public class DrumVisual : MonoBehaviour
{
    [Header("Visual Target")]
    public Transform visualTarget;

    [Header("Deformation")]
    [Range(0.7f, 1f)] public float compressY = 0.986f;
    [Range(1f, 1.1f)] public float expandXZ = 1.0023f;
    [Range(1f, 1.1f)] public float reboundY = 1.0046f;
    [Range(0.9f, 1f)] public float reboundXZ = 0.9969f;

    [Header("Power Response")]
    public float maxExtraCompression = 0.08f;
    public float maxExtraExpand = 0.02f;
    public float maxPositionShift = 0.00f;

    [Header("Animation Timing")]
    public float downTime = 0.03f;
    public float reboundTime = 0.04f;
    public float settleTime = 0.02f;

    Vector3 restScale;
    Vector3 restLocalPosition;
    Coroutine punchRoutine;

    void Awake()
    {
        if (visualTarget == null)
            visualTarget = transform;

        restScale = visualTarget.localScale;
        restLocalPosition = visualTarget.localPosition;
    }

    public void PlayHitVisual(float power, DrumSurfaceZones.ZoneResult zoneResult)
    {
        if (visualTarget == null) return;

        if (punchRoutine != null)
            StopCoroutine(punchRoutine);

        punchRoutine = StartCoroutine(DrumHeadPunch(power, zoneResult));
    }

    IEnumerator DrumHeadPunch(float power, DrumSurfaceZones.ZoneResult zoneResult)
    {
        Vector3 startScale = visualTarget.localScale;
        Vector3 startPosition = visualTarget.localPosition;

        float zoneCompressionBonus = 0f;

        switch (zoneResult.zone)
        {
            case DrumSurfaceZones.DrumZone.Center:
                zoneCompressionBonus = 1.0f;
                break;
            case DrumSurfaceZones.DrumZone.Mid:
                zoneCompressionBonus = 0.7f;
                break;
            case DrumSurfaceZones.DrumZone.Edge:
                zoneCompressionBonus = 0.45f;
                break;
            default:
                zoneCompressionBonus = 0.2f;
                break;
        }

        float compressionAmount = power * zoneCompressionBonus;

        float finalCompressY = compressY - (maxExtraCompression * compressionAmount);
        float finalExpandXZ = expandXZ + (maxExtraExpand * compressionAmount);

        Vector3 downScale = new Vector3(
            restScale.x * finalExpandXZ,
            restScale.y * finalCompressY,
            restScale.z * finalExpandXZ
        );

        Vector3 overshootScale = new Vector3(
            restScale.x * reboundXZ,
            restScale.y * reboundY,
            restScale.z * reboundXZ
        );

        Vector3 localOffset = zoneResult.localHit;
        localOffset.y = 0f;

        Vector3 positionOffset = Vector3.zero;
        if (localOffset.sqrMagnitude > 0.0001f)
        {
            Vector3 offsetDir = localOffset.normalized;
            positionOffset = new Vector3(offsetDir.x, 0f, offsetDir.z) *
                             (maxPositionShift * power * zoneResult.normalizedDistance);
        }

        Vector3 downPosition = restLocalPosition + positionOffset;

        for (float t = 0f; t < downTime; t += Time.deltaTime)
        {
            float a = t / downTime;
            visualTarget.localScale = Vector3.Lerp(startScale, downScale, a);
            visualTarget.localPosition = Vector3.Lerp(startPosition, downPosition, a);
            yield return null;
        }

        for (float t = 0f; t < reboundTime; t += Time.deltaTime)
        {
            float a = t / reboundTime;
            visualTarget.localScale = Vector3.Lerp(downScale, overshootScale, a);
            visualTarget.localPosition = Vector3.Lerp(downPosition, restLocalPosition, a);
            yield return null;
        }

        for (float t = 0f; t < settleTime; t += Time.deltaTime)
        {
            float a = t / settleTime;
            visualTarget.localScale = Vector3.Lerp(overshootScale, restScale, a);
            visualTarget.localPosition = Vector3.Lerp(restLocalPosition, restLocalPosition, a);
            yield return null;
        }

        visualTarget.localScale = restScale;
        visualTarget.localPosition = restLocalPosition;
        punchRoutine = null;
    }
}