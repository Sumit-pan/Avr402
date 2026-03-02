using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossEndingChase : MonoBehaviour
{
    [Header("Refs")]
    public NavMeshAgent agent;
    public Animator animator;
    public Transform player;
    public PlayerHealth playerHealth;
    public DeathScreenManager deathUI;

    [Header("Chase/Attack")]
    public float attackWindup = 0.7f;
    public int killDamage = 9999;
    public float attackCooldown = 2.0f;

    [Header("Animator Params")]
    public string speedParam = "Speed";
    public string attackTrigger = "Attack";

    bool attacking;
    float nextAttackTime;

    public MonsterAudio monsterAudio;

    void Awake()
    {
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!animator) animator = GetComponentInChildren<Animator>();
        if (!monsterAudio) monsterAudio = GetComponent<MonsterAudio>();
    }

    void Start()
    {
        if (!player)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p) player = p.transform;
        }
        Debug.Log("Player found");
        monsterAudio?.PlayChase();
        

        if (!playerHealth && player)
            playerHealth = player.GetComponentInChildren<PlayerHealth>(true);

        if (agent) agent.isStopped = false;
    }

    void Update()
    {
        if (!player || !agent) return;

        if (!attacking)
            agent.SetDestination(player.position);

        if (animator && !string.IsNullOrEmpty(speedParam))
            animator.SetFloat(speedParam, agent.desiredVelocity.magnitude);

        // Attack ONLY when boss reaches the player
        if (!attacking && Time.time >= nextAttackTime && !agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance + 0.25f)
            {
                Debug.Log("Attack");
                StartCoroutine(AttackRoutine());
            }
        }
    }

    IEnumerator AttackRoutine()
    {
        if (attacking) yield break;
        attacking = true;

        nextAttackTime = Time.time + attackCooldown;
        agent.isStopped = true;

        if (animator && !string.IsNullOrEmpty(attackTrigger))
            animator.SetTrigger(attackTrigger);

        yield return new WaitForSecondsRealtime(attackWindup);

        if (playerHealth) playerHealth.TakeDamage(killDamage);
        
        if (deathUI) deathUI.ShowDeath();
        Debug.Log("About to damage: " + (playerHealth ? playerHealth.name : "NULL PlayerHealth"));Debug.Log("Player damaged");
        
    }

    public void TryAttackNow()
{
    if (attacking) return;
    if (Time.time < nextAttackTime) return;
    StartCoroutine(AttackRoutine());
    monsterAudio?.PlayAttack();
monsterAudio?.PlayJumpscare();
CameraShake.Instance?.Shake(0.3f, 0.35f, 30);

}
}