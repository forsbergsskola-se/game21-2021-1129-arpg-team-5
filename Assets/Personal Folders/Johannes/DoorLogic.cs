using System.Collections;
using UnityEngine;

public class DoorLogic : MonoBehaviour, IOpenLogic
{
    [SerializeField] private float rotationSpeed;

    private bool isOpen;
    
    
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

        for (int angle = 0; angle < 90; angle++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Rotate(Vector3.up, -1);
        }
        
    }
}
