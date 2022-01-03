using UnityEngine;

namespace Team5.Inventories.Items
{
    public interface IConsumable
    {
        public bool Consume(GameObject user);
    }
}