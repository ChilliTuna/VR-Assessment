using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    Enemy enemyStats;
    private float bulletDamage;
    public float projectileSpeed = 10f;
    private bool isAOE;
    private float explosionRadius = 0f;

    public void Chase(Transform givenTarget, float damage, bool aoeAttack, float aoeAttackRadius)
    {
        target = givenTarget;
        bulletDamage = damage;
        isAOE = aoeAttack;
        explosionRadius = aoeAttackRadius;
        enemyStats = target.gameObject.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        //Speed of the projectile during the frames for a constant speed
        float speedOverDistance = projectileSpeed * Time.deltaTime;

        if (direction.magnitude <= speedOverDistance)
        {
            if (isAOE)
            {
                AreaOfEffectAttack();
            }
            else
            {
                HitTarget();
            }
        }

        transform.Translate(direction.normalized * speedOverDistance, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        enemyStats.TakeDamage(bulletDamage);
        Destroy(gameObject);
    }

    void AreaOfEffectAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider c in colliders)
        {
            if (c.gameObject.CompareTag("Enemy"))
            {
                enemyStats.TakeAOEDamage(c.transform, bulletDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.localPosition, explosionRadius);
    }
}
