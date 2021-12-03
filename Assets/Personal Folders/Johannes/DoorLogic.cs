using System.Collections;
using UnityEngine;

public class DoorLogic : MonoBehaviour, IOpenLogic
{
    [SerializeField] private float timeToOpen;
    
    private bool isOpen;
    private float movementPerFrame;
    private float totalAnimationFrames;

    
    
    private void Awake()
    {
        totalAnimationFrames = Mathf.Round(timeToOpen * 60);
        movementPerFrame = 90 / totalAnimationFrames;
    }

    public void Open()
    {
        if (isOpen) 
            return;

        StartCoroutine(OpeningAnimation());
            
        Debug.Log("This is open.");
    }

    private IEnumerator OpeningAnimation()
    {
        for (int i = 0; i < totalAnimationFrames; i++)
        {
            yield return new WaitForSeconds(0.0166f);
            transform.Rotate(Vector3.up, movementPerFrame);
        }
        
    }
}
