using UnityEngine;

public class VisualNote : MonoBehaviour
{
    RectTransform rectTransform;
    float targetTime; 
    float scrollSpeed; 
    float hitZoneY; 
    Conductor conductor;

    bool isInitialized = false;

    public void Initialize(float target, float speed, float hitY, Conductor cond)
    {
        rectTransform = GetComponent<RectTransform>();
        targetTime = target;
        scrollSpeed = speed;
        hitZoneY = hitY;
        conductor = cond;
        
        isInitialized = true;
    }

    void Update()
    {
        if (!isInitialized) return;

        float timeRemaining = targetTime - conductor.songPosition;
        float distanceFromHitZone = timeRemaining * scrollSpeed;
        float currentY = hitZoneY + distanceFromHitZone;

        rectTransform.anchoredPosition = new Vector2(0f, currentY);

        if (timeRemaining < -0.5f)
        {
            Destroy(gameObject);
        }
    }
}