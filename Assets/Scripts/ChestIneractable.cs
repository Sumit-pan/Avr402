using UnityEngine;

public class ChestInteractable : MonoBehaviour, IInteractable
{
    public enum LootType { Ammo, Health, Key }

    public LootType loot = LootType.Ammo;

    [Header("Loot Amounts")]
    public int ammoAmount = 10;
    public int healAmount = 25;

    [Header("Optional")]
    public Animator animator;         
    public AudioSource audioSource;
    public AudioClip openSfx;

    bool opened;

    public void Interact()
    {
        if (opened) return;
        opened = true;


        if (animator)
            animator.SetTrigger("Open");

        if (audioSource && openSfx) audioSource.PlayOneShot(openSfx);

        var inv = FindObjectOfType<PlayerInventory>();
        var hp  = FindObjectOfType<PlayerHealth>();

        switch (loot)
        {
            case LootType.Ammo:
                if (inv) inv.AddAmmo(ammoAmount);
                 UIManager.Instance?.ShowMessage($"+{ammoAmount} Bullets");
                break;

            case LootType.Health:
                if (hp) hp.Heal(healAmount);
                UIManager.Instance?.ShowMessage($"+{healAmount} health");
                break;

            case LootType.Key:
                if (inv) inv.hasExitKey = true;
                UIManager.Instance?.ShowMessage("You found the exit key!");
                break;
        }

        var col = GetComponent<Collider>();
        if (col) col.enabled = false;
    }

    public string GetPrompt() => opened ? null : "Open chest";
    
}