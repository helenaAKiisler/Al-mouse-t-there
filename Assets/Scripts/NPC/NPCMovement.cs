using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float followDistance = 2f;

    private NavMeshAgent agent;
    private Animator animator;

    public bool HasArrived =>
        agent.isOnNavMesh && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        if (!agent.isOnNavMesh)
            Debug.LogWarning("NPC is not on a baked NavMesh, it cannot move.", this);
        animator.SetBool("IsRunning", false);
    }

    public bool MoveTo(Vector3 position)
    {
        if (!agent.isOnNavMesh) return false;
        animator.SetBool("IsRunning", true);
        animator.SetFloat("Speed", agent.velocity.magnitude);
        return agent.SetDestination(position);
    }
    
    public void Follow(Transform target)
    {
        if (target == null || !agent.isOnNavMesh) return;

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > followDistance)
        {
            animator.SetBool("IsRunning", true);
            animator.SetFloat("Speed", agent.velocity.magnitude);
            agent.SetDestination(target.position);
        }
        else
        {
            animator.SetBool("IsRunning", false);
            animator.SetFloat("Speed", agent.velocity.magnitude);
            agent.ResetPath();
        }
    }
}