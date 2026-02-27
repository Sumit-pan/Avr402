using UnityEngine;

public class GunEffects : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    public Light muzzleLight;              
    public float muzzleLightTime = 0.05f;

    public AudioSource audioSource;       
    public AudioClip gunshotClip;

    public float recoilAmount=5f;
    public float recoilReturnSpeed=8f;

    float lightOffTime;
    Quaternion originalRotation;
    Quaternion targetRotation;

    void Awake()
    {
        if (!audioSource) audioSource = GetComponent<AudioSource>();
        if (muzzleLight) muzzleLight.enabled = false;

        originalRotation = transform.localRotation;
        targetRotation = originalRotation;
    }

    void Update()
    {
        if (muzzleLight && muzzleLight.enabled && Time.time >= lightOffTime)
            muzzleLight.enabled = false;

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation, originalRotation,Time.deltaTime*recoilReturnSpeed
        );
    }

    public void PlayShot()
    {
        
        if (muzzleFlash) muzzleFlash.Play(true);

        if (muzzleLight)
        {
            muzzleLight.enabled = true;
            lightOffTime = Time.time + muzzleLightTime;
        }

        if (audioSource && gunshotClip)
        {
            audioSource.pitch = Random.Range(0.97f, 1.03f);
            audioSource.PlayOneShot(gunshotClip);
        }
        
        float rand = Random.Range(-3f,3f);
        targetRotation = originalRotation * Quaternion.Euler(-recoilAmount, rand, 0f);
        transform.localRotation = targetRotation;
    }
}