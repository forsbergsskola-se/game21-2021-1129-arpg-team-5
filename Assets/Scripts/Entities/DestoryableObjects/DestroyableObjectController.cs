using UnityEngine;
using Team5.Core;
using Team5.EntityBase;

namespace Team5.Entities.Objects.DestroyableObject
{
    public class DestroyableObjectController: Entity, IInteractable
    {
        public Texture2D mouseTexture=> cursorTexture;
        public Texture2D cursorTexture;
        public GameObject explosion;
        public ParticleSystem SmokeSystem;
        
        private GameObject player;
        
        
        
        private void Start()
        {
            player= GameObject.FindWithTag("Player");
            Vector3.Distance(player.transform.position, transform.position);
        }
    
        
        
        public void OnClick(Vector3 mouseClickVector)
        {
            // //TODO If distance between the object are to big you cant run the animation, You start to run closer to the vase to destroy it.
            //
            // //TODO Explosion Animation Run. 
            //
            // //TODO Idle After the Explosion should run.
            // //TODO Explosion if it happened and its on idle after explosion you can walk though ruble.   
            //
            // gameObject.SetActive(false);
            // Instantiate(SmokeSystem, transform.position, transform.rotation);
            // Instantiate(explosion, transform.position, transform.rotation);
        }
    
        
        
        protected override void OnDeath()
        {
            // base.OnDeath();
            gameObject.SetActive(false);
            Instantiate(SmokeSystem, transform.position, transform.rotation);
            Instantiate(explosion, transform.position, transform.rotation);
            IsDead = true;
        }
    
    
        
        public void OnHover()
        {
            
        }
    }
}