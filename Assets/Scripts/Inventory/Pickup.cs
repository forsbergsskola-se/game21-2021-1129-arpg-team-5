﻿using UnityEngine;

namespace Team5.Inventories
{
    /// <summary>
    /// To be placed at the root of a Pickup prefab. Contains the data about the
    /// pickup such as the type of item and the number.
    /// </summary>
    public class Pickup : MonoBehaviour
    {
  
        InventoryItem item;
        Inventory inventory;


        private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            inventory = player.GetComponent<Inventory>();
        }


        /// <summary>
        /// Set the vital data after creating the prefab.
        /// </summary>
        /// <param name="item">The type of item this prefab represents.</param>
        public void Setup(InventoryItem item)
        {
            this.item = item;
        }

        public InventoryItem GetItem()
        {
            return item;
        }

        public void PickupItem()
        {
            bool foundSlot = inventory.AddToFirstEmptySlot(item);
            if (foundSlot)
            {
                Destroy(gameObject);
            }
        }

        public bool CanBePickedUp()
        {
            return inventory.HasSpaceFor(item);
        }
    }
}