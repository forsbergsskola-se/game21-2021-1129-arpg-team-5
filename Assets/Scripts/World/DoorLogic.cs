using System.Collections;
using FMODUnity;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Team5.Core;
using Team5.Ui;

namespace Team5.World.Interactables
{
    public class DoorLogic : MonoBehaviour, IOpenLogic
    {
        public FMODUnity.EventReference unlockedDoor;
        [SerializeField] private float timeToOpen;
        [SerializeField] private float openDegrees;
    
        private bool isOpen;
        private float movementPerFrame;
        private float totalAnimationFrames;
        private float frameTime;
        
        
        private const float AnimationFramerate = 60;

        
        
        private void Awake()
        {
            totalAnimationFrames = Mathf.Round(timeToOpen * AnimationFramerate);
            movementPerFrame = openDegrees / totalAnimationFrames;
            frameTime = 1 / AnimationFramerate;
        }

        
        
        public void Open()
        {
            if (isOpen) 
                return;
            StartCoroutine(OpeningAnimation());
            GetComponent<StudioEventEmitter>().Play();
        }

        
        
        private IEnumerator OpeningAnimation()
        {
            for (int i = 0; i < totalAnimationFrames; i++)
            {
                yield return new WaitForSeconds(frameTime);
                transform.Rotate(Vector3.up, movementPerFrame);
            }
            
            GetComponent<OutlineController>().DisableOutlineController();
        }

    
        
        // Draw a line indicating where the door will stop when opened. Only visible if gizmos are enabled.
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;

            Vector3 doorEndPosition = transform.Find("DoorEndMarker").transform.localPosition;

            Vector3 target = Quaternion.Euler(0, openDegrees, 0) * doorEndPosition;
        
            Gizmos.DrawLine(transform.position, transform.TransformPoint(target)); 
        }
    }
}