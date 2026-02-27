using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public Camera cam;
    public float range = 3f;
    public KeyCode interactKey = KeyCode.E;

    void Awake()
    {
        if (!cam) cam = Camera.main;
    }

    void Update()
    {
        if (!Input.GetKeyDown(interactKey)) return;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            var interactable = hit.collider.GetComponentInParent<IInteractable>();
            interactable?.Interact();
        }
    }
}
