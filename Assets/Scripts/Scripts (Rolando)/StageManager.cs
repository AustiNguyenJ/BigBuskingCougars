using UnityEngine;
//Attach this to the same GameManager in all 3 Stages
public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public int currentStage = 1;

    void Awake()
    {
        Instance = this;
    }

    public void CheckProgress(int score)
    {
        if (currentStage == 1 && score >= 500)
        {
            UnlockGarage();
        }

        if (currentStage == 2 && score >= 2000)
        {
            UnlockConcert();
        }
    }

    void UnlockGarage()
    {
        currentStage = 2;
        Debug.Log("Garage Stage Unlocked");
    }

    void UnlockConcert()
    {
        currentStage = 3;
        Debug.Log("Concert Stage Unlocked");
    }
}