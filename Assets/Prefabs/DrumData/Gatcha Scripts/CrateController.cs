using UnityEngine;

public enum CrateResult
{
    Error,
    Success,
    NotEnoughMoney
}
public class CrateController : MonoBehaviour
{
    public CrateOpener crateOpener;
    public GameObject cratePrefab;
    public Transform crateSpawnPoint;
    public PlayerData player;
    public ScoringManager ScoringManager;
    
    void Awake()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.GetComponent<PlayerData>();
        ScoringManager = FindObjectOfType<ScoringManager>();
    }
    
    public CrateResult OpenCrate(CrateData crate)
    {
        if (crate == null || crateOpener == null) 
            return CrateResult.Error;

        // 1️⃣ Get drum to give
        DrumData droppedDrum = crateOpener.OpenCrate(crate);
        if (droppedDrum == null) 
            return CrateResult.Error;
        
        //Spend Money
        if (player.CheckMoney() < crate.price)
            return CrateResult.NotEnoughMoney;
        player.SpendMoney(crate.price);
        ScoringManager.UpdateScoreUI();
        
        // 2️⃣ Add drum to inventory
        InventoryManager.Instance.AddDrum(droppedDrum);

        // 3️⃣ Spawn crate prefab
        GameObject crateObj = Instantiate(cratePrefab, crateSpawnPoint.position, crateSpawnPoint.rotation);

        // 4️⃣ Get the Crate3DController from the spawned crate
        Crate3DController crate3D = crateObj.GetComponent<Crate3DController>();

        // 5️⃣ Open crate (shake, break, reveal drum)
        crate3D.OpenCrate(droppedDrum);

        Debug.Log($"You received: {droppedDrum.name} ({droppedDrum.rarity})");

        return CrateResult.Success;
    }
}