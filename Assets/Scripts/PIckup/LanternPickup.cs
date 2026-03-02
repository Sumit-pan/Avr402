using UnityEngine;

public class LanternPickupRay : MonoBehaviour, IInteractable
{
    public GameObject lanternOnPlayer;
    bool picked;

    public void Interact()
{
    Debug.Log("Interact called"); // <- should appear no matter what

    if (picked) return;

    Debug.Log(UIManager.Instance == null ? "UIManager.Instance is NULL" : "UIManager.Instance OK");

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