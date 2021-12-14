using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMPNAME_transparencyTrigger : MonoBehaviour
{
    private Ray ray;
    private GameObject cameraObject;

    public Material transparency;
    private Material currentMat;

    private RaycastHit lastHit;

    private Renderer lastObject;
    
    
    // Start is called before the first frame update
    void Start()
    {
        cameraObject = GameObject.FindWithTag("MainCamera");
        lastHit = new RaycastHit();
    }

    // Update is called once per frame
    void Update()
    {
        // Shoot a ray toward the camera.
        int layermask = 1 << 13;

        // layermask = ~layermask;

        ray = new Ray(transform.position, transform.position - cameraObject.transform.position);
        
        // Gizmos.DrawLine(transform.position, transform.position - cameraObject.transform.position);
        // If the ray hits a object
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layermask))
        {
            Debug.Log("HIT!");
            Debug.Log(hit.collider.gameObject.name);
            
            // hit.collider.gameObject.SetActive(false);
            //
            // THIS WORKS!
            // TODO: Replace with something that instead of destroying it, makes it transparent until the player is no longer hitting it.
            // hit.collider.transform.parent.gameObject.SetActive(false);

            // currentMat = hit.collider.transform.parent.gameObject.GetComponent<Renderer>().material;

            // hit.collider.transform.parent.gameObject.GetComponent<Renderer>().material = transparency;


            // TODO: Make possible to change values of multiple gameobjects attached like children.


            // if (lastHit.collider != hit.collider)
            // {
            //     // lastHit.collider.transform.parent.GetComponent<Renderer>().material = currentMat;
            //     hit.collider.transform.parent.GetComponent<Renderer>().material = transparency;
            //     // lastHit = hit;
            //     currentMat = hit.collider.transform.parent.GetComponent<Renderer>().material;
            // }

            if (lastHit.collider != hit.collider)
            {
                // lastObject.material = currentMat;
                lastObject = hit.collider.transform.parent.GetComponent<Renderer>();
                currentMat = lastObject.material;
                lastObject.material = transparency;
                lastHit = hit;
            }


            // TODO:
            // Check if the object is already hit by comparing the last known hit.

            // If it was not hit already:
            // Set lasthit object to stop being transparent
            // Make current hit transparent
            // Set lastHit to current hit.

            // If was already hit:
            // Do nothing

            // If no hit was made make lasthit null.
        }
        else
        {
            if(lastObject == null)
                return;
            lastObject.material = currentMat;
            lastHit = new RaycastHit();
        }
    }
}
