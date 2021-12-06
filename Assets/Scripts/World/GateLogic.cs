using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLogic : MonoBehaviour, IOpenLogic
{
    [SerializeField] private float timeToGoUpp;
    [SerializeField] private float openDegrees;
    private float totalAntationFrames;
    private const float AnimationFramerate = 60;
    private float frameTime;
    private bool isOpen;
    private float movementUpPerFrame;
    
    private void Awake()
    {
        totalAntationFrames = Mathf.Round(timeToGoUpp * AnimationFramerate);
        movementUpPerFrame = openDegrees / totalAntationFrames;
        frameTime = 1 / AnimationFramerate;
    }

    // Method to open door:
    public void Open()
    {
        if (isOpen)
            return;
        StartCoroutine(OpeningAnimation());
    }

    private IEnumerator OpeningAnimation()
    {
        for (int distance = 0; distance < totalAntationFrames; distance++)
        {
            yield return new WaitForSeconds(frameTime);
            transform.Translate(movementUpPerFrame*Vector3.up);
        }
    }
}
