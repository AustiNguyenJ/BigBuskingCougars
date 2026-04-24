using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInventory
{
    public List<DrumData> ownedDrumIds = new List<DrumData>();
    public List<DrumData> equippedDrumIds = new List<DrumData>();
}