using UnityEngine;

public class RoomController : MonoBehaviour
{
    
    [Tooltip("Add objects that should be hidden when the room is unlocked.")]
    public GameObject[] objectsToDisable;
    

    
    /// <summary>
    /// Disable all selected objects to make the room more open and clear..
    /// </summary>
    public void DisableObjectsForVisibility()
    {
        foreach (var obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }
}
