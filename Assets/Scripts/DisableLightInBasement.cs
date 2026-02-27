using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableFlashlightInBasement : MonoBehaviour
{
    public GameObject flashlight;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Basement")
        {
            if (flashlight) flashlight.SetActive(false);
        }
    }
}
