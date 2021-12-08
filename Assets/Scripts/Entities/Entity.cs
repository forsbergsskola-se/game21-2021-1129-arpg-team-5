using System.Collections;
using Team5.Core;
using UnityEngine;

namespace Team5.EntityBase
{
    public abstract class Entity : MonoBehaviour
    {
        [HideInInspector] public float MovementSpeed;
        [HideInInspector] public float CritChance;

        [SerializeField] private int BaseMaxHealth;
        [SerializeField] private int BaseArmor;
        [SerializeField] private float BaseMovementSpeed;
        [SerializeField] private float BaseCritChance;
        [SerializeField] private float BaseDamageCooldown;
        [SerializeField] private float EntityLevelValueMuliplier;

        private bool takeDamageOnCooldown;

        private float health;
        private float armor;
        private float damageCooldownTime;
        private bool isAlive;

        protected float damageResistance;
        protected float maxHealth;
        protected float level;
        
        public bool IsDead;

        private void Awake()
        {
            maxHealth = BaseMaxHealth;
            Health = maxHealth;
            Armor = BaseArmor;
            MovementSpeed = BaseMovementSpeed;
            CritChance = BaseCritChance;
            damageCooldownTime = BaseDamageCooldown;
        }

        /// <summary>
        /// Reset the entity to its base statistics. Does not include it's level.
        /// </summary>
        public virtual void ResetEntity()
        {
            maxHealth = BaseMaxHealth;
            Health = maxHealth;
            Armor = BaseArmor;
            MovementSpeed = BaseMovementSpeed;
            CritChance = BaseCritChance;
            damageCooldownTime = BaseDamageCooldown;
        }

        /// <summary>
        /// Sets or gets the entity's health. Will set it to inactive if the health reaches 0.
        /// </summary>
        public virtual float Health
        {
            get => health;
            private set
            {
                health = value;
                if (health <= 0)
                {
                    OnDeath();
                }
            }
        }

        public virtual float Armor
        {
            get => armor;
            set => damageResistance = (100 - value) / 100;
        }
        
        /// <summary>
        /// Function called on death of a entity. Override to add additional changes on death.
        /// </summary>
        public virtual void OnDeath()
        {
            if (IsDead)
                return;
            IsDead = true;

            Debug.Log($"{name} is now dead!");
            
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        /// <summary>
        /// Get: the level, Set: the Level and scale stats accordingly. 
        /// </summary>
        public virtual float EntityLevel
        {
            get => level;
            set
            {
                var oldMultiplier = EntityLevelValueMuliplier * value;
            
                var bonusMaxHealth = maxHealth - BaseMaxHealth * oldMultiplier;
                var bonusArmor = Armor - BaseArmor * oldMultiplier;
                var bonusMovementSpeed = MovementSpeed - BaseMovementSpeed * oldMultiplier;
                var bonusCritChance = CritChance - BaseCritChance * oldMultiplier;
            
            
                var multiplier = EntityLevelValueMuliplier * value;
            
                maxHealth = BaseMaxHealth * multiplier + bonusMaxHealth;
                Armor = BaseArmor * multiplier + bonusArmor;
                MovementSpeed = BaseMovementSpeed * multiplier + bonusMovementSpeed;
                CritChance = BaseCritChance * multiplier + bonusCritChance;

                level = value;
            }
        }
        
        /// <summary>
        /// Deal damage to the Entity if it is not on cooldown and used in other Entity for example Player, GameObject and Enemies.
        /// </summary>
        /// <param name="damageTaken">Damage you deal to Entity</param>
        public virtual void TakeDamage(float damageTaken)
        {
            if (takeDamageOnCooldown) return;
        
            StartCoroutine(DamageCooldown());
            Health -= damageTaken * damageResistance;
            Debug.Log(damageTaken*damageResistance);
        }

        private IEnumerator DamageCooldown()
        {
            takeDamageOnCooldown = true;
            yield return new WaitForSeconds(damageCooldownTime);
            takeDamageOnCooldown = false;
        }
    }

}

