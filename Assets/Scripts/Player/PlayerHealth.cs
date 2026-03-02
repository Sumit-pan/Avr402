using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    
    public int maxHealth = 100;
    public int currentHealth = 100;

    [Header("End UI")]
   
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

    UIManager.Instance?.ShowMessage("You died"); // optional

    if (deathUI)
        deathUI.ShowDeath();
    else
        Debug.LogWarning("PlayerHealth: deathUI not assigned (Ending scene is handling UI).");
}
}