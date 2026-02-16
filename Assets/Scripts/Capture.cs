using UnityEngine;

public class CaptureTrigger : MonoBehaviour
{
    public float delay = 1.2f;
    bool triggered;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;

        // Freeze movement for a moment
        var cc = other.GetComponent<CharacterController>();
        if (cc) cc.enabled = false;

        Invoke(nameof(DoCapture), delay);
        Debug.Log("CAPTURE TRIGGER HIT: " + other.name);

    }

    void DoCapture()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.Capture();
    }
}
