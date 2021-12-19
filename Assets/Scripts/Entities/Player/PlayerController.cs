using System.Collections;
using UnityEngine.AI;
using UnityEngine;
using Team5.Combat;
using Team5.Entities;
using Team5.Inventories.Control.sample;
using UnityEngine.EventSystems;
using System;

namespace Team5.Entities.Player
{
    public class PlayerController : Entity
    {
        [System.Serializable]
        public struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }
        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] private float healthOnRevive;
        [SerializeField] private float healthRegenPerSecond;
        [SerializeField] private float timeToRevive;

        private NavMeshAgent agent;
        private Animator animator;
        private bool stopRegen;
        
        public int reviveCounter;


        protected override void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            base.Awake();
            agent.speed = MovementSpeed;
        }
        
        
        
        void Update()
        {
            if (IsDead) 
                return;

            if (InteractWithCombat()) 
                return;

            // if (InteractWithMovement()) 
            //     return;

            if (InteractWithUI()) return;
        }
        #region UI Implementation of curser we need to review and implement it in mouse controller 
        private bool InteractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }
        #endregion


        protected override void OnDeath()
        {
            EntityLevel += 1;
            Debug.Log("Player died!");
            base.OnDeath();
            FMODUnity.RuntimeManager.PlayOneShot("event:/TempDeath");
            Revive();
        }

        public override void TakeDamage(float damageTaken)
        {
            base.TakeDamage(damageTaken);
            stopRegen = true;
        }


        private void Revive()
        {
            Debug.Log("Player reviving");
            agent.enabled = true;
            StartCoroutine(WaitToRevive());
        }

        private IEnumerator WaitToRevive()
        {
            yield return new WaitForSeconds(timeToRevive);

            Debug.Log("Player wakes up");
            
            agent.enabled = true;
            agent.ResetPath();

            Health = healthOnRevive;
            IsDead = false;
            
            animator.SetTrigger("revive");
            animator.SetBool("isDead", false);
            reviveCounter++;
            
            StartCoroutine(PlayerRegenHealth());
        }

        private IEnumerator PlayerRegenHealth()
        {
            stopRegen = false;
            
            while (Health < MaxHealth && !stopRegen)
            {
                yield return new WaitForSeconds(1);
                Health += healthRegenPerSecond;
            }

            stopRegen = false;

            Debug.Log("Player finished regen.");
        }
        
        
        

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                
                if (target == null) 
                    continue;
            
                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) 
                    continue;
            
                if (Input.GetMouseButtonDown(0)) 
                    GetComponent<Fighter>().Attack(target.gameObject);

                return true;
            }
            return false;
        }

        
        
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }



        public void AddHealth(int value)
        {
            Health += value;
        }

        public void RemoveHealth(int value)
        {
            Health -= value;
        }
    }
}