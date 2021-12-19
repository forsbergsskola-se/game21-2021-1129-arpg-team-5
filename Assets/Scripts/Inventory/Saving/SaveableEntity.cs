using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Team5.Saving
{
    /// <summary>
    /// To be placed on any GameObject that has ISaveable components that
    /// require saving.
    ///
    /// </summary>
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
   
        [Tooltip("The unique ID is automatically generated in a scene file if " + "left empty. Do not set in a prefab unless you want all instances to " + "be linked.")]
        [SerializeField] string uniqueIdentifier = "";


        static Dictionary<string, SaveableEntity> globalLookup = new Dictionary<string, SaveableEntity>();

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        /// <summary>
        /// Will capture the state of all `ISaveables` on this component and
        /// return a `System.Serializable` object that can restore this state later.
        /// </summary>
        public object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return state;
        }

        /// <summary>
        /// Will restore the state that was captured by `CaptureState`.
        /// </summary>
        /// <param name="state">
        /// The same object that was returned by `CaptureState`.
       

#if UNITY_EDITOR


        private bool IsUnique(string candidate)
        {
            if (!globalLookup.ContainsKey(candidate)) return true;

            if (globalLookup[candidate] == this) return true;

            if (globalLookup[candidate] == null)
            {
                globalLookup.Remove(candidate);
                return true;
            }

            if (globalLookup[candidate].GetUniqueIdentifier() != candidate)
            {
                globalLookup.Remove(candidate);
                return true;
            }

            return false;
        }
    }
}