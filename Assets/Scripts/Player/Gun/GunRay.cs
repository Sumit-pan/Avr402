using UnityEngine;

public class GunPickupRay : MonoBehaviour, IInteractable
{
    public GameObject gunInHand; 

    bool picked;

    public void Interact()
    {
        var inv = FindObjectOfType<PlayerInventory>();
        if (inv) inv.hasGun = true;

        if (picked) return;
        picked = true;

        if (gunInHand) gunInHand.SetActive(true);

        gameObject.SetActive(false);
         UIManager.Instance?.ShowMessage("Picked up Gun");
    }
    public string GetPrompt()
{
    return "Pick up Gun";
}
}