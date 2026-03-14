using UnityEngine;
//Attach this to a ComboManager Object / or in either Stage/Crowd Manager
//depends if you want to keep things neet
public class ComboManager : MonoBehaviour
{
    public static ComboManager Instance;

    public int combo;
    public float comboResetTime = 2f;

    float lastHitTime;

    void Awake()
    {
        Instance = this;
    }

    public void RegisterHit()
    {
        if(Time.time - lastHitTime > comboResetTime)
            combo = 0;

        combo++;

        lastHitTime = Time.time;
    }

    public int GetMultiplier()
    {
        if (combo > 20) return 5;
        if (combo > 10) return 3;
        if (combo > 5) return 2;

        return 1;
    }
}