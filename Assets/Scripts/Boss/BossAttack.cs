using UnityEngine;

public class BossAttackTrigger : MonoBehaviour
{
    public BossEndingChase boss;

    void Reset()
    {
        boss = GetComponentInParent<BossEndingChase>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!boss) boss = GetComponentInParent<BossEndingChase>();
        if (!boss) return;

        if (other.CompareTag("Player"))
            boss.TryAttackNow();
    }

    void OnTriggerStay(Collider other)
    {
        if (!boss) return;
        if (other.CompareTag("Player"))
            boss.TryAttackNow();
    }
}