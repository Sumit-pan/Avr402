using UnityEngine;

public class GunShooterPooled : MonoBehaviour
{
    [Header("Refs")]
    public Camera cam;
    public Transform muzzle;
    public BulletPool pool;
    public GunEffects gunEffects;          

    [Header("Fire")]
    public float fireRate = 1f;         
    public float aimRange = 200f;
    public float muzzleForwardOffset = 0.2f;

    [Header("Ammo SFX (optional)")]
    public AudioSource audioSource;
    public AudioClip emptySfx;

    float nextFireTime;
    PlayerInventory inv;

    void Awake()
    {
        if (!cam) cam = Camera.main;
        inv = FindObjectOfType<PlayerInventory>();

        if (!gunEffects) gunEffects = GetComponent<GunEffects>(); // auto-find if on same object
    }

    void Update()
    {
        if (inv == null || !inv.hasGun) return;

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            if (inv.ammo <= 0)
            {
                if (audioSource && emptySfx) audioSource.PlayOneShot(emptySfx);
                return;
            }

            inv.ammo--;

           
            gunEffects?.PlayShot();

           
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);

            Vector3 targetPoint;
            if (Physics.Raycast(ray, out RaycastHit hit, aimRange, ~0, QueryTriggerInteraction.Ignore))
                targetPoint = hit.point;
            else
                targetPoint = cam.transform.position + cam.transform.forward * aimRange;

           
            Vector3 dir = (targetPoint - muzzle.position).normalized;
            Quaternion rot = Quaternion.LookRotation(dir, Vector3.up);

            
            Vector3 spawnPos = muzzle.position + muzzle.forward * muzzleForwardOffset;

            var bullet = pool.Get();
            bullet.Init(pool, spawnPos, rot);
        }
    }
}