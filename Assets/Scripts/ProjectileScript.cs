using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;

    public float projectileSpeed = 10f;

    public void Chase(Transform givenTarget)
    {
        target = givenTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        //Speed of the projectile during the frames for a constant speed
        float speedOverDistance = projectileSpeed * Time.deltaTime;

        if(direction.magnitude <= speedOverDistance)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * speedOverDistance, Space.World);
    }

    void HitTarget()
    {

        Destroy(gameObject);
    }
}
