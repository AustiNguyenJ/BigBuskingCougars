using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryCrateUI : MonoBehaviour
{
    [SerializeField] private Transform crateContainer;
    [SerializeField] private GameObject crateRowPrefab;
    [SerializeField] private GameObject popup;
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

            row.Setup(crate.displayName, "Buy", () =>
            {
                CrateResult result = crateController.OpenCrate(crate);
                HandleCrateResult(result, row, crate);
            });
        }
    }
    
    private void HandleCrateResult(CrateResult result, CrateRowUI row, CrateData crate)
    {
        switch (result)
        {
            case CrateResult.Error:
                Debug.LogError("Crate error.");
                popup.GetComponent<UIPopup>().Show("Something went wrong.");
                break;

            case CrateResult.Success:
                popup.GetComponent<UIPopup>().Show("Crate.");
                Debug.Log("Purchased!");
                InventoryDynamicUI dynamicUI = FindObjectOfType<InventoryDynamicUI>();
                if (dynamicUI != null) dynamicUI.RefreshUI();
                break;

            case CrateResult.NotEnoughMoney:
                Debug.Log("Not enough money.");
                popup.GetComponent<UIPopup>().Show("Not enough Boings");
                break;
        }
        
        RefreshUI();
    }

    void ClearContainer(Transform container)
    {
        if (container == null) return;

        for (int i = container.childCount - 1; i >= 0; i--)
            Destroy(container.GetChild(i).gameObject);
    }
}