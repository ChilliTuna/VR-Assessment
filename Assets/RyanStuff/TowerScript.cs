﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [Header("Tower Attributes")]
    public float attackPower = 1f;
    public float attackSpeed = 1f;
    private float attackSpeedCountdown = 0f;
    public float attackRange = 10f;


    [Header("Tower Setup Settings")]
    public string enemyTag = "Enemy";
    private Transform target;
    public Transform rotateTowerPart;

    public GameObject projectilePrefab;
    public Transform firePoint;

    //How fast to rotate the tower towards an enemy
    public float towerRotationSpeed = 5f;
    //How often a tower performs checks of enemies in the map. **Does require more computing power the quickly you check
    public float towerTargetSearchRate = 0.5f;


    private void Start()
    {
        InvokeRepeating("SearchForTarget", 0f, towerTargetSearchRate);
    }

    void Update()
    {
        if(target == null)
        {
            return;
        }
        //Rotates the tower into the enemies direction
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 towerRotation = Quaternion.Lerp(rotateTowerPart.rotation, lookRotation, towerRotationSpeed * Time.deltaTime).eulerAngles;
        rotateTowerPart.rotation = Quaternion.Euler(0f, towerRotation.y, 0f);

        if(attackSpeedCountdown <= 0f)
        {
            Attack();
            attackSpeedCountdown = 1f / attackSpeed;
        }
        attackSpeedCountdown -= Time.deltaTime;
    }

    void Attack()
    {
        GameObject projectileVar = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Projectile projectile = projectileVar.GetComponent<Projectile>();

        if(projectile != null)
        {
            projectile.Chase(target);
        }
    }

    void SearchForTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= attackRange)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
