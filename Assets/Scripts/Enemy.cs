using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform end;

    public float health;
    public float value = 3;
    public float movementSpeed;

    public UnityEvent onDeath;

    void Start()
    {
        end = GameObject.Find("EndTarget").transform;
        agent.speed = movementSpeed;
        agent.SetDestination(end.position);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            onDeath.Invoke();
            Destroy(gameObject);
        }
    }

    public void TakeAOEDamage(Transform enemy, float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(enemy.gameObject);
        }
    }
}
