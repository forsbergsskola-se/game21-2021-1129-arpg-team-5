using TMPro;

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
                healthText.SetText(value.ToString());
                base.Health = value;
            }
        }

        protected override void Awake()
        {
            healthText = GetComponentInChildren<TMP_Text>();
            base.Awake();
        }
    }
}

