using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    Enemy enemyStats;
    private float bulletDamage;
    public float projectileSpeed = 10f;

    public void Chase(Transform givenTarget, float damage)
    {
        target = givenTarget;
        bulletDamage = damage;
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
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * speedOverDistance, Space.World);
    }

    void HitTarget()
    {
        enemyStats.TakeDamage(bulletDamage);
        Destroy(gameObject);
    }
}
