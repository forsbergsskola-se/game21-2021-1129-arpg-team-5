using UnityEngine;

namespace team5.Inventory.Items.Consumables
{
    public class HealthPotion : MonoBehaviour, IConsumable
    {

        public void Consume()
        {
            Debug.Log("this");
        }
    }
}