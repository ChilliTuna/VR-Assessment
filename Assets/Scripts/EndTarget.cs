using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTarget : MonoBehaviour
{

    public GameManager manager;

    private void OnTriggerEnter(Collider other)
    {

        Debug.LogWarning("Something entered the trigger");

        if (other.tag == "Enemy")
        {
            manager.playerHealth -= 1;

            Debug.LogWarning("Destroyed Object");

            Destroy(other.gameObject);
        }
    }
}
