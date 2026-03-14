using UnityEngine;
//Attach this to a ComboManager Object / or in either Stage/Crowd Manager
//depends if you want to keep things neet
public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    public float scoreMultiplier = 1f;

    void Awake()
    {
        Instance = this;
    }

    public void BuyDrumUpgrade()
    {
        scoreMultiplier += 0.5f;
    }
}