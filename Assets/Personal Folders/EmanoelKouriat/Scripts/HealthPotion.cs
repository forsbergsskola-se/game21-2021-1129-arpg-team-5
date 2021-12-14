using UnityEngine;
using Team5.Entities.Player;

namespace team5.Inventory.Items.Consumables
{
    public class HealthPotion : MonoBehaviour, IConsumable
    {
        private GameObject player;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
        }
        public void Consume()
        {
            
            //TODO: Potion gives max hp + 10
            player.GetComponent<PlayerController>().AddHealth(50);
            Debug.Log("+50 HP");
        }
    }
}