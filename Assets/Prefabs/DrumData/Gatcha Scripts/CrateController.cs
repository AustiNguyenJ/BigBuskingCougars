using UnityEngine;

public class CrateController : MonoBehaviour
{
    public CrateOpener crateOpener;
    public GameObject cratePrefab;
    public Transform crateSpawnPoint;

    public void OpenCrate(CrateData crate)
    {
        if (crate == null || crateOpener == null) return;

        // 1️⃣ Get drum to give
        DrumData droppedDrum = crateOpener.OpenCrate(crate);
        if (droppedDrum == null) return;

        // 2️⃣ Add drum to inventory
        InventoryManager.Instance.AddDrum(droppedDrum.drumId);

        // 3️⃣ Spawn crate prefab
        GameObject crateObj = Instantiate(cratePrefab, crateSpawnPoint.position, crateSpawnPoint.rotation);

        // 4️⃣ Get the Crate3DController from the spawned crate
        Crate3DController crate3D = crateObj.GetComponent<Crate3DController>();

        // 5️⃣ Open crate (shake, break, reveal drum)
        crate3D.OpenCrate(droppedDrum);

        Debug.Log($"You received: {droppedDrum.name} ({droppedDrum.rarity})");
    }
}