using UnityEngine;

public class EndDoorJumpscare : MonoBehaviour
{
    [Header("Spawn")]
    public GameObject scarePrefab;    
    public Transform spawnPoint;       
    public float despawnAfter = 3f;

    [Header("Audio")]
    public AudioSource audioSource;   
    public AudioClip scareSfx;

    [Header("Camera")]
    public CameraShake cameraShake;   
    public float shakeDuration = 0.35f;
    public float shakeIntensity = 0.18f;

    bool triggered;

    void Awake()
    {
        if (!audioSource) audioSource = GetComponentInChildren<AudioSource>();
        if (!cameraShake)
        {
            Camera cam = Camera.main;
            if (cam) cameraShake = cam.GetComponent<CameraShake>();
        }
    }

    public void TriggerScare()
    {
        if (triggered) return;
        triggered = true;

        if (audioSource && scareSfx) audioSource.PlayOneShot(scareSfx);

        if (scarePrefab && spawnPoint)
        {
            GameObject obj = Instantiate(scarePrefab, spawnPoint.position, spawnPoint.rotation);
            Destroy(obj, despawnAfter);
        }

        if (cameraShake != null)
            StartCoroutine(cameraShake.Shake(shakeDuration, shakeIntensity));
    }
}