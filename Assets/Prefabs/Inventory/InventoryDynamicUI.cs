using System.Collections.Generic;
using UnityEngine;

public class InventoryDynamicUI : MonoBehaviour
{
    [SerializeField] private Transform allDrumsContainer;
    [SerializeField] private Transform ownedDrumsContainer;
    [SerializeField] private GameObject rowPrefab;

    void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        ClearContainer(allDrumsContainer);
        ClearContainer(ownedDrumsContainer);

        BuildAllDrumsPanel();
        BuildOwnedDrumsPanel();
    }

    void BuildAllDrumsPanel()
    {
        List<DrumData> allDrums = DrumDatabase.Instance.GetAllDrums();

        foreach (DrumData drum in allDrums)
        {
            if (drum == null) continue;

            GameObject rowObj = Instantiate(rowPrefab, allDrumsContainer);
            InventoryRowUI row = rowObj.GetComponent<InventoryRowUI>();

            string display = $"{drum.drumId} ({drum.rarity})";

            row.Setup(
                display,
                "Add",
                () =>
                {
                    InventoryManager.Instance.AddDrum(drum.drumId);
                    RefreshUI();
                },
                "Remove",
                () =>
                {
                    InventoryManager.Instance.RemoveDrum(drum.drumId);
                    RefreshUI();
                }
            );
        }
    }

    void BuildOwnedDrumsPanel()
    {
        PlayerInventory inventory = InventoryManager.Instance.GetInventory();

        foreach (string drumId in inventory.ownedDrumIds)
        {
            GameObject rowObj = Instantiate(rowPrefab, ownedDrumsContainer);
            InventoryRowUI row = rowObj.GetComponent<InventoryRowUI>();

            bool equipped = inventory.equippedDrumIds.Contains(drumId);
            string display = equipped ? $"{drumId} [EQUIPPED]" : drumId;

            row.Setup(
                display,
                equipped ? "Unequip" : "Equip",
                () =>
                {
                    if (equipped)
                        InventoryManager.Instance.UnequipDrum(drumId);
                    else
                        InventoryManager.Instance.EquipDrum(drumId);

                    RefreshUI();
                },
                "Remove",
                () =>
                {
                    if (equipped)
                        InventoryManager.Instance.UnequipDrum(drumId);
                    InventoryManager.Instance.RemoveDrum(drumId);
                    RefreshUI();
                }
            );
        }
    }

    void ClearContainer(Transform container)
    {
        if (container == null) return;

        for (int i = container.childCount - 1; i >= 0; i--)
        {
            Destroy(container.GetChild(i).gameObject);
        }
    }
}