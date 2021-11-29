using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team5.Core
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        private void LateUpdate()
        {
            transform.position = target.position;
        }

    }
}
