using UnityEngine;
using Team5.Entities.Player;

namespace Team5.Inventory.Items.Accessories
{
    public class Shackle : MonoBehaviour, IAccessory
    {
        public bool Equipped;

        private PlayerController player;
        [SerializeField] private int healthValue = 10;

        private void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        public AccessoryController oldSlot { get; set; }

        public void Equip()
        {
            Debug.Log("Equiped!");
            
            player.MaxHealth += healthValue;
            player.AddHealth(healthValue);
            
            // Debug.Log("new test equip" + player.Health);
        }
        
        
        
        public void UnEquip()
        {
            Debug.Log("Unequiped!");

            player.MaxHealth -= healthValue;
            player.TakeDamage(healthValue);

            // Debug.Log("new test unequip" + player.Health);
        }
    }
}