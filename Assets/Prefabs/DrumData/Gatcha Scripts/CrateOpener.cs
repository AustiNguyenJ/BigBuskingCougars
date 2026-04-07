using UnityEngine;

public class CrateOpener : MonoBehaviour
{
    public DrumData OpenCrate(CrateData crate)
    {
        if (crate == null || crate.lootTable == null)
        {
            Debug.LogWarning("CrateOpener: Invalid crate or missing loot table.");
            return null;
        }

        var entries = crate.lootTable.entries;

        if (entries == null || entries.Count == 0)
        {
            Debug.LogWarning("CrateOpener: Loot table is empty.");
            return null;
        }

        // 1. Calculate total weight
        int totalWeight = 0;
        foreach (var entry in entries)
        {
            if (entry.drum == null || entry.weight <= 0)
                continue;

            totalWeight += entry.weight;
        }

        // 2. Roll random number
        int roll = Random.Range(0, totalWeight);

        // 3. Find result
        int cumulative = 0;
        foreach (var entry in entries)
        {
            if (entry.drum == null || entry.weight <= 0)
                continue;

            cumulative += entry.weight;

            if (roll < cumulative)
            {
                return entry.drum;
            }
        }

        // Fallback (should never happen)
        Debug.LogWarning("CrateOpener: Roll failed, returning null.");
        return null;
    }
}