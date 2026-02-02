using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public Door door;
    public float spinSpeed = 120f;
    private AudioSource audioSrc;
    private bool picked;

    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        // spin for visual feedback
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (picked) return;
        if (!other.CompareTag("Player")) return;

        picked = true;

        if (GameManager.Instance != null)
            GameManager.Instance.GetKey();

        // optional: you can also unlock door immediately, or require the player to interact at the door
        if (door != null)
            door.Unlock(); // door becomes unlockable/openable

        if (audioSrc != null && audioSrc.clip != null)
        {
            audioSrc.Play();
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject, audioSrc.clip.length);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
