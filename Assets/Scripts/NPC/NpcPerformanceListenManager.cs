using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.AI;

public class NpcPerformanceListenManager : MonoBehaviour
{
    public static NpcPerformanceListenManager Instance;

    [SerializeField] NpcManager npcManager;
    public Transform playerArea;

    [Header("Viewing Area Settings")]
    public float innerRadius = 3f;
    public float outerRadius = 7f;
    public float listeningRange = 20f;
    
    public List<NpcScript> activeListeners = new List<NpcScript>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Assert.IsNotNull(npcManager, "npcManager != null");
        Assert.IsNotNull(playerArea, "playerArea != null");
    }

    void Update()
    {
        if (Validate.AnyNull(playerArea, npcManager)) return;

        CheckForNewListeners();
        UpdateActiveListeners();
    }

    void CheckForNewListeners()
    {
        foreach (GameObject npcObj in npcManager.currentActiveNpcs)
        {
            if (npcObj == null) continue;
            
            NpcScript npc = npcObj.GetComponent<NpcScript>();
            if (npc == null || npc.currentState == NpcScript.NpcState.Listening) continue;

            float distance = Vector3.Distance(playerArea.position, npc.transform.position);
            
            if (distance <= listeningRange && npc.hasListened == false)
            {
                if (TryGetListeningPosition(out Vector3 validPosition))
                {
                    npc.SetListeningState(validPosition, playerArea);
                    activeListeners.Add(npc);
                }
            }
        }
    }

    void UpdateActiveListeners()
    {
        for (int i = activeListeners.Count - 1; i >= 0; i--)
        {
            NpcScript npc = activeListeners[i];
            
            if (npc == null)
            {
                activeListeners.RemoveAt(i);
                continue;
            }

            bool isAtSpot = !npc.agent.isActiveAndEnabled || (!npc.agent.pathPending && npc.agent.remainingDistance <= npc.agent.stoppingDistance);

            if (isAtSpot)
            {
                npc.currentListeningTime += Time.deltaTime;

                if (npc.currentListeningTime >= npc.listeningDuration)
                {
                    npc.currentListeningTime = 0f;
                    npc.SetMovingState();
                    activeListeners.RemoveAt(i);
                }
            }
        }
    }

    public bool TryGetListeningPosition(out Vector3 validPosition)
    {
        validPosition = Vector3.zero;
        if (playerArea == null) return false;

        Vector2 randomDir = Random.insideUnitCircle.normalized;
        float randomDist = Random.Range(innerRadius, outerRadius);
        Vector3 targetPoint = playerArea.position + new Vector3(randomDir.x, 0, randomDir.y) * randomDist;

        if (NavMesh.SamplePosition(targetPoint, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            validPosition = hit.position;
            return true;
        }
        
        return false;
    }

    void OnDrawGizmos()
    {
        if (playerArea == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerArea.position, innerRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(playerArea.position, outerRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerArea.position, listeningRange);
    }
}