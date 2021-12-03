using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLogic : MonoBehaviour, IOpenLogic
{
    [SerializeField] private float MoveSpeed;

    private bool isOpen;
    
    // Method to open door:
    public void Open()
    {
        if (isOpen)
            return;
        StartCoroutine(OpeningAnimation());
        Debug.Log("this goes up");


    }

    private IEnumerator OpeningAnimation()
    {
        for (int distance = 0; distance < 30; distance++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Translate(0.5f*Vector3.up);
        }
    }
}
