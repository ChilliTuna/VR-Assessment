using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMaterialSetter : MonoBehaviour
{
    public string leftHandName;
    public string rightHandName;

    public Material handMaterial;

    private void Start()
    {
        InvokeRepeating("SetMaterial", 0, 0.2f);
    }

    void SetMaterial()
    {
        GameObject.Find(leftHandName).GetComponentInChildren<SkinnedMeshRenderer>().material = handMaterial;
        GameObject.Find(rightHandName).GetComponentInChildren<SkinnedMeshRenderer>().material = handMaterial;
        if (GameObject.Find(leftHandName).GetComponentInChildren<SkinnedMeshRenderer>().material == handMaterial && GameObject.Find(rightHandName).GetComponentInChildren<SkinnedMeshRenderer>().material == handMaterial)
        {
            CancelInvoke("SetMaterial");
        }
    }
}
