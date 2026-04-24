using UnityEngine;

[CreateAssetMenu(fileName = "NewCrate", menuName = "Drums/Crate")]
public class CrateData : ScriptableObject
{
    public string crateId;
    public string displayName;
    public int price;

    public LootTable lootTable;
}