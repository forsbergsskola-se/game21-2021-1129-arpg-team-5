using TMPro;
using UnityEngine;

namespace Team5.EntityBase
{
    public abstract class Enemy : Entity
    {
        // TODO: Update this with designer choice. Maybe a healthbar instead, or something else.
        private TMP_Text healthText; 
        
        public override float Health
        {
            get => base.Health;
            protected set
            {
                base.Health = value;
                healthText.SetText(Health.ToString());
                Debug.Log("Hej health texten är ändrad");
            }
        }

        protected override void Awake()
        {
            healthText = GetComponentInChildren<TMP_Text>();
            base.Awake();
        }
    }
}

