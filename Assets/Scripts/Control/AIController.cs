using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team5.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        float chaseDistance = 5f;
        private void Update()
        {
            if(DistanceWithPlayer() < chaseDistance)
            {
                Debug.Log(gameObject.name +" Chase starts!");
            }
            
        }

        private float DistanceWithPlayer()
        {
            GameObject player = GameObject.FindWithTag("Player");
            return Vector3.Distance(player.transform.position, transform.position);
        }
    }
}
