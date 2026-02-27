using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepAudio : MonoBehaviour
{
    [Header("Clips")]
    public AudioClip[] footstepClips;

    [Header("Tuning")]
    public float walkStepInterval = 0.5f;
    public float runStepInterval = 0.35f;
    public float minMoveSpeed = 0.15f;

    [Header("Ground Check")]
    public Transform groundCheck;          
    public float groundCheckRadius = 0.25f;
    public LayerMask groundMask = ~0;

    [Header("Refs (optional)")]
    public CharacterController characterController;
    public Rigidbody rb;

    AudioSource audioSource;
    float nextStepTime;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (!characterController) characterController = GetComponent<CharacterController>();
        if (!rb) rb = GetComponent<Rigidbody>();
        if (!groundCheck) groundCheck = transform;
    }

    void Update()
    {
        if (!IsGrounded()) return;

        float speed = GetSpeed();
        if (speed < minMoveSpeed) return;

       
        bool running = Input.GetKey(KeyCode.LeftShift);

        float interval = running ? runStepInterval : walkStepInterval;

        if (Time.time >= nextStepTime)
        {
            PlayStep();
            nextStepTime = Time.time + interval;
        }
    }

    bool IsGrounded()
    {
        if (characterController) return characterController.isGrounded;
        return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask, QueryTriggerInteraction.Ignore);
    }

    float GetSpeed()
    {
        if (characterController) return characterController.velocity.magnitude;
        if (rb) return rb.velocity.magnitude;
        return 0f;
    }

    void PlayStep()
    {
        if (footstepClips == null || footstepClips.Length == 0) return;

        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.PlayOneShot(clip);
    }
}