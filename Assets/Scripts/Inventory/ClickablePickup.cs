using UnityEngine;
using Team5.Inventories;
using Team5.Entities.Player;

namespace Team5.Inventories.Control.sample
{
    [RequireComponent(typeof(Pickup))]
    public class ClickablePickup : MonoBehaviour, IRaycastable
    {
        Pickup pickup;

        private void Awake()
        {
            pickup = GetComponent<Pickup>();
        }

        public CursorType GetCursorType()
        {
            if (pickup.CanBePickedUp())
            {
                // print("Selected!");
                return CursorType.Pickup;
            }
            else
            {
                print("Cant select!");
                return CursorType.FullPickup;
            }
        }

        

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                pickup.PickupItem();
            }
            return true;
        }

       
    }
}