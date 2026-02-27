using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    public int hp = 100;

    [Header("Death")]
    public Animator animator;              
    public string dieTrigger = "Die";      
    public NavMeshAgent agent;             
    public MonoBehaviour[] disableOnDeath; 
    public Collider[] collidersToDisable;  
    public float destroyAfter = 6f;

    bool dead;

    void Awake()
    {
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!animator) animator = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(int dmg)
    {
        if (dead) return;

        hp -= dmg;

        if (hp <= 0)
        {
            hp = 0;
            Die();
        }
    }

    void Die()
    {
        dead = true;

      
        if (agent)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        
        if (animator && !string.IsNullOrEmpty(dieTrigger))
            animator.SetTrigger(dieTrigger);

        
        if (disableOnDeath != null)
            foreach (var b in disableOnDeath)
                if (b) b.enabled = false;

        
        if (collidersToDisable != null)
            foreach (var c in collidersToDisable)
                if (c) c.enabled = false;

        Destroy(gameObject, destroyAfter);
        Debug.Log("the ghoul is dead.");
    }
}