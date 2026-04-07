using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLootTable", menuName = "Drums/Loot Table")]
public class LootTable : ScriptableObject
{
    public List<LootEntry> entries = new();
}

[System.Serializable]
public class LootEntry
{
    public DrumData drum;
    public int weight;
}