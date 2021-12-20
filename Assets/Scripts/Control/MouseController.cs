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
        private bool mouseClicked;

        private RaycastHit hit;
        private Collider lastHitCollider;
        private IInteractable targetInteractable;
    
        public CursorMode cursorMode = CursorMode.Auto;
        public Vector3 hotSpot = Vector3.zero;

        
        
        private void Start()
        {
            cameraObject = gameObject.GetComponent<Camera>();
        }
        
        

        private void Update()
        {
            CheckMouseButton();

            if (TryCastRaySuccess())
            {
                if (TargetHasChanged())
                {
                    SendOldTargetOnHoverExit();
                    
                    if (TryGetInteractableSuccess())
                    {
                        SendOnHoverEnter();
                    }
                }

                if (targetInteractable != null)
                {
                    TrySendOnClick();
                    SetCursorTexture(targetInteractable.mouseTexture);
                    return;
                }
            }
            SetCursorTexture(null);
            
        }
        
        

        private void CheckMouseButton()
        {
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
        }

        public LayerMask mask;
        
        private bool TryCastRaySuccess()
        {
            // Ignore layer 13 and 2. Layer 13 is used for the transparency tester, which also casts rays, but from the player towards the camera. And they need to react to different hitboxes. Layer 2 is IgnoreRaycast.
            int layermask = 1 << 13 | 1 << 2;
            layermask = ~layermask;
            mask = layermask;
            
            ray = cameraObject.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, layermask))
            {
                SetCursorTexture(null);
                return false;
            }

            return true;
        }
        
        
        
        private bool TargetHasChanged()
        {
            bool value = hit.collider != lastHitCollider;
            lastHitCollider = hit.collider;
            return value;
        }
        
        
        
        private void SendOldTargetOnHoverExit()
        {
            if (targetInteractable != null)
                targetInteractable.OnHoverExit();
        }
        
        

        private bool TryGetInteractableSuccess()
        {
            if (hit.collider.gameObject.TryGetComponent(out IInteractable interactable))
            {
                targetInteractable = interactable;
                return true;
            }
            targetInteractable = null;
            return false;
        }
        
        

        private void SendOnHoverEnter()
        {
            targetInteractable.OnHoverEnter();
        }
        
        
        
        private void TrySendOnClick()
        {
            if (mouseClicked)
            {
                targetInteractable.OnClick(hit.point);
                TryCallOutline();
            }
        }


        
        private void TryCallOutline()
        {
            if (hit.collider.TryGetComponent(out OutlineController outlineController))
            {
                outlineController.OnClick();
            }
        }


        
        private void SetCursorTexture(Texture2D texture)
        {
            Cursor.SetCursor(texture, hotSpot, cursorMode);
        }
        
        
        
        /// <summary>
        /// Invoked with a true when the player clicks the mouse button, canceling actions.
        /// </summary>
        public event EventHandler<bool> ChangedTarget;
    }
}
