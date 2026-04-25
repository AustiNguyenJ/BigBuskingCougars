using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor.Internal;

public class InventoryCrateUI : MonoBehaviour
{
    [SerializeField] private Transform crateContainer;
    [SerializeField] private GameObject crateRowPrefab;
    [SerializeField] private List<CrateData> availableCrates;
    [SerializeField] private CrateController crateController;
    
    private Coroutine refreshRoutine;
    [SerializeField] private float refreshDelay = 2f;
    
    private InventoryDynamicUI inventoryDynamicUI;
    
    void Start()
    {
        RequestRefresh();
        inventoryDynamicUI = FindObjectOfType<InventoryDynamicUI>();
    }

    public void RequestRefresh()
    {
        if (refreshRoutine != null)
            StopCoroutine(refreshRoutine);

        refreshRoutine = StartCoroutine(RefreshAfterDelay());
    }
    
    private IEnumerator RefreshAfterDelay()
    {
        yield return new WaitForSeconds(refreshDelay);
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

            row.Setup(crate.displayName, "Buy", $"Price: {crate.price}b", () =>
            {
                CrateResult result = crateController.OpenCrate(crate);
                HandleCrateResult(result, row, crate);
                RequestRefresh();
                inventoryDynamicUI.RefreshUI();
            });
        }
        
        
    }
    
    private void HandleCrateResult(CrateResult result, CrateRowUI row, CrateData crate)
    {
        switch (result)
        {
            case CrateResult.Error:
                Debug.LogError($"Price: {crate.price}b - Crate error.");
                row.ChangeDetails($"Price: {crate.price}b - Crate error.", Color.black);
                break;

            case CrateResult.Success:
                Debug.Log("Purchased!");
                row.ChangeDetails($"Price: {crate.price}b - Opening crate!", Color.greenYellow);
                break;

            case CrateResult.NotEnoughMoney:
                Debug.Log($"Price: {crate.price}b - Not enough money.");
                row.ChangeDetails($"Price: {crate.price}b - Not enough money.", Color.red);
                break;
        }
        
    }

    void ClearContainer(Transform container)
    {
        if (container == null) return;

        for (int i = container.childCount - 1; i >= 0; i--)
            Destroy(container.GetChild(i).gameObject);
    }
}