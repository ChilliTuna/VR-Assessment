using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform end;

    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(end.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
