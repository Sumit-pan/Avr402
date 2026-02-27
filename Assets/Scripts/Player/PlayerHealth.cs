using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    
    public int maxHealth = 100;
    public int currentHealth = 100;

    [Header("End UI")]
    // public EndScreenManager endUI;
    public DeathScreenManager deathUI;
    bool isDead;

    void Start()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UIManager.Instance?.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UIManager.Instance?.UpdateHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UIManager.Instance?.UpdateHealth(currentHealth, maxHealth);
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;
        deathUI.ShowDeath();
        UIManager.Instance?.ShowMessage("You died");

        // if (endUI) endUI.Show(EndState.Death);
        // else Debug.LogWarning("PlayerHealth: endUI not assigned!");
    }
}