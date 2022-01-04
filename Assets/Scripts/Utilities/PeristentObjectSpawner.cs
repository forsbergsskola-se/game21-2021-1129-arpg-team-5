using System;
using UnityEngine;

namespace Team5.Utils
{
    
    public class PeristentObjectSpawner : MonoBehaviour
    {

        [Tooltip("This prefab will only be spawned once and persisted between " + "scenes.")]
        [SerializeField] GameObject persistentObjectPrefab = null;


        static bool hasSpawned = false;

        private void Awake() {
            if (hasSpawned) return;

            SpawnPersistentObjects();

            hasSpawned = true;
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}