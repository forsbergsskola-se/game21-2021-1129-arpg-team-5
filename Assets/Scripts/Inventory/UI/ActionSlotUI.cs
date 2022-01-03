using System;
using System.Collections;
using System.Collections.Generic;
using Team5.Core.UI.Dragging;
using Team5.Inventories;
using Team5.UI.Inventories;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Team5.UI.Inventories
{
    public class ActionSlotUI : MonoBehaviour, IItemHolder, IDragContainer<InventoryItem>
    {
        [SerializeField] InventoryItemIcon icon = null;
        [SerializeField] int index = 0;

        ActionStore store;

        public void AddItems(InventoryItem item, int number)
        {
            store.AddAction(item, index, number);
            Debug.Log($"Added {item.name} at index {index} with amount {number}");
        }

        public InventoryItem GetItem()
        {
            return store.GetAction(index);
        }

        public int GetNumber()
        {
            return store.GetNumber(index);
        }

        public int MaxAcceptable(InventoryItem item)
        {
            return store.MaxAcceptable(item, index);
        }

        public void RemoveItems(int number)
        {
            store.RemoveItems(index, number);
        }

        void Awake()
        {
            store = GameObject.FindGameObjectWithTag("Player").GetComponent<ActionStore>();
            store.storeUpdated += UpdateIcon;
        }

        void UpdateIcon()
        {
            icon.SetItem(GetItem(), GetNumber());
        }


        private const int NumberKeysOffset = 49;
        
        private void Update()
        {
            Debug.Log("Testing");
            if (Input.GetKeyDown((KeyCode)(NumberKeysOffset+index)))
            {
                Debug.Log("SMÖRGÅS nr " + index);

                Debug.Log($"Object: {store.name}");

                // if (GetItem().TryGetComponent(out IConsumable consumable))
                // {
                //     Debug.Log("EatEateateat nomnomnom");
                //     consumable.Consume();
                // }

                store.Use(index, store.gameObject);
            }
        }
    }
}
