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
        
        public virtual void Use(GameObject user)
        {
            Debug.Log("Using action: " + this);
        }

        public bool isConsumable()
        {
            return consumable;
        }
    }

}