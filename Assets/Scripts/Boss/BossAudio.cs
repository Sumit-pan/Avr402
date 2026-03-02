using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MonsterAudio : MonoBehaviour
{
    [Header("Clips")]
    public AudioClip idleLoop;
    public AudioClip chaseLoop;
    public AudioClip attackSfx;     // quick roar / scream
    public AudioClip jumpscareHit;  // optional loud stinger

    [Header("Volume")]
    [Range(0f, 1f)] public float loopVolume = 0.6f;
    [Range(0f, 1f)] public float oneShotVolume = 1.0f;

    AudioSource src;

    void Awake()
    {
        src = GetComponent<AudioSource>();
        src.loop = true;
        src.playOnAwake = false;
        src.spatialBlend = 1f; // 3D sound
    }

    public void PlayIdle()
    {
        PlayLoop(idleLoop);
    }

    public void PlayChase()
    {
        PlayLoop(chaseLoop);
    }

    void PlayLoop(AudioClip clip)
    {
        if (!clip) return;
        if (src.clip == clip && src.isPlaying) return;

        src.clip = clip;
        src.volume = loopVolume;
        src.loop = true;
        src.Play();
    }

    public void StopLoop()
    {
        src.Stop();
        src.clip = null;
    }

    public void PlayAttack()
    {
        if (attackSfx) src.PlayOneShot(attackSfx, oneShotVolume);
    }

    public void PlayJumpscare()
    {
        if (jumpscareHit) src.PlayOneShot(jumpscareHit, 1f);
    }
}