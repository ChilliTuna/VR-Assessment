﻿using UnityEngine;

public class TurretCreator : MonoBehaviour
{
    public GameObject turretType;

    public HandManager handManager;

    public Collider grabCollider;

    [HideInInspector]
    public bool isColliding = false;

    [HideInInspector]
    public bool preferredTurret = false;

    [HideInInspector]
    public float distanceToHand;

    [HideInInspector]
    public Collider currentCollision;

    [HideInInspector]
    public bool isSpawnBlocked = false;

    [HideInInspector]
    public bool beingPlaced = false;

    [HideInInspector]
    public GameObject activeTurret;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0)
        {
            if (isColliding)
            {
                if (!isSpawnBlocked)
                {
                    if (preferredTurret)
                    {
                        MakeNewTurret();
                        isColliding = false;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == grabCollider)
        {
            isColliding = true;
            currentCollision = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == grabCollider)
        {
            isColliding = false;
            preferredTurret = false;
        }
    }

    private void MakeNewTurret()
    {
        activeTurret = Instantiate(turretType);
        activeTurret.transform.position = transform.position;
    }
}