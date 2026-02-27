using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PooledProjectile : MonoBehaviour
{
    public float speed = 25f;
    public float lifeTime = 2.5f;
    public int damage = 50;

    float life;
    Rigidbody rb;
    Collider col;
    BulletPool pool;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        rb.useGravity = false;
        rb.isKinematic = false;

        
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        
        col.isTrigger = false;
    }

    public void Init(BulletPool poolRef, Vector3 position, Quaternion rotation)
    {
        pool = poolRef;

        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.SetPositionAndRotation(position, rotation);

        life = lifeTime;
        col.enabled = true;
        gameObject.SetActive(true);

        rb.velocity = transform.forward * speed;
    }

    void Update()
    {
        life -= Time.deltaTime;
        if (life <= 0f)
            ReturnToPool();
    }

    void OnCollisionEnter(Collision collision)
    {
        var hp = collision.collider.GetComponentInParent<Health>();
        if (hp) hp.TakeDamage(damage);

        ReturnToPool();
    }

    void ReturnToPool()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

       
        col.enabled = false;

        pool.Release(this);
    }
}