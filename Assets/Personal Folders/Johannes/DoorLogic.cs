using System.Collections;
using UnityEngine;

public class DoorLogic : MonoBehaviour, IOpenLogic
{
    [SerializeField] private float timeToOpen;
    [SerializeField] private float OpenDegrees;
    
    private bool isOpen;
    private float movementPerFrame;
    private float totalAnimationFrames;
    private float frameTime;

    private const float AnimationFramerate = 60;

    private void Awake()
    {
        totalAnimationFrames = Mathf.Round(timeToOpen * AnimationFramerate);
        movementPerFrame = OpenDegrees / totalAnimationFrames;
        frameTime = 1 / AnimationFramerate;
    }

    public void Open()
    {
        if (isOpen) 
            return;

        StartCoroutine(OpeningAnimation());
    }

    private IEnumerator OpeningAnimation()
    {
        for (int i = 0; i < totalAnimationFrames; i++)
        {
            yield return new WaitForSeconds(frameTime);
            transform.Rotate(Vector3.up, movementPerFrame);
        }
        
    }
}
