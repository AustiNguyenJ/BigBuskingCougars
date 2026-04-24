using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] private PlayerInventory inventory = new PlayerInventory();
    [SerializeField] private int maxEquippedDrums = 6;

    

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public bool HasDrum(DrumData drumId)
    {
        return inventory.ownedDrumIds.Contains(drumId);
    }

    public void AddDrum(DrumData drumId)
    {
        if (HasDrum(drumId))
            return;

        inventory.ownedDrumIds.Add(drumId);
        Debug.Log($"Added drum: {drumId}");
    }

    public bool RemoveDrum(DrumData drumId)
    {
        if (!HasDrum(drumId))
        {
            Debug.LogWarning($"Cannot remove drum '{drumId}' because it is not owned.");
            return false;
        }

        inventory.ownedDrumIds.Remove(drumId);
        inventory.equippedDrumIds.Remove(drumId);

        Debug.Log($"Removed drum: {drumId}");
        return true;
    }

    public bool IsEquipped(DrumData drumId)
    {
        return inventory.equippedDrumIds.Contains(drumId);
    }

    public bool EquipDrum(DrumData drumId)
    {
        if (!HasDrum(drumId))
        {
            Debug.LogWarning($"Cannot equip drum '{drumId}' because it is not owned.");
            return false;
        }

        if (IsEquipped(drumId))
        {
            Debug.LogWarning($"Drum '{drumId}' is already equipped.");
            return false;
        }

        if (inventory.equippedDrumIds.Count >= maxEquippedDrums)
        {
            Debug.LogWarning($"Cannot equip more than {maxEquippedDrums} drums.");
            return false;
        }

        inventory.equippedDrumIds.Add(drumId);

        if (DrumLoadoutSpawner.Instance != null)
            DrumLoadoutSpawner.Instance.SpawnDrum(drumId);

        Debug.Log($"Equipped drum: {drumId}");
        return true;
    }

    public bool UnequipDrum(DrumData drumId)
    {
        if (!IsEquipped(drumId))
        {
            Debug.LogWarning($"Drum '{drumId}' is not equipped.");
            return false;
        }

        inventory.equippedDrumIds.Remove(drumId);
        if (DrumLoadoutSpawner.Instance != null)
            DrumLoadoutSpawner.Instance.DeSpawnDrum(drumId);

        Debug.Log($"Unequipped drum: {drumId}");
        return true;
    }

    public List<DrumData> GetEquippedDrums()
    {
        return inventory.equippedDrumIds;
    }

    public PlayerInventory GetInventory()
    {
        return inventory;
    }
    
    void OnEnable()
    {
        GlobalEventAsset.Instance.StartListening<RequestSceneLoadEvent>(ClearEquipped);
    }

    void OnDisable()
    {
        GlobalEventAsset.Instance.StopListening<RequestSceneLoadEvent>(ClearEquipped);
    }
    
    private void ClearEquipped()
    {
        DrumLoadoutSpawner.Instance.ClearSpawnedDrums();
        GetInventory().equippedDrumIds.Clear();
    }
}