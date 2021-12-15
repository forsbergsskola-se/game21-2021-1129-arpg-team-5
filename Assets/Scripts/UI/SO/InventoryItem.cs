using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team5.Inventory
{
    [CreateAssetMenu(menuName = ("Team5/Inventory/Item"))]
    public class InventoryItem : ScriptableObject //, ISerializationCallbackReceiver
    {
        [SerializeField] int slotSize;
        [SerializeField] string itemID = null;
        [SerializeField] string itemName = null;
        [SerializeField] string description = null;
        [SerializeField] Sprite icon = null;
        [SerializeField] bool isStackable = false; 


        static Dictionary<string, InventoryItem> itemCache;


        public InventoryItem GetFromID(string itemID)
        {
            if(itemCache == null)
            {
                itemCache = new Dictionary<string, InventoryItem>();
                var itemList = Resources.LoadAll<InventoryItem>("");
                foreach (var i in itemList)
                {
                    if (itemCache.ContainsKey(i.itemID)) continue;
                    itemCache[i.itemID] = i;
                }
            }
            if (itemID == null || itemCache.ContainsKey(itemID)) return null;
            return itemCache[itemID];
        }

        public string GetItemID()
        {
            return itemID;
        }

        public string GetItemName()
        {
            return itemName;
        }
        public string GetDescription()
        {
            return description;
        }
        public bool GetStackable()
        {
            return isStackable;
        }

        public Sprite GetItemIcon()
        {
            return icon;
        }

        public int GetSlotSize()
        {
            return slotSize;
        }






        /*

        public void OnBeforeSerialize()
        {
            throw new System.NotImplementedException();
        }

        public void OnAfterDeserialize()
        {
            throw new System.NotImplementedException();
        }*/
    }
}
