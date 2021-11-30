using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private Ray ray;

    private Camera camera;
    private void Start()
    {
        // TODO: Look into getting the interactables only once instead, saving performance.
        camera = GameObject.FindObjectOfType<Camera>();
    }

    private void Update()
    {
        ray = camera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit))
            return;
        
        if (hit.collider.gameObject.TryGetComponent(out IInteractable iInteractable))
        {
            if (Input.GetMouseButtonDown(0))
            {
                iInteractable.OnClick();
            }
            else
            {
                iInteractable.OnHover();
            }
        }
    }
}
