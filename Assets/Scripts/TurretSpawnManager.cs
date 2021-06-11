using System.Collections.Generic;
using UnityEngine;

public class TurretSpawnManager : MonoBehaviour
{
    private List<TurretCreator> turrets = new List<TurretCreator>();
    private List<TurretCreator> collidingTurrets = new List<TurretCreator>();

    private GameObject currentTower;
    private bool currentlyPlacing = false;

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            turrets.Add(transform.GetChild(i).gameObject.GetComponent<TurretCreator>());
        }
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (TurretCreator turret in turrets)
        {
            if (turret.activeTurret != null && turret.activeTurret.GetComponent<OVRGrabbable>().isActiveAndEnabled)
            {
                currentlyPlacing = true;
                turret.beingPlaced = true;
                break;
            }
            else
            {
                turret.beingPlaced = false;
                currentlyPlacing = false;
            }
        }
        foreach (TurretCreator turretCreator in turrets)
        {
            turretCreator.isSpawnBlocked = currentlyPlacing;
        }
        EstablishBestTurret();
    }

    private void EstablishBestTurret()
    {
        foreach (TurretCreator turret in turrets)
        {
            if (turret.isColliding)
            {
                turret.distanceToHand = Vector3.Magnitude(turret.currentCollision.transform.position - turret.transform.position);
                if (!collidingTurrets.Contains(turret))
                {
                    collidingTurrets.Add(turret);
                }
            }
            else
            {
                if (collidingTurrets.Contains(turret))
                {
                    collidingTurrets.Remove(turret);
                }
            }
        }
        if (collidingTurrets.Count > 1)
        {
            TurretCreator closestTurret = collidingTurrets[0];
            foreach (TurretCreator turret in collidingTurrets)
            {
                turret.preferredTurret = false;
                if (turret.distanceToHand < closestTurret.distanceToHand)
                {
                    closestTurret = turret;
                }
            }
            closestTurret.preferredTurret = true;
        }
        else if (collidingTurrets.Count == 1)
        {
            collidingTurrets[0].preferredTurret = true;
        }
    }
}