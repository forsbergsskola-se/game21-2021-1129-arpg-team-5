using UnityEngine;
using Team5.Entities.Player;

namespace Team5.Inventory.Items.Accessories
{
    public class Shackle : MonoBehaviour, IAccessory
    {
        public bool Equipped;

        [SerializeField] private int Armor;


        public void Equip()
        {
            Debug.Log("Equiped!");
        }
        
        
        
        public void UnEquip()
        {
            Debug.Log("Unequiped!");
        }
    }
}