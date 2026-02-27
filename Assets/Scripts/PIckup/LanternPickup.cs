using UnityEngine;

public class LanternPickupRay : MonoBehaviour, IInteractable
{
    public GameObject lanternOnPlayer;
    bool picked;

    public void Interact()
    {
        if (picked) return;
        UIManager.Instance?.ShowMessage("Picked up lantern");
        picked = true;

        if (lanternOnPlayer)
            lanternOnPlayer.SetActive(true);

        gameObject.SetActive(false);
    }

    public string GetPrompt()
    {
        return "Pick up Lantern";
    }
}