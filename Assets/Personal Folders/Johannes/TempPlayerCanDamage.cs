using Team5.Entities;
using UnityEngine;

public class TempPlayerCanDamage : MonoBehaviour
{
    
    public GameObject Enemies;
    // Update is called once per frame
    void Update()
    {
        transform.position= Vector3.MoveTowards(transform.position, Enemies.transform.position, 0.003f);
    }

    private void OnCollisionStay(Collision other)
    {
        // Debug.Log("hej");
        other.gameObject.GetComponent<Entity>().TakeDamage(10);
        
    }
}
