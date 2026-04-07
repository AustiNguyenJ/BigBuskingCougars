using UnityEngine;
using TMPro;

public class FloatingScore : MonoBehaviour
{
    TextMeshPro textMesh;

    [Header("Motion")]
    public float upwardSpeed = 1.5f;
    public float sidewaysSpeed = 0.4f;
    public float swayAmount = 0.2f;
    public float swayFrequency = 3f;
    public float lifetime = 1f;

    [Header("Rotation")]
    public float rotationSpeedMin = -40f;
    public float rotationSpeedMax = 40f;

    [Header("Facing")]
    public bool faceCamera = true;

    float timer;
    Color startColor;

    Vector3 startPosition;
    Vector3 driftDirection;
    float swayOffset;
    float rotationSpeed;

    void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshPro>();

        if (textMesh == null)
        {
            Debug.LogError("FloatingScore: No TextMeshPro found in this prefab or its children.", this);
            return;
        }

        startColor = textMesh.color;
        startPosition = transform.position;

        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        driftDirection = new Vector3(randomX, 0f, randomZ).normalized;

        swayOffset = Random.Range(0f, Mathf.PI * 2f);
        rotationSpeed = Random.Range(rotationSpeedMin, rotationSpeedMax);
    }

    public void SetText(string value, Color color)
    {
        if (textMesh != null)
        {
            textMesh.text = value;
            textMesh.color = color;
            startColor = color;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        float normalizedTime = timer / lifetime;

        Vector3 upwardOffset = Vector3.up * upwardSpeed * timer;
        Vector3 diagonalOffset = driftDirection * sidewaysSpeed * timer;
        Vector3 swayOffsetVec = driftDirection * Mathf.Sin((timer * swayFrequency) + swayOffset) * swayAmount;

        transform.position = startPosition + upwardOffset + diagonalOffset + swayOffsetVec;

        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        if (faceCamera && Camera.main != null)
        {
            transform.forward = Camera.main.transform.forward;
        }

        if (textMesh != null)
        {
            Color c = startColor;
            c.a = Mathf.Lerp(1f, 0f, normalizedTime);
            textMesh.color = c;
        }

        if (timer >= lifetime)
            Destroy(gameObject);
    }
}