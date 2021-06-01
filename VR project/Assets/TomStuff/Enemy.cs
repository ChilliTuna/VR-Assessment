using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform end;

    void Start()
    {
        end = GameObject.Find("EndTarget").transform;

        agent.SetDestination(end.position);
    }

}
