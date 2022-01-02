using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team5.Inventories
{
    [CreateAssetMenu(menuName = ("Team5/Inventory/Equipable Item"))]
    public class EquipedItem : InventoryItem
    {
        [Tooltip("Where allowed to put this item.")]
        [SerializeField] EquipedLocation allowedEquipLocation = EquipedLocation.Weapon;

        public EquipedLocation GetAllowedEquipLocation()
        {
            return allowedEquipLocation;
        }
    }

}