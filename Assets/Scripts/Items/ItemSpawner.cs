using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private LootTable lootTable;

    public void SpawnItem()
    {
        var item = Instantiate(lootTable.GetRandomItem());
        var spawnedPickup = item.SpawnPickup(transform.position, 1);
        spawnedPickup.transform.position = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position + Vector3.up * 1, 0.3f);
    }
}
