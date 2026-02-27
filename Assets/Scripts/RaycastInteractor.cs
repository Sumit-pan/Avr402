using UnityEngine;

public class RaycastInteractor : MonoBehaviour
{
    public Camera cam;
    public float range = 3f;
    public KeyCode interactKey = KeyCode.E;
    public LayerMask interactMask;

    IInteractable current;

    void Awake()
    {
        if (!cam) cam = Camera.main;
    }

    void Update()
    {
        current = null;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * range, Color.green);

        if (Physics.Raycast(ray, out RaycastHit hit, range, interactMask))
        {
            current = hit.collider.GetComponentInParent<IInteractable>();
        }

        if (current != null)
        {
            UIManager.Instance?.ShowPrompt($"Press E  •  {current.GetPrompt()}");

            if (Input.GetKeyDown(interactKey))
                current.Interact();
        }
        else
        {
            UIManager.Instance?.HidePrompt();
        }
    }
}