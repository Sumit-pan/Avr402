using System.Collections;
using UnityEngine;

public class DeathScreenManager : MonoBehaviour
{
    public CanvasGroup deathPanel;

    public float fadeDuration = 1.2f;
    public float delayBeforeFade = 0.15f;

    bool shown;

    void Awake()
    {
        if (deathPanel)
        {
            deathPanel.alpha = 0f;
            deathPanel.interactable = false;
            deathPanel.blocksRaycasts = false;
        }
    }

    public void ShowDeath()
    {
        if (shown) return;
        shown = true;

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSecondsRealtime(delayBeforeFade);

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            deathPanel.alpha = Mathf.Clamp01(t / fadeDuration);
            yield return null;
        }

        deathPanel.alpha = 1f;
        deathPanel.interactable = true;
        deathPanel.blocksRaycasts = true;
    }
}