using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInventory
{
    public List<string> ownedDrumIds = new List<string>();
    public List<string> equippedDrumIds = new List<string>();
}