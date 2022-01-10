using System;
using FMODUnity;
using Team5.Core;
using Team5.Entities.Player;
using Team5.Inventories.Control.sample;
using Team5.Movement;
using Team5.Ui;
using UnityEngine;
using UnityEngine.EventSystems;
using static Team5.Entities.Player.PlayerController;

namespace Team5.Control
{
    public class MouseController : MonoBehaviour
    {
        [System.Serializable]
        public struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        private Ray ray;
        private Camera cameraObject;
        private bool mouseClicked;

        private RaycastHit hit;
        private Collider lastHitCollider;
        private IInteractable targetInteractable;
        [SerializeField] float raycastRadius = 1f;
        [SerializeField] CursorMapping[] cursorMappings = null;
        public CursorMode cursorMode = CursorMode.Auto;
        public Vector3 hotSpot = Vector3.zero;
        [SerializeField] private Texture2D invalidCursor;

        PlayerController player;

        [SerializeField] private StudioEventEmitter invalidLocationSound;


        private void Start()
        {
            player = GetComponent<PlayerController>();
            cameraObject = gameObject.GetComponent<Camera>();
        }
        
        

        private void Update()
        {
            CheckMouseButton();
            
            if (!InteractWithComponent())
            {
                if (InteractWithUI()) return;

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
                SetCursorTexture(invalidCursor);
                TryPlaySound();
            }
        }



        private void TryPlaySound()
        {
            if (mouseClicked)
                invalidLocationSound.Play();
        }
        
        
        
        bool InteractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }
            return false;
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
        
        

        private bool TryCastRaySuccess()
        {
            // Ignore layer 13 and 2. Layer 13 is used for the transparency tester, which also casts rays, but from the player towards the camera. And they need to react to different hitboxes. Layer 2 is IgnoreRaycast.
            int layermask = 1 << 13 | 1 << 2;
            layermask = ~layermask;
            LayerMask mask = layermask;
            
            ray = cameraObject.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, layermask))
            {
                SetCursorTexture(invalidCursor);
                TryPlaySound();
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
        
        

        private bool InteractWithComponent()
        {
            //print("Can Interact");
            RaycastHit[] hits = RaycastAllSorted();
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(player))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), raycastRadius);
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }
        /// <summary>
        /// Invoked with a true when the player clicks the mouse button, canceling actions.
        /// </summary>
        public event EventHandler<bool> ChangedTarget;
    }
}
