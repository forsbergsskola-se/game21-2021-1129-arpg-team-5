using System.Collections;
using System.Collections.Generic;
using Team5.Inventories;
using UnityEngine;

namespace Team5.UI.Inventories
{
    public interface IItemHolder
    {
        InventoryItem GetItem();
    }
}