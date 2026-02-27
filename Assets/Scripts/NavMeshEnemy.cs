using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] points;
    public float pointReachDistance = 0.6f;
    public float waitTimeAtPoint = 1f;
    public bool loop = true;

    NavMeshAgent agent;
    int index = 0;
    float waitTimer = 0f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

 void Start()
{
  
    if (Physics.Raycast(transform.position + Vector3.up * 2f, Vector3.down, out var hit, 10f))
        transform.position = hit.point;

    
    if (points == null || points.Length == 0) return;
    if (!agent || !agent.enabled || !agent.isOnNavMesh) return;

    agent.SetDestination(points[index].position);
}

  void Update()
{
    if (points == null || points.Length == 0) return;
    if (agent == null || !agent.enabled) return;

  
    if (!agent.isOnNavMesh) return;

    if (agent.pathPending) return;

    if (agent.remainingDistance <= pointReachDistance)
    {
        waitTimer += Time.deltaTime;
        if (waitTimer >= waitTimeAtPoint)
        {
            waitTimer = 0f;
            GoNext();
        }
    }
}

    void GoNext()
    {
        if (points.Length == 0) return;

        index++;

        if (index >= points.Length)
        {
            if (loop) index = 0;
            else { enabled = false; return; }
        }

        agent.SetDestination(points[index].position);
    }
}