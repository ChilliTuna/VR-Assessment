using UnityEngine;

public class TurretCreator : MonoBehaviour
{
    public GameObject turretType;

    public HandManager handManager;

    public Collider grabCollider;

    private bool prepareSpawn = false;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0)
        {
            if (prepareSpawn)
            {
                MakeNewTurret();
                prepareSpawn = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == grabCollider)
        {
            prepareSpawn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == grabCollider)
        {
            prepareSpawn = false;
        }
    }

    private void MakeNewTurret()
    {
        GameObject newTurret = Instantiate(turretType);
        newTurret.transform.position = transform.position;
    }
}