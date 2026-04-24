using System.Collections.Generic;
using UnityEngine;

public class DrumLoadoutSpawner : MonoBehaviour
{
    public static DrumLoadoutSpawner Instance;

    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    
    private Dictionary<DrumData, GameObject> drumMap = new Dictionary<DrumData, GameObject>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SpawnDrum(DrumData drum)
    {
        List<DrumData> equippedDrums = InventoryManager.Instance.GetEquippedDrums();

        if (!equippedDrums.Contains(drum))
            return;
            
        if (drum == null)
            return;

        if (drum.prefab == null)
        {
            Debug.LogWarning($"Drum '{drum.drumId}' has no prefab assigned.");
            return;
        }

        Transform spawnPoint = spawnPoints[0];
        GameObject spawned = Instantiate(
            drum.prefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        drumMap[drum] = spawned;
    }

    public void DeSpawnDrum(DrumData drum)
    {
        if (drum == null)
            return;

        if (drumMap.TryGetValue(drum, out GameObject spawned))
        {
            Destroy(spawned);
            drumMap.Remove(drum);
        }
    }

    public void ClearSpawnedDrums()
    {
        foreach (GameObject drum in drumMap.Values)
        {
            Destroy(drum);
        }

        drumMap.Clear();
    }
}