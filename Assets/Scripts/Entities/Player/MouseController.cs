using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private Ray ray;

    private Camera camera;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector3 hotSpot = Vector3.zero;
    
    private void Start()
    {
        // TODO: Look into getting the interactables only once instead, saving performance.
        camera = GameObject.FindObjectOfType<Camera>();
    }

    private void Update()
    {
        ray = camera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit))
        {
            Cursor.SetCursor(null, hotSpot, cursorMode);
            return;
        }
        
        if (hit.collider.gameObject.TryGetComponent(out IInteractable Interact))
        {
            Cursor.SetCursor(Interact.mouseTexture, hotSpot, cursorMode);
            if (Input.GetMouseButtonDown(0))
            {
                Interact.OnClick(this);
            }
            else
            {
                Interact.OnHover(this);
            }
        }
        else
        {
            Cursor.SetCursor(null, hotSpot, cursorMode);
        }
    }
    // public void CursorSet(Texture2D texture2D)
    // {
    //     Cursor.SetCursor(texture2D, hotSpot,cursorMode);
    // }
}
