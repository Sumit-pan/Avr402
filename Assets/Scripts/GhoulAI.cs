using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class GhoulAI : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform attackPoint; 

    [Header("Detection")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float chaseRange = 10f;
    [SerializeField] private float attackRange = 1.8f;

    [Header("Attack")]
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackCooldown = 1.2f;

    [Header("Movement")]
    [SerializeField] private float rotateToTargetSpeed = 12f; 

    private Transform player;
    private PlayerHealth playerHealth;
    private float nextAttackTime;
    private bool wasChasing;

   
    private const string PARAM_SPEED = "Speed";
    private const string PARAM_ISCHASING = "IsChasing";
    private const string TRIG_ATTACK = "Attack";
    public MonsterAudio monsterAudio;

    private void Awake()
    {
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!animator) animator = GetComponentInChildren<Animator>();
        if (!monsterAudio) monsterAudio = GetComponent<MonsterAudio>();
        
        agent.updateRotation = true;
        agent.updatePosition = true;

       
        if (attackPoint == null) attackPoint = transform;
    }

    private void Start()
{
    GameObject p = GameObject.FindGameObjectWithTag(playerTag);
    if (!p)
    {
        Debug.LogError($"GhoulAI: No GameObject found with tag '{playerTag}'.");
        enabled = false;
        return;
    }

    player = p.transform;

    // Try to find PlayerHealth anywhere on that player object
    playerHealth =
        p.GetComponent<PlayerHealth>() ??
        p.GetComponentInParent<PlayerHealth>() ??
        p.GetComponentInChildren<PlayerHealth>();

    if (!playerHealth)
    {
        Debug.LogError("GhoulAI: PlayerHealth component NOT found on Player (or parent/child).");
        // Optional: disable attacking so it doesn't spam warnings
        // enabled = false;
        return;
    }

    // Optional: start idle loop once
    monsterAudio?.PlayIdle();
}

    private void Update()
    {
        if (!agent || !agent.enabled || !agent.isOnNavMesh) return;
        if (!player) return;

        float directDist = Vector3.Distance(attackPoint.position, player.position);

        bool chasing = directDist <= chaseRange;

        // Animator updates
        if (animator)
        {
            animator.SetFloat(PARAM_SPEED, agent.velocity.magnitude);
            animator.SetBool(PARAM_ISCHASING, chasing);
            
        }

        if (!chasing)
        {
          
            agent.isStopped = false;
            
            return;
        }

        
        agent.stoppingDistance = attackRange;
        agent.isStopped = false;
        agent.SetDestination(player.position);

      
        bool inAttackRange =
            !agent.pathPending &&
            agent.pathStatus == NavMeshPathStatus.PathComplete &&
            agent.remainingDistance <= agent.stoppingDistance + 0.05f;

       
        if (inAttackRange)
        {
            agent.isStopped = true;
            FaceTargetSmooth(player.position);

            TryAttack();
        }
        if (chasing && !wasChasing) monsterAudio?.PlayChase();
if (!chasing && wasChasing) monsterAudio?.PlayIdle();
wasChasing = chasing;
    }

    private void TryAttack()
    {
        if (Time.time < nextAttackTime) return;


        nextAttackTime = Time.time + attackCooldown;

        Debug.Log($"GhoulAI ATTACK FIRED time={Time.time:F2}");

        if (animator)
        {
            animator.ResetTrigger(TRIG_ATTACK);
            animator.SetTrigger(TRIG_ATTACK);
        }
        else
        {
            Debug.LogWarning("GhoulAI: Animator reference missing (attack animation won't play).");
        }

        if (playerHealth)
        {
            playerHealth.TakeDamage(damage);
            monsterAudio?.PlayAttack();
    monsterAudio?.PlayJumpscare();
    CameraShake.Instance?.Shake(0.25f, 0.25f, 25);
            Debug.Log($"GhoulAI dealt {damage} damage. Player HP now {playerHealth.currentHealth}");
        }
        else
        {
            Debug.LogWarning("GhoulAI: playerHealth is NULL, cannot deal damage.");
        }
    }

    private void FaceTargetSmooth(Vector3 targetPos)
    {
        Vector3 dir = (targetPos - transform.position);
        dir.y = 0f;

        if (dir.sqrMagnitude < 0.0001f) return;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateToTargetSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        if (attackPoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(attackPoint.position, 0.1f);
        }
    }
}