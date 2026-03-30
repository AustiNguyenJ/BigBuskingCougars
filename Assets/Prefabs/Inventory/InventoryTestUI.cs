//using System.Text;
//using TMPro;
//using UnityEngine;

//public class InventoryTestUI : MonoBehaviour
//{
//    [SerializeField] private TextMeshProUGUI ownedDrumsText;

//    void Start()
//    {
//        RefreshUI();
//    }

//    public void AddDrum(string drumId)
//    {
//        InventoryManager.Instance.AddDrum(drumId);
//        RefreshUI();
//    }

//    public void EquipDrum(string drumId)
//    {
//        InventoryManager.Instance.EquipDrum(drumId);
//        RefreshUI();
//    }

//    public void RefreshUI()
//    {
//        if (ownedDrumsText == null)
//            return;

//        PlayerInventory inventory = InventoryManager.Instance.GetInventory();

//        StringBuilder sb = new StringBuilder();
//        sb.AppendLine("Owned Drums:");

//        foreach (string drumId in inventory.ownedDrumIds)
//        {
//            string equippedTag = inventory.equippedDrumIds.Contains(drumId) ? " [EQUIPPED]" : "";
//            sb.AppendLine("- " + drumId + equippedTag);
//        }

//        if (inventory.ownedDrumIds.Count == 0)
//            sb.AppendLine("None");

//        ownedDrumsText.text = sb.ToString();
//    }
//}