using UnityEngine;

public class TurretCreator : MonoBehaviour
{
    public GameObject turretType;

    public GameManager gameManager;

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
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            if (isColliding)
            {
                if (!isSpawnBlocked)
                {
                    if (preferredTurret)
                    {
                        if (gameManager.currency >= turretType.GetComponent<TowerScript>().cost)
                        {
                            MakeNewTurret();
                            isColliding = false;
                            gameManager.currency -= turretType.GetComponent<TowerScript>().cost;
                        }
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