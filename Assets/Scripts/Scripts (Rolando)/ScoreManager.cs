using UnityEngine;
//Attach this to an empty GameObject called "GameManager"
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score;

    void Awake()
    {
        Instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;

        StageManager.Instance.CheckProgress(score);
        CrowdManager.Instance.CheckCrowd(score);
    }
}