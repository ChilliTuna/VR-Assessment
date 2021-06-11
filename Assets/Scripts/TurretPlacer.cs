using UnityEngine;

public class TurretPlacer : MonoBehaviour
{
    //private bool isPlaced = false;
    
    private void Update()
    {
        //if (!isPlaced)
        //{
            MakeVertical();
        //}
    }

    public void MakeVertical()
    {
        Vector3 newRot = new Vector3(0, 0, 0);
        gameObject.transform.rotation = Quaternion.Euler(newRot);
    }
}