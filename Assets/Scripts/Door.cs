using UnityEngine;

public class Door : MonoBehaviour
{
    public bool unlocked;
    public float openAngle = 90f;
    public float openSpeed = 2.5f;

    private bool opening;
    private Quaternion closedRot;
    private Quaternion openRot;

    void Start()
    {
        closedRot = transform.rotation;
        openRot = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, openAngle, 0f));
    }

    public void Unlock()
    {
        unlocked = true;
        Debug.Log("Door unlocked!");
    }

    public void TryOpen()
    {
        if (!unlocked) { Debug.Log("Door is locked."); return; }
        opening = true;
        if (GameManager.Instance != null) GameManager.Instance.UpdateUI("Door opened! Go to the exit.");
    }

    void Update()
    {
        if (!opening) return;
        transform.rotation = Quaternion.Slerp(transform.rotation, openRot, openSpeed * Time.deltaTime);
    }
}
