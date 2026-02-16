using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPPController : MonoBehaviour
{
    public Transform cameraPivot;
    public float walkSpeed = 4f;
    public float sprintSpeed = 6.5f;
    public float gravity = -20f;

    CharacterController cc;
    Vector3 vel;

    void Awake() => cc = GetComponent<CharacterController>();

    void Update()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = cameraPivot.forward; forward.y = 0;
        Vector3 right = cameraPivot.right; right.y = 0;
        forward.Normalize(); right.Normalize();

        Vector3 move = Vector3.ClampMagnitude(forward * v + right * h, 1f);
        cc.Move(move * speed * Time.deltaTime);

        if (cc.isGrounded && vel.y < 0) vel.y = -2f;
        vel.y += gravity * Time.deltaTime;
        cc.Move(vel * Time.deltaTime);
    }
}
