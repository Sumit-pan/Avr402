using UnityEngine;

public class FlashlightToggle : MonoBehaviour
{
    Light l;

    void Awake()
    {
        l = GetComponent<Light>();
        l.enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            l.enabled = !l.enabled;
    }
}
