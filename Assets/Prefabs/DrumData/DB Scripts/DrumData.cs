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
    public string color;
    public string drumName;
    public GameObject prefab;
    public DrumRarity rarity;
}
