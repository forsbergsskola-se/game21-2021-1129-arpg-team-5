using System.Collections;
using Team5.Core;
using UnityEngine;
using UnityEngine.AI;

namespace Team5.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        [HideInInspector] public bool IsDead;
        // [HideInInspector] public float MaxHealth;
        // [HideInInspector] public float MovementSpeed;

        [SerializeField] private int BaseMaxHealth;
        [SerializeField] private int BaseArmor;
        [SerializeField] private float BaseMovementSpeed;
        [SerializeField] private float BaseDamageCooldown;
        [SerializeField] private float EntityLevelValueMuliplier;

        private float armor;
        private float damageCooldownTime;
        private float health;
        private float level = 1;
        private float maxHealth;
        private float movementSpeed;

        protected float damageResistance;
        protected bool takeDamageOnCooldown;

        

        /// <summary>
        /// Override this to set gameobjects and similar in child classes.. Then call base when you want the values to be set.
        /// </summary>
        protected virtual void Awake()
        {
            maxHealth = BaseMaxHealth;
            Health = maxHealth;
            Armor = BaseArmor;
            MovementSpeed = BaseMovementSpeed;
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
            damageCooldownTime = BaseDamageCooldown;
        }

        
        
        /// <summary>
        /// Sets or gets the entity's health. Will set it to inactive if the health reaches 0.
        /// </summary>
        public virtual float Health
        {
            get => health;
            protected set
            {
                health = Mathf.Clamp(value, 0, maxHealth);
                if (health <= 0)
                {
                    OnDeath();
                }
            }
        }



        /// <summary>
        /// Set entity maxHealth. Automatically heals/damages for the difference between new and old values.
        /// </summary>
        public float MaxHealth
        {
            get => maxHealth;
            set
            {
                var healthDifference = value - maxHealth;
                maxHealth = value;
                ModifyHealth(healthDifference);
            }
        }

        
        
        /// <summary>
        /// Sets entity armor value. Automatically scales the damage resistance too.
        /// </summary>
        public virtual float Armor
        {
            get => armor;
            set
            {
                damageResistance = (100 - value) / 100;
                armor = value;
                /*Debug.Log($"{this.name} Armor is now: {value}");
                Debug.Log($"{this.name} DamageResistance is now: {damageResistance}");*/
            }
        }



        public virtual float MovementSpeed
        {
            get => movementSpeed;
            set
            {
                movementSpeed = value;
                if (TryGetComponent(out NavMeshAgent navMeshAgent))
                {
                    navMeshAgent.speed = movementSpeed;
                }
            }
        }
        
        
        
        /// <summary>
        /// Function called on death of a entity. Override to add additional changes on death.
        /// </summary>
        protected virtual void OnDeath()
        {
            if (IsDead)
                return;
            IsDead = true;

            Debug.Log($"{name} is now dead!");

            if (TryGetComponent(out Animator animator)) animator.SetTrigger("die");
            if (TryGetComponent(out ActionScheduler actionScheduler)) actionScheduler.CancelCurrentAction();
        }

        
        
        /// <summary>
        /// Get: the level, Set: the Level and scale stats accordingly. 
        /// </summary>
        public virtual float EntityLevel
        {
            get => level;
            set
            {
                var oldMultiplier = EntityLevelValueMuliplier * level;
            
                var bonusMaxHealth = MaxHealth - BaseMaxHealth * oldMultiplier;
                var bonusArmor = Armor - BaseArmor * oldMultiplier;
                var bonusMovementSpeed = MovementSpeed - BaseMovementSpeed * oldMultiplier;


                // TODO: FINISH THIS: ----------------------------------------------------------------------------------
                // The level multiplier is set wrongly, it doubles the entire value instead of only the decimals.
                var multiplier = EntityLevelValueMuliplier * value;
            
                MaxHealth = BaseMaxHealth * multiplier + bonusMaxHealth;
                Armor = BaseArmor * multiplier + bonusArmor;
                MovementSpeed = BaseMovementSpeed * multiplier + bonusMovementSpeed;

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
            if (this.CompareTag("Player") || this.CompareTag("Enemy"))
            {
                Debug.Log(name + " took " + damageTaken*damageResistance + " damage!");
            }
            Health -= damageTaken * damageResistance;
        }

        
        
        private IEnumerator DamageCooldown()
        {
            takeDamageOnCooldown = true;
            yield return new WaitForSeconds(damageCooldownTime);
            takeDamageOnCooldown = false;
        }
        
        
        
        public void ModifyHealth(float value)
        {
            Health += value;
        }



        /// <summary>
        /// Modify entity statistics.
        /// </summary>
        /// <param name="movementSpeedMOD"></param>
        /// <param name="maxHealhMOD"></param>
        /// <param name="armorMOD"></param>
        /// <param name="multiplier">Use to subtract values or add. 1 = Add, -1 = Subtract.</param>
        public void ModifyStats(float movementSpeedMOD, float maxHealhMOD, float armorMOD, int multiplier)
        {
            if (movementSpeedMOD != 0) MovementSpeed += movementSpeedMOD * multiplier;
            if (maxHealhMOD != 0) MaxHealth += maxHealhMOD * multiplier;
            if (armorMOD != 0) Armor += armorMOD * multiplier;
        }
    }
}

