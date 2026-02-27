using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float intensity)
    {
        Vector3 original = transform.localPosition;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            transform.localPosition = original + Random.insideUnitSphere * intensity;
            yield return null;
        }

        transform.localPosition = original;
    }
}