using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    [Header("Tuning")]
    public float defaultDuration = 0.18f;
    public float defaultStrength = 0.18f;
    public int defaultVibrato = 18;

    Vector3 originalLocalPos;
    Coroutine shakeRoutine;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        originalLocalPos = transform.localPosition;
    }

    void OnEnable()
    {
        originalLocalPos = transform.localPosition;
    }

    public void Shake(float duration, float strength, int vibrato = 18)
    {
        if (!isActiveAndEnabled) return;

        if (shakeRoutine != null) StopCoroutine(shakeRoutine);
        shakeRoutine = StartCoroutine(ShakeRoutine(duration, strength, vibrato));
    }

    public void ShakeDefault()
    {
        Shake(defaultDuration, defaultStrength, defaultVibrato);
    }

    IEnumerator ShakeRoutine(float duration, float strength, int vibrato)
    {
        float t = 0f;
        float step = (vibrato <= 0) ? 0.02f : (duration / vibrato);

        while (t < duration)
        {
            // random inside sphere for punchy shake
            Vector3 offset = Random.insideUnitSphere * strength;
            transform.localPosition = originalLocalPos + offset;

            yield return new WaitForSeconds(step);
            t += step;
        }

        transform.localPosition = originalLocalPos;
        shakeRoutine = null;
    }
}