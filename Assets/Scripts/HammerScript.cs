using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerScript : MonoBehaviour
{
    public TowerScript currentTower;
    public GameManager gameManager;

    private void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Tower"))
        {
            currentTower = other.gameObject.GetComponent<TowerScript>();
            gameManager.currency += (currentTower.cost / 2);
            Destroy(currentTower.gameObject);
        }
    }
}
