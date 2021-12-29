using System;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    
    [Tooltip("Add objects that should be hidden when the room is unlocked.")]
    public GameObject[] objectsToDisable;


    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void ActivateRoom()
    {
        gameObject.SetActive(true);
        DisableObjectsForVisibility();
    }
    
    
    
    /// <summary>
    /// Disable all selected objects to make the room more open and clear..
    /// </summary>
    void DisableObjectsForVisibility()
    {
        foreach (var obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }
}
