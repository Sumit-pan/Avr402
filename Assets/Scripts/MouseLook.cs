using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody; // drag Player here
    public float sensitivity = 120f;
    public float pitchMin = -60f;
    public float pitchMax = 70f;

    private float pitch;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // yaw: rotate player left/right
        playerBody.Rotate(Vector3.up * mouseX);

        // pitch: rotate camera pivot up/down
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
