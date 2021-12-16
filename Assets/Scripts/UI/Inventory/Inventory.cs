using System;
using System.Collections;
using System.Collections.Generic;
using Team5.Core.saving;
using UnityEngine;

namespace Team5.Inventories
{
    public class Inventory : MonoBehaviour, IISavable
    {
        [SerializeField] int inventorySize = 16;


        InventoryItem[] slots;


        public event Action inventoryUpdated;


        private void Awake()
        {
            slots = new InventoryItem[inventorySize];
            slots[0] = InventoryItem.GetFromID("e9a47ad7-8867-4ec7-962a-4c7da9a7bd4f");
            slots[1] = InventoryItem.GetFromID("04859820-cbbc-4a2c-9660-d9c12cc44ca3");
        }

        public static Inventory GetPlayerInventory()
        {
            var player = GameObject.FindWithTag("Player");
            return player.GetComponent<Inventory>();
        }
        

        public bool HasSpaceFor(InventoryItem item)
        {
            return FindSlot(item) >= 0;
        }


        public int GetSize()
        {
            return slots.Length;
        }


        public bool AddToFirstEmptySlot(InventoryItem item)
        {
            int i = FindSlot(item);

            if (i < 0)
            {
                return false;
            }

            slots[i] = item;
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
            return true;
        }

        public bool HasItem(InventoryItem item)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (object.ReferenceEquals(slots[i], item))
                {
                    return true;
                }
            }
            return false;
        }

        public InventoryItem GetItemInSlot(int slot)
        {
            return slots[slot];
        }


        public void RemoveFromSlot(int slot)
        {
            slots[slot] = null;
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
        }


        public bool AddItemToSlot(int slot, InventoryItem item)
        {
            if (slots[slot] != null)
            {
                return AddToFirstEmptySlot(item); ;
            }

            slots[slot] = item;
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
            return true;
        }


     

        private int FindSlot(InventoryItem item)
        {
            return FindEmptySlot();
        }


        private int FindEmptySlot()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        

        public void RestoreState(object state)
        {
            var slotStrings = (string[])state;
            for (int i = 0; i < inventorySize; i++)
            {
                slots[i] = InventoryItem.GetFromID(slotStrings[i]);
            }
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
        }

        public object CaptureState()
        {
            var slotStrings = new string[inventorySize];
            for (int i = 0; i < inventorySize; i++)
            {
                if (slots[i] != null)
                {
                    slotStrings[i] = slots[i].GetItemID();
                }
            }
            return slotStrings;
        }
    }
}
