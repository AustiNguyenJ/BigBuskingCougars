using System.Collections.Generic;
using UnityEngine;

public class DrumDatabase : MonoBehaviour
{
    public static DrumDatabase Instance;

    [SerializeField] private List<DrumData> allDrums = new();

    private Dictionary<string, DrumData> drumLookup = new();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        BuildLookup();
    }

    void BuildLookup()
    {
        drumLookup.Clear();

        foreach (DrumData drum in allDrums)
        {
            if (drum == null)
                continue;

            if (string.IsNullOrWhiteSpace(drum.drumId))
            {
                Debug.LogWarning($"DrumDatabase: Drum asset '{drum.name}' has no drumId.");
                continue;
            }

            if (drumLookup.ContainsKey(drum.drumId))
            {
                Debug.LogWarning($"DrumDatabase: Duplicate drumId '{drum.drumId}' found on '{drum.name}'.");
                continue;
            }

            drumLookup.Add(drum.drumId, drum);
        }
    }

    public DrumData GetDrumById(string drumId)
    {
        if (drumLookup.TryGetValue(drumId, out DrumData drum))
            return drum;

        Debug.LogWarning($"DrumDatabase: No drum found with id '{drumId}'.");
        return null;
    }

    public List<DrumData> GetAllDrums()
    {
        return allDrums;
    }
}