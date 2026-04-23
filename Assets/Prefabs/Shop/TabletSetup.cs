using UnityEngine;

public class TabletSetup : MonoBehaviour
{
    [Header("References")]
    public Transform tablet;
    public CanvasGroup uiGroup;
    public Transform uiTransform; // hologram object
    public ParticleSystem crateParticles;

    [Header("Facing Detection")]
    public float showThreshold = 0.6f;
    public float hideThreshold = 0.4f;

    [Header("Animation")]
    public float fadeSpeed = 5f;
    public float moveSpeed = 5f;
    public Vector3 hiddenOffset = new Vector3(0, -0.5f, 0);
    public Vector3 visibleOffset = new Vector3(0, 0.4f, 0);

    private bool isFacingUp = false;
    private float currentAlpha = 0f;
    
    void Update()
    {
        HandleOrientation();
        AnimateUI();
    }

    void HandleOrientation()
    {
        float dot = Vector3.Dot(tablet.up, Vector3.up);

        if (!isFacingUp && dot > showThreshold)
            isFacingUp = true;

        if (isFacingUp && dot < hideThreshold)
            isFacingUp = false;
    }

    void AnimateUI()
    {
        float targetAlpha = isFacingUp ? 1f : 0f;
        currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, Time.deltaTime * fadeSpeed);

        
        var emission = crateParticles.emission;
        emission.enabled = isFacingUp;

        uiGroup.alpha = currentAlpha;
        uiGroup.interactable = currentAlpha > 0.9f;
        uiGroup.blocksRaycasts = currentAlpha > 0.9f;

        // Position animation
        Vector3 targetOffset = isFacingUp ? visibleOffset : hiddenOffset;
        uiTransform.localPosition = Vector3.Lerp(
            uiTransform.localPosition,
            targetOffset,
            Time.deltaTime * moveSpeed
        );
    }
}
