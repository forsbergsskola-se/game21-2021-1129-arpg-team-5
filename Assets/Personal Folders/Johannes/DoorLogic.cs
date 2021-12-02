using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour, IOpenLogic
{
    [SerializeField] private float rotationSpeed;

    private bool isOpen;
    // Method to open door:
    public void Open()
    {
        if (isOpen) 
            return;

        StartCoroutine(OpeningAnimation());
            
        Debug.Log("This is open.");
        
    }

    private IEnumerator OpeningAnimation()
    {
        Debug.Log("Hello");
        transform.Rotate(Vector3.up, -45f);
        // while (this.transform.rotation.y)
        // {
        //     
        // }
        yield return new WaitForSeconds(1f);
        transform.Rotate(Vector3.up, -45f);
    }
}
