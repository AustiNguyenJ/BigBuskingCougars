using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseDrumScript : MonoBehaviour
{
    [Header("Input (temporary)")]
    public KeyCode hitKey = KeyCode.F;

    [Header("Refs")]
    public Transform visualTransform;     // assign Visual (ideally drum head mesh)
    public AudioSource audioSource;       // assign AudioSource (3D)
    public AudioClip hitClip;

    [Header("Drum Head Deformation")]
    [Range(0.7f, 1f)] public float compressY = 0.90f;
    [Range(1f, 1.1f)] public float expandXZ = 1.02f;
    [Range(1f, 1.1f)] public float reboundY = 1.03f;
    [Range(0.9f, 1f)] public float reboundXZ = 0.99f;

    [Header("Animation Timing (seconds)")]
    [Min(0.001f)] public float downTime = 0.03f;
    [Min(0.001f)] public float reboundTime = 0.04f;
    [Min(0.001f)] public float settleTime = 0.02f;

    Vector3 _baseScale;
    Coroutine _punchRoutine;

    void Awake()
    {
        if (visualTransform == null) visualTransform = transform;
        _baseScale = visualTransform.localScale;

        if (audioSource == null)
            audioSource = GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // KeyCode -> InputSystem Key (by name)
        if (!Enum.TryParse(hitKey.ToString(), true, out Key key)) return;

        if (Keyboard.current[key].wasPressedThisFrame)
            Hit();
    }

    public void Hit()
    {
        if (audioSource != null && hitClip != null)
            audioSource.PlayOneShot(hitClip);

        if (visualTransform == null) return;

        if (_punchRoutine != null) StopCoroutine(_punchRoutine);
        _punchRoutine = StartCoroutine(DrumHeadPunch());
    }

    IEnumerator DrumHeadPunch()
    {
        // In case scale changes at runtime
        _baseScale = visualTransform.localScale;

        Vector3 down = new Vector3(
            _baseScale.x * expandXZ,
            _baseScale.y * compressY,
            _baseScale.z * expandXZ
        );

        Vector3 overshoot = new Vector3(
            _baseScale.x * reboundXZ,
            _baseScale.y * reboundY,
            _baseScale.z * reboundXZ
        );

        // Down
        for (float t = 0f; t < downTime; t += Time.deltaTime)
        {
            float a = t / downTime;
            visualTransform.localScale = Vector3.Lerp(_baseScale, down, a);
            yield return null;
        }

        // Rebound
        for (float t = 0f; t < reboundTime; t += Time.deltaTime)
        {
            float a = t / reboundTime;
            visualTransform.localScale = Vector3.Lerp(down, overshoot, a);
            yield return null;
        }

        // Settle
        for (float t = 0f; t < settleTime; t += Time.deltaTime)
        {
            float a = t / settleTime;
            visualTransform.localScale = Vector3.Lerp(overshoot, _baseScale, a);
            yield return null;
        }

        visualTransform.localScale = _baseScale;
        _punchRoutine = null;
    }
}