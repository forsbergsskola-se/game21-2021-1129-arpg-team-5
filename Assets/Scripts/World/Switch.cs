using Team5.Core;
using UnityEngine;

namespace Team5.World.Interactables
{
    public class Switch : MonoBehaviour, IInteractable
    {
        public InteractableBarrierController barrier;
        [SerializeField] private Texture2D OnHoverMousetexture;

        private void Start()
        {
            if (barrier == null)
            {
                Debug.Log("Switch has no assigned InteractableBarrierController!");
            }
            barrier.IsLocked = true;
        }

        public Texture2D mouseTexture => OnHoverMousetexture;
        public void OnHoverEnter()
        {
        }

        public void OnHoverExit()
        {
        }

        public void OnClick(Vector3 mouseClickVector)
        {
            barrier.IsLocked = false;
            
            transform.Find("Lever").transform.Rotate(Vector3.back, 60);
            
            GetComponent<BoxCollider>().enabled = false;
            this.enabled = false;
        }
    }
}
