using UnityEngine;
using TMPro;
using DG.Tweening;

public class HitPopupManager : MonoBehaviour
{
    public GameObject popupPrefab;
    public RectTransform spawnContainer;
    
    public float animationDuration = 0.8f;
    public float upwardDistance = 150f;
    public float horizontalSpread = 75f;

    void OnEnable()
    {
        GlobalEventAsset.Instance.StartListening<OnHitScore>(OnHitReceived);
    }

    void OnDisable()
    {
        GlobalEventAsset.Instance.StopListening<OnHitScore>(OnHitReceived);
    }

    void OnHitReceived(OnHitScore data)
    {
        GameObject newPopup = Instantiate(popupPrefab, spawnContainer);
        RectTransform rectTransform = newPopup.GetComponent<RectTransform>();
        TextMeshProUGUI textMesh = newPopup.GetComponent<TextMeshProUGUI>();

        rectTransform.anchoredPosition = Vector2.zero;

        switch (data.hitType)
        {
            case HitType.Perfect:
                textMesh.text = "Perfect!";
                textMesh.color = Color.yellow;
                break;
            case HitType.Good:
                textMesh.text = "Good";
                textMesh.color = Color.green;
                break;
            case HitType.Bad:
                textMesh.text = "Bad";
                textMesh.color = Color.red;
                break;
        }

        float randomX = UnityEngine.Random.Range(-horizontalSpread, horizontalSpread);
        Vector2 targetPosition = new Vector2(randomX, upwardDistance);

        rectTransform.DOAnchorPos(targetPosition, animationDuration).SetEase(Ease.OutCubic);
        
        textMesh.DOFade(0f, animationDuration).SetEase(Ease.InQuart).OnComplete(() =>
        {
            Destroy(newPopup);
        });
    }
}