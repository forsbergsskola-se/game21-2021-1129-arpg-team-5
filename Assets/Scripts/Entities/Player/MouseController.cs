using System;
using Logic;
using UnityEngine;

namespace Entities.Player
{
    public class MouseController : MonoBehaviour
    {
        private Ray ray;
        private Camera cameraObject;
    
        public CursorMode cursorMode = CursorMode.Auto;
        public Vector3 hotSpot = Vector3.zero;

        private void Start()
        {
            cameraObject = gameObject.GetComponent<Camera>();
        }

        private void Update() 
        {
            ray = cameraObject.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit)) 
            {
                SetCursorTexture(null);
                return;
            }
        
            if (hit.collider.gameObject.TryGetComponent(out IInteractable interact))
            {
                SetCursorTexture(interact.mouseTexture);

                if (Input.GetMouseButtonDown(0))
                {
                    // Invoke a event to signify for others that the player switched to a different target.
                    ChangedTarget?.Invoke(this, true);
                    
                    interact.OnClick(hit.point);
                }
                else 
                    interact.OnHover();
            }
            else {
                SetCursorTexture(null);
            }
        }


        private void SetCursorTexture(Texture2D texture)
        {
            Cursor.SetCursor(texture, hotSpot, cursorMode);
        }

        /// <summary>
        /// Invoked with a true when the player switch target by pressing a interactable object.
        /// </summary>
        public event EventHandler<bool> ChangedTarget;
    }
}
