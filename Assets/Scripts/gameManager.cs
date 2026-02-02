using UnityEngine;
using TMPro;

public class PauseCursor : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool locked = Cursor.lockState == CursorLockMode.Locked;
            Cursor.lockState = locked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = !locked;
        }
    }
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text statusText;
    public bool hasKey;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        UpdateUI("Find the key.");
    }

    public void GetKey()
    {
        hasKey = true;
        UpdateUI("Key found! Go to the door.");
    }

    public void Win()
    {
        UpdateUI("YOU WIN! Press R to Restart.");
    }

    public void UpdateUI(string objective)
    {
        if (statusText == null) return;
        statusText.text = $"Key: {(hasKey ? "YES" : "NO")}\n{objective}";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
            );
    }
}
