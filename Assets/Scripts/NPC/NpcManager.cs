using System;
using System.Collections.Generic;
using Events.Npc;
using UnityEngine;
using Random = UnityEngine.Random;


[Serializable]
public struct NpcRoute
{
    public Transform spawnPoint;
    public Transform endPoint;
}


public class NpcManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject npcPrefab;
    [Tooltip("(Start/SpawnPoint)   (EndGoalPoint)")]
    [SerializeField] List<NpcRoute> availableNpcRoutes;

    [Header("Settings")] 
    [Tooltip("Interval is in Seconds")]
    public float spawnInterval;
    public int maxNumOfActiveNpcs;

    public List<GameObject> currentActiveNpcs = new List<GameObject>();
    // runtime values
    float timer;
    int currentNumOfActiveNpcs => currentActiveNpcs.Count;
    
    void OnEnable()
    {
        GlobalEventAsset.Instance.StartListening<OnNpcSpawned>(OnNpcSpawned);
        GlobalEventAsset.Instance.StartListening<OnNpcReachedEndPoint>(DespawnNpc);   
    }

    void OnDisable()
    {
        GlobalEventAsset.Instance.StopListening<OnNpcSpawned>(OnNpcSpawned);
        GlobalEventAsset.Instance.StopListening<OnNpcReachedEndPoint>(DespawnNpc);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            if (TrySpawningNpc())
                timer = 0f;
        }
    }

    bool TrySpawningNpc()
    {
        if (currentNumOfActiveNpcs < maxNumOfActiveNpcs)
        {
            // choose random route
            NpcRoute route = availableNpcRoutes[Random.Range(0, availableNpcRoutes.Count)];
            if (Validate.AnyNull(route, route.spawnPoint, route.endPoint)) return false;
            
            // spawn at route start point and assign the end point to npc instance
            NpcScript npcScript = Instantiate(npcPrefab, route.spawnPoint.position, Quaternion.identity).GetComponent<NpcScript>();
            npcScript.Initialize(route.endPoint);
            RegisterNpc(npcScript.gameObject);
            return true;
        }
        return false;
    }

    void OnNpcSpawned(OnNpcSpawned data) => RegisterNpc(data.npcObject);
    void RegisterNpc(GameObject npc)
    {
        if (currentActiveNpcs.Contains(npc)) return;
        currentActiveNpcs.Add(npc);
    }

    void DespawnNpc(OnNpcReachedEndPoint data)
    {
        if (!currentActiveNpcs.Contains(data.npcObject)) return;
        
        currentActiveNpcs.Remove(data.npcObject);
        Destroy(data.npcObject);
    }
}