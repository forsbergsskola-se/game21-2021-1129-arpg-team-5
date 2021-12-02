using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team5.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmoRadius = 3f;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(transform.GetChild(i).position, waypointGizmoRadius);
                
            }
            
            
        }
    }
}
