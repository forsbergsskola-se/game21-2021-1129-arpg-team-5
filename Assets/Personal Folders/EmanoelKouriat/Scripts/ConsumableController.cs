using System.Collections.Generic;
using UnityEngine;

namespace Team5.Ui.Hotbar
{
    public class ConsumableController : MonoBehaviour
    {
        public List<GameObject> hotbarSlots = new();

        private void Start()
        {
            
            // Get all the hotbar slots and put them inside a list.
            Transform slotSearch = gameObject.transform;
            for (int i = 0; slotSearch != null; i++)
            {
                slotSearch = transform.Find($"HotbarSlot{i}");
                if (slotSearch != null)
                    hotbarSlots.Add(slotSearch.gameObject);
            }

            foreach (var VARIABLE in hotbarSlots)
            {
                Debug.Log("Found: " + VARIABLE.name);
            }
        }
    }
}