using UnityEngine;

public class TransparencyTriggerer : MonoBehaviour
{
    private Ray ray;
    private GameObject cameraObject;

    public Material transparency;
    
    private Material currentMat;
    private RaycastHit lastHit;
    private Renderer lastObject;
    
    
    
    void Awake()
    {
        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        lastHit = new RaycastHit();
    }

    
    
    void Update()
    {
        // Shoot a ray toward the camera.
        int layermask = 1 << 13;
        ray = new Ray(transform.position,  cameraObject.transform.position - transform.position);
        
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layermask))
        {
            if (lastHit.collider != hit.collider)
            {
                // lastObject.material = currentMat;
                lastObject = hit.collider.transform.parent.GetComponent<Renderer>();
                currentMat = lastObject.material;
                lastObject.material = transparency;
                lastHit = hit;
            }
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
