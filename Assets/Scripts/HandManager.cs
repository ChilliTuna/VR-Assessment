using UnityEngine;

public class HandManager : MonoBehaviour
{
    public string leftHandName;
    public string rightHandName;

    public Material handMaterial;

    [HideInInspector]
    public GameObject rightHand;

    [HideInInspector]
    public GameObject leftHand;

    private bool handsFound = false;

    private void Start()
    {
        InvokeRepeating("GetHands", 0, 0.2f);
        InvokeRepeating("SetMaterial", 0, 0.2f);
    }

    private void SetMaterial()
    {
        if (handsFound)
        {
            rightHand.GetComponentInChildren<SkinnedMeshRenderer>().material = handMaterial;
            leftHand.GetComponentInChildren<SkinnedMeshRenderer>().material = handMaterial;
            if (rightHand.GetComponentInChildren<SkinnedMeshRenderer>().material == handMaterial && leftHand.GetComponentInChildren<SkinnedMeshRenderer>().material == handMaterial)
            {
                CancelInvoke("SetMaterial");
            }
        }
    }

    private void GetHands()
    {
        rightHand = GameObject.Find(rightHandName);
        leftHand = GameObject.Find(leftHandName);
        if (rightHand != null && leftHand != null)
        {
            handsFound = true;
            CancelInvoke("GetHands");
        }
    }
}