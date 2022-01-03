using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team5.Inventories
{
    [CreateAssetMenu(menuName = ("Team5/Inventory/Action Item"))]
    
    
    public class ActionItem : InventoryItem
    {
        [Tooltip("Does this item get consumed every time its used.")]
        [SerializeField] bool consumable = false;
        
        public virtual bool Use(GameObject user)
        {
            Debug.Log("Using action: " + this);
            
            if (pickup.TryGetComponent(out IConsumable consumable))
            {
                Debug.Log("EatEateateat nomnomnom");
                
                return consumable.Consume(user);
            }

            Debug.Log("Item could not be consumed since no IConsumable interface was found.");
            return false;
        }

        public bool isConsumable()
        {
            return consumable;
        }
    }
}