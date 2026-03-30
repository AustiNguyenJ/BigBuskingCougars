using UnityEngine;

public enum DrumRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}

[CreateAssetMenu(fileName = "NewDrum", menuName = "Drums/Drum Data")]
public class DrumData : ScriptableObject
{
    public string drumId;
    public GameObject prefab;
    public DrumRarity rarity;
}
