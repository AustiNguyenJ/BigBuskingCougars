using UnityEngine;using System.Collections;

public class Crate3DController : MonoBehaviour
{
    [Header("Drum Spawn & Hover")]
    public Transform drumSpawnPoint;
    public Vector3 drumHoverAmplitude = new Vector3(0, 0.2f, 0);
    public float drumHoverSpeed = 2f;
    public float drumHoverDuration = 3f; 
    [Header("Crate Shake")]
    public float shakeDuration = 1f;
    public float shakeMagnitude = 0.1f;

    [Header("Particles")]
    public ParticleSystem breakParticles;

    private GameObject spawnedDrum;

    public void OpenCrate(DrumData drumData)
    {
        StartCoroutine(UnboxRoutine(drumData));
    }

    private IEnumerator UnboxRoutine(DrumData drumData)
    {
        // --- 1️⃣ Shake crate with buildup ---
        Quaternion originalRot = transform.rotation;
        Vector3 originalScale = transform.localScale;

        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float progress = elapsed / shakeDuration; // 0 → 1

            // Buildup factor (ease-in): start small, end at full magnitude
            float shakeFactor = Mathf.Pow(progress, 2f); // quadratic ease-in
                                                         // could also use Mathf.Sin(progress * Mathf.PI * 0.5f) for smooth easing

            // Rotation shake: small random rotations multiplied by shakeFactor
            float rotX = Random.Range(-shakeMagnitude, shakeMagnitude) * shakeFactor * 50;
            float rotY = Random.Range(-shakeMagnitude, shakeMagnitude) * shakeFactor * 50;
            float rotZ = Random.Range(-shakeMagnitude, shakeMagnitude) * shakeFactor * 50;
            transform.rotation = originalRot * Quaternion.Euler(rotX, rotY, rotZ);

            // Scale shake: small pulsing multiplied by shakeFactor
            float scaleFactor = 1f + Random.Range(-shakeMagnitude, shakeMagnitude) * 0.5f * shakeFactor;
            transform.localScale = originalScale * scaleFactor;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset to original rotation and scale
        transform.rotation = originalRot;
        transform.localScale = originalScale;


        // --- 2️⃣ Play break particles ---
        if (breakParticles != null)
            breakParticles.Play();

        // --- 3️⃣ Hide crate mesh ---
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var r in renderers)
            r.enabled = false;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        // --- 4️⃣ Spawn drum prefab ---
        if (drumData.prefab != null)
        {
            spawnedDrum = Instantiate(drumData.prefab, drumSpawnPoint.position, Quaternion.identity);
        }

        // --- 5️⃣ Hover drum for set duration ---
        float hoverTime = 0f;
        float timer = 0f;
        while (timer < drumHoverDuration)
        {
            if (spawnedDrum != null)
            {
                Vector3 hover = drumHoverAmplitude * Mathf.Sin(hoverTime * drumHoverSpeed);
                spawnedDrum.transform.position = drumSpawnPoint.position + hover;
            }

            hoverTime += Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        // --- 6️⃣ Stop hover, optionally keep drum static ---
        if (spawnedDrum != null)
        {
            spawnedDrum.transform.position = drumSpawnPoint.position;
        }

        // Optional: Destroy crate object if you want
         Destroy(spawnedDrum);
    }
}