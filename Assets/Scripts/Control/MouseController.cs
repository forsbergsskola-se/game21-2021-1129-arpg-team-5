using System;
using Team5.Core;
using Team5.Movement;
using Team5.Ui;
using UnityEngine;

namespace Team5.Control
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
        
        
        
        // Todo: This heavy use of Input.GetMouseButtonDown is not nice. Try to find a nicer implementation.
        private void Update()
        {
            bool mouseClicked;
            if (Input.GetMouseButtonDown(0))
            {
                mouseClicked = true;
                GameObject.Find("Player").GetComponent<Move>().Cancel();
                // Invoke a event to signify for others that the player clicked something.
                ChangedTarget?.Invoke(this, true);
            }
            else
            {
                mouseClicked = false;
            }
            
            int layermask = 1 << 13;
            layermask = ~layermask;
            
            ray = cameraObject.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, layermask)) 
            {
                SetCursorTexture(null);
                
                // if (Input.GetMouseButtonDown(0))
                //     GameObject.Find("Player").GetComponent<Move>().Cancel();
                
                return;
            }
        
            if (hit.collider.gameObject.TryGetComponent(out IInteractable interact))
            {
                SetCursorTexture(interact.mouseTexture);

                if (mouseClicked)
                {
                    interact.OnClick(hit.point);
                    if (hit.collider.TryGetComponent(out OutlineController outlineController))
                    {
                        outlineController.OnClick();
                    }
                }
                else 
                    interact.OnHover();
            }
            else
            {
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
