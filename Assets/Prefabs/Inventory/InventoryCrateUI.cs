using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryCrateUI : MonoBehaviour
{
    [SerializeField] private Transform crateContainer;
    [SerializeField] private GameObject crateRowPrefab;
    [SerializeField] private List<CrateData> availableCrates;
    [SerializeField] private CrateController crateController;

    void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        ClearContainer(crateContainer);

        foreach (CrateData crate in availableCrates)
        {
            if (crate == null) continue;

            GameObject rowObj = Instantiate(crateRowPrefab, crateContainer);
            CrateRowUI row = rowObj.GetComponent<CrateRowUI>();

            row.Setup(crate.displayName, "Open", () =>
            {
                crateController.OpenCrate(crate);
                // Optionally refresh inventory UI
                InventoryDynamicUI dynamicUI = FindObjectOfType<InventoryDynamicUI>();
                if (dynamicUI != null) dynamicUI.RefreshUI();
            });
        }
    }

    void ClearContainer(Transform container)
    {
        if (container == null) return;

        for (int i = container.childCount - 1; i >= 0; i--)
            Destroy(container.GetChild(i).gameObject);
    }
}