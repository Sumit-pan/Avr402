using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string basementSceneName = "Basement";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Capture()
    {
        SceneManager.LoadScene(basementSceneName);
        Debug.Log("Loading scene: " + basementSceneName);

    }
}
