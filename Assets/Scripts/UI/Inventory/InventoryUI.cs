using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Team5.Inventories;

namespace Team5.Ui.Inventories
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] UISlotInventory itemPrefab;

        Inventory playerInventory = null;

        private void Awake()
        {
            playerInventory = Inventory.GetPlayerInventory();
            playerInventory.inventoryUpdated += Redraw;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Redraw()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < playerInventory.GetSize(); i++)
            {
                var itemUI = Instantiate(itemPrefab, transform);
                itemUI.Setup(playerInventory, i);
            }
        }
    }

}