using Events.Npc;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(Animator))]
public class NpcScript : MonoBehaviour
{
    public enum NpcState
    {
        Moving,
        Listening
    }

    [Header("References")] 
    public NpcSettings settings;
    Animator animator;
    
    public NavMeshAgent agent { get; set; }
    NavMeshObstacle obstacle;
    public NpcState currentState { get; set; }
    public Transform mainEndGoal { get; set; }
    public Transform currentGoal { get; set; }
    public Transform lookTarget { get; set; }
    
    public float listeningDuration { get; set; }
    public float currentListeningTime { get; set; }
    public bool hasListened { get; set; }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        animator = GetComponent<Animator>();
        
        if (Validate.AnyNull(settings))
        {
            Debug.LogError("Ensure Npc has a reference to an Npc settings scriptable object");
            return;
        }
        
        agent.avoidancePriority = Random.Range(30, 70);
        listeningDuration = Random.Range(settings.listeningDuration.x, settings.listeningDuration.y);
        currentState = NpcState.Moving;
    }

    void Start()
    {
        GlobalEventAsset.Instance.TriggerEvent(new OnNpcSpawned { npcObject = gameObject });
    }

    public void Initialize(Transform endGoal)
    {
        mainEndGoal = endGoal;
        SetMovingState();
    }

    public void SetListeningState(Vector3 viewingSpot, Transform targetToLookAt)
    {
        hasListened = true;
        currentState = NpcState.Listening;
        currentGoal = null;
        lookTarget = targetToLookAt;
        
        obstacle.enabled = false;
        agent.enabled = true;
        agent.SetDestination(viewingSpot);
    }

    public void SetMovingState()
    {
        currentState = NpcState.Moving;
        currentGoal = mainEndGoal;
        lookTarget = null;
        
        obstacle.enabled = false;
        agent.enabled = true;
        
        if (mainEndGoal != null)
        {
            agent.SetDestination(mainEndGoal.position);
        }
    }

    void Update()
    {
        if (agent.enabled)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
        
        if (currentState == NpcState.Listening && lookTarget != null)
        {
            if (agent.enabled && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.enabled = false;
                obstacle.enabled = true;
            }

            if (obstacle.enabled)
            {
                Vector3 direction = (lookTarget.position - transform.position).normalized;
                direction.y = 0f; 
                
                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
                }
            }
        }

        if (Validate.AnyNull(currentGoal, false)) return;

        if (currentState == NpcState.Moving && currentGoal == mainEndGoal)
        {
            if (Vector3.Distance(transform.position, currentGoal.position) < 1f)
            {
                GlobalEventAsset.Instance.TriggerEvent(new OnNpcReachedEndPoint { npcObject = gameObject });
            }
        }
    }
}