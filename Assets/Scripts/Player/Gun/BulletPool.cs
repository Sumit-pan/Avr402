using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public PooledProjectile bulletPrefab;
    public int initialSize = 30;

    readonly Queue<PooledProjectile> pool = new Queue<PooledProjectile>();

    void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            var b = Instantiate(bulletPrefab, transform);
            b.gameObject.SetActive(false);
            pool.Enqueue(b);
        }
    }

    public PooledProjectile Get()
    {
        if (pool.Count > 0)
            return pool.Dequeue();

        // If we run out, expand
        var b = Instantiate(bulletPrefab, transform);
        b.gameObject.SetActive(false);
        return b;
    }

    public void Release(PooledProjectile bullet)
    {
        bullet.gameObject.SetActive(false);
        pool.Enqueue(bullet);
    }
}