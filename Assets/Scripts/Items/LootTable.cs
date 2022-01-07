using Team5.Inventories;
using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu(menuName = ("Team5/Drops/LootTable"))]
public class LootTable : ScriptableObject
{
    [Tooltip("Loot entry.")] 
    [SerializeField] public LootEntry[] Table;

    
    /// <summary>
    /// Returns a random item based on weight from this loot table.
    /// </summary>
    /// <returns></returns>
    public InventoryItem GetRandomItem()
    {
        var totalWeight = 0;
        for (int i = 0; i < Table.Length; i++)
        {
            Table[i].rangeLow = totalWeight;
            totalWeight += Table[i].Weight;
            Table[i].rangeHigh = totalWeight;
        }

        
        var randomValue = Random.Range(0, totalWeight);
        foreach (var entry in Table)
        {
            if (entry.rangeLow <= randomValue && entry.rangeHigh > randomValue)
            {
                return entry.Item;
            }
        }

        return null;
    }



    [System.Serializable]
    public struct LootEntry
    {
        [SerializeField] public InventoryItem Item;
        [SerializeField] public int Weight;
        [HideInInspector] public int rangeHigh;
        [HideInInspector] public int rangeLow;
    }
}