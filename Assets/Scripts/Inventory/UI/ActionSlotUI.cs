using Team5.Core.UI.Dragging;
using Team5.Inventories;
using UnityEngine;

namespace Team5.UI.Inventories
{
    public class ActionSlotUI : MonoBehaviour, IItemHolder, IDragContainer<InventoryItem>
    {
        [SerializeField] private InventoryItemIcon icon = null;
        [SerializeField] private int index = 0;

        private ActionStore store;
        private const int NumberKeysOffset = 49;
        
        

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

        private void Update()
        {
            if (Input.GetKeyDown((KeyCode)(NumberKeysOffset+index)))
                store.Use(index, store.gameObject);
        }
    }
}
