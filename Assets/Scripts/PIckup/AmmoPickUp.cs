using UnityEngine;

public class AmmoPickupRay : MonoBehaviour, IInteractable
{
    public int ammoAmount = 10;

    public void Interact()
    {
        var inv = FindObjectOfType<PlayerInventory>();
        if (inv) inv.AddAmmo(ammoAmount);
        UIManager.Instance?.ShowMessage($"+{ammoAmount} bullets");

        gameObject.SetActive(false);
    }

    public string GetPrompt() => $"Pick up bullets (+{ammoAmount})";
}