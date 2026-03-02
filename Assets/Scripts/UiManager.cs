using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}

    [Header("Interact Prompt")]
    public GameObject promptRoot;
    public TextMeshProUGUI promptText;

    [Header("Messages")]
    public TextMeshProUGUI messageText;
    private Coroutine msgRoutine;

    [Header("Health UI")]
    public Image healthFill;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (promptRoot) promptRoot.SetActive(false);
        if (messageText) messageText.text = "";
    }

    public void UpdateHealth(int current, int max)
    {
        if (!healthFill) return;
        healthFill.fillAmount = (max <= 0) ? 0f : (float)current / max;
    }

    public void ShowPrompt(string text)
    {
        if (!promptRoot || !promptText) return;
        promptRoot.SetActive(true);
        promptText.text = text;
    }

    public void HidePrompt()
    {
        if (!promptRoot) return;
        promptRoot.SetActive(false);
    }

    public void ShowMessage(string text, float seconds = 2f)
    {
        Debug.Log(text);

        if (!messageText) return;

        if (!messageText.gameObject.activeSelf)
        messageText.gameObject.SetActive(true);

        if (msgRoutine != null) StopCoroutine(msgRoutine);
        msgRoutine = StartCoroutine(MessageRoutine(text, seconds));
    }

    IEnumerator MessageRoutine(string text, float seconds)
    {
        messageText.text = text;
        yield return new WaitForSeconds(seconds);
        messageText.text = "";
        messageText.gameObject.SetActive(false);
        msgRoutine=null;
    }
}