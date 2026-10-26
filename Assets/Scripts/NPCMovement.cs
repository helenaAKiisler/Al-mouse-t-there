using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float followDistance = 2f;

    private NavMeshAgent agent;

    public bool HasArrived =>
        agent.isOnNavMesh && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (!agent.isOnNavMesh)
            Debug.LogWarning("NPC is not on a baked NavMesh, it cannot move.", this);
    }

    public void MoveTo(Vector3 position)
    {
        if (agent.isOnNavMesh)
            agent.SetDestination(position);
    }
    
    public void Follow(Transform target)
    {
        if (target == null || !agent.isOnNavMesh) return;

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > followDistance)
            agent.SetDestination(target.position);
        else
            agent.ResetPath();
    }
}