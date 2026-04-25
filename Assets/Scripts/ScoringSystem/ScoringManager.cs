using UnityEngine;
using System.Collections;
using TMPro;

public class ScoringManager : MonoBehaviour
{
    public static ScoringManager Instance;

    [Header("Player Reference")]
    public PlayerData player;
    
    [Header("Floating Score")]
    public GameObject floatingScorePrefab;

    [Header("Spawn Area")]
    public BoxCollider spawnArea;

    [Header("Velocity Normalization")]
    public float minVelocity = 0f;
    public float maxVelocity = 4f;

    [Header("Score Values")]
    public int slowScore = 5;
    public int goodScore = 10;
    public int fastScore = 20;

    [Header("Score Colors")]
    public Color slowColor = new Color32(0x65, 0x65, 0x65, 0xFF);
    public Color goodColor = new Color32(0xFF, 0xE2, 0x79, 0xFF);
    public Color fastColor = new Color32(0x78, 0x00, 0x12, 0xFF);

    [Header("UI Score Display")]
    public TextMeshPro scoreText;

    [Header("Pulse Settings")]
    public float pulseScaleMultiplier = 1.2f;
    public float pulseDuration = 0.1f;
    public float maxPulseTilt = 10f;


    
    public void UpdateScoreUI()
    {
        if (scoreText != null && player != null)
        {
            scoreText.text = "Boings : " + player.money;
        }
    }

    Vector3 baseScale;
    Quaternion baseRotation;
    Coroutine pulseCoroutine;

    void Awake()
    {
        Debug.Log("ScoringManager Awake called", this);
    
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.GetComponent<PlayerData>();
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        if (scoreText != null)
        {
            baseScale = scoreText.transform.localScale;
            baseRotation = scoreText.transform.localRotation;
        }

        UpdateScoreUI();
    }

    public Color ProcessHit(float velocity)
    {
        int amount = GetScoreFromVelocity(velocity);
        
        if (player != null)
        {
            player.AddMoney(amount);
        }

        UpdateScoreUI();

        Color color = GetColorFromScore(amount);
        ShowFloatingScore(amount, color);

        TriggerPulse();

        return color;
    }

    void TriggerPulse()
    {
        if (scoreText == null)
            return;

        if (pulseCoroutine != null)
            StopCoroutine(pulseCoroutine);

        scoreText.transform.localScale = baseScale;
        scoreText.transform.localRotation = baseRotation;

        pulseCoroutine = StartCoroutine(PulseScore());
    }

    IEnumerator PulseScore()
    {
        float randomTilt = Random.Range(0f, 1f) < 0.5f
            ? -Random.Range(3f, maxPulseTilt)
            : Random.Range(3f, maxPulseTilt);

        Vector3 targetScale = baseScale * pulseScaleMultiplier;
        Quaternion targetRotation = baseRotation * Quaternion.Euler(0f, 0f, randomTilt);

        float timer = 0f;

        while (timer < pulseDuration)
        {
            timer += Time.deltaTime;
            float t = timer / pulseDuration;

            scoreText.transform.localScale = Vector3.Lerp(targetScale, baseScale, t);
            scoreText.transform.localRotation = Quaternion.Lerp(targetRotation, baseRotation, t);

            yield return null;
        }

        scoreText.transform.localScale = baseScale;
        scoreText.transform.localRotation = baseRotation;
        pulseCoroutine = null;
    }
    

    void ShowFloatingScore(int amount, Color color)
    {
        if (floatingScorePrefab == null || spawnArea == null)
        {
            Debug.LogWarning("ScoringManager missing prefab or spawnArea.", this);
            return;
        }

        Vector3 spawnPosition = GetRandomPointInBox();
        GameObject scoreObj = Instantiate(floatingScorePrefab, spawnPosition, Quaternion.identity);

        FloatingScore floatingScore = scoreObj.GetComponent<FloatingScore>();
        if (floatingScore != null)
        {
            floatingScore.SetText("+" + amount + "b", color);
        }
        else
        {
            Debug.LogWarning("Spawned prefab is missing FloatingScore script.", scoreObj);
        }
    }

    int GetScoreFromVelocity(float velocity)
    {
        float normalized = Mathf.InverseLerp(minVelocity, maxVelocity, velocity);
        float scaledVelocity = Mathf.Lerp(0f, 100f, normalized);

        switch (scaledVelocity)
        {
            case float v when v <= 20f:
                return slowScore;
            case float v when v <= 50f:
                return goodScore;
            default:
                return fastScore;
        }
    }

    Color GetColorFromScore(int score)
    {
        switch (score)
        {
            case var s when s == slowScore:
                return slowColor;
            case var s when s == goodScore:
                return goodColor;
            case var s when s == fastScore:
                return fastColor;
            default:
                return Color.white;
        }
    }

    Vector3 GetRandomPointInBox()
    {
        Bounds bounds = spawnArea.bounds;

        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    public int GetTotalScore()
    {
        return player != null ? player.money : 0;
    }

    public void ResetScore()
    {
        if (player != null)
            player.money = 0;

        UpdateScoreUI();

        if (scoreText != null)
        {
            scoreText.transform.localScale = baseScale;
            scoreText.transform.localRotation = baseRotation;
        }

        if (pulseCoroutine != null)
        {
            StopCoroutine(pulseCoroutine);
            pulseCoroutine = null;
        }
    }
}