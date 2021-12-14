using System.Collections;
using UnityEngine;
using Team5.Core;
using Team5.Ui;

namespace Team5.World.Interactables
{
    public class GateLogic : MonoBehaviour, IOpenLogic
    {
        [SerializeField] private float distanceToOpen;
        [SerializeField] private float timeToGoUp;
    
    
        private const float AnimationFramerate = 60;
        private float frameTime;
        private bool isOpen;
        private float movementUpPerFrame;
        private float totalAnimationFrames;
        public AudioSource Opening;
    
    
    
        private void Awake()
        {
            totalAnimationFrames = Mathf.Round(timeToGoUp * AnimationFramerate);
            movementUpPerFrame = distanceToOpen / totalAnimationFrames;
            frameTime = 1 / AnimationFramerate;
        }

    
    
        // Method to open door:
        public void Open()
        {
            if (isOpen)
                return;
            Opening.Play();
            StartCoroutine(OpeningAnimation());
        }

    
    
        private IEnumerator OpeningAnimation()
        {
            for (int distance = 0; distance < totalAnimationFrames; distance++)
            {
                yield return new WaitForSeconds(frameTime);
                transform.Translate(movementUpPerFrame*Vector3.up);
            }

            GetComponent<OutlineController>().DisableOutlineController();
        }
    }
}