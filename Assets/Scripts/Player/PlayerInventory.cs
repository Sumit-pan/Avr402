using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasGun;
    public int ammo;

    public bool hasExitKey;

    public void AddAmmo(int amount) => ammo += amount;
}