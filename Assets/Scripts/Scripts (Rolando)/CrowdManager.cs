using UnityEngine;
//Attach this to a CrowdManager object.
public class CrowdManager : MonoBehaviour
{
    public static CrowdManager Instance;

    public GameObject fanPrefab;
    public Transform[] spawnPoints;

    int currentFans;

    void Awake()
    {
        Instance = this;
    }

    public void CheckCrowd(int score)
    {
        int targetFans = score / 200;

        if(targetFans > currentFans)
        {
            SpawnFan();
            currentFans++;
        }
    }

    void SpawnFan()
    {
        int random = Random.Range(0, spawnPoints.Length);

        Instantiate(fanPrefab, spawnPoints[random].position, Quaternion.identity);
    }
}