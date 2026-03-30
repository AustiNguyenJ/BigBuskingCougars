using System.Collections.Generic;
using UnityEngine;

public class DrumLoadoutSpawner : MonoBehaviour
{
    public static DrumLoadoutSpawner Instance;

    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    private readonly List<GameObject> spawnedDrums = new List<GameObject>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RefreshLoadout()
    {
        ClearSpawnedDrums();

        List<string> equippedDrumIds = InventoryManager.Instance.GetEquippedDrumIds();

        for (int i = 0; i < equippedDrumIds.Count; i++)
        {
            if (i >= spawnPoints.Count)
            {
                Debug.LogWarning("Not enough spawn points for equipped drums.");
                break;
            }

            string drumId = equippedDrumIds[i];
            DrumData drumData = DrumDatabase.Instance.GetDrumById(drumId);

            if (drumData == null)
                continue;

            if (drumData.prefab == null)
            {
                Debug.LogWarning($"Drum '{drumId}' has no prefab assigned.");
                continue;
            }

            Transform spawnPoint = spawnPoints[i];
            GameObject spawned = Instantiate(
                drumData.prefab,
                spawnPoint.position,
                spawnPoint.rotation
            );

            spawnedDrums.Add(spawned);
        }
    }

    void ClearSpawnedDrums()
    {
        for (int i = 0; i < spawnedDrums.Count; i++)
        {
            if (spawnedDrums[i] != null)
                Destroy(spawnedDrums[i]);
        }

        spawnedDrums.Clear();
    }
}