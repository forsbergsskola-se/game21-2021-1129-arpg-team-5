using System.Collections;
using System.Collections.Generic;
using Team5.Core.UI.Dragging;
using Team5.Inventories;
using Team5.UI.Inventories;
using UnityEngine;

public class EquipmentSlotUI : MonoBehaviour, IItemHolder, IDragContainer<InventoryItem>
{
    [SerializeField] InventoryItemIcon icon = null;
    [SerializeField] EquipedLocation equipedlocation = EquipedLocation.Weapon;

    Equipment playerEquipment;

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        playerEquipment = player.GetComponent<Equipment>();
        playerEquipment.equipmentUpdated += RedrawUI;
    }

    private void Start()
    {
        RedrawUI();
    }

    public int MaxAcceptable(InventoryItem item)
    {
        EquipedItem equipableItem = item as EquipedItem;
        if (equipableItem == null) return 0;
        if (equipableItem.GetAllowedEquipLocation() != equipedlocation) return 0;
        if (GetItem() != null) return 0;

        return 1;
    }

    public void AddItems(InventoryItem item, int number)
    {
        playerEquipment.AddItem(equipedlocation, (EquipedItem)item);
    }

    public InventoryItem GetItem()
    {
        return playerEquipment.GetItemInSlot(equipedlocation);
    }

    public int GetNumber()
    {
        if (GetItem() != null)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public void RemoveItems(int number)
    {
        playerEquipment.RemoveItem(equipedlocation);
    }

    void RedrawUI()
    {
        icon.SetItem(playerEquipment.GetItemInSlot(equipedlocation));
    }
}
