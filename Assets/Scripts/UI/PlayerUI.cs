using System;
using System.Collections;
using System.Net;
using Team5.Combat;
using Team5.Core;
using Team5.Entities;
using Team5.Entities.Player;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Team5.Ui
{
    public class PlayerUI : MonoBehaviour
    {
        private TMP_Text reviveText;
        private TMP_Text killText;
        private TMP_Text lvlText;
        private TMP_Text skullText;
        
        private Entity entity;
        private float healthCount;        
        private int reviveCount;
        
        public int skullCount { get; private set; } = 0;
        public int whiteSkulls { get; private set; } = 0;
        public int redSkulls { get; private set; } = 0;
        public int purpleSkulls { get; private set; } = 0;
        public int goldSkulls { get; private set; } = 0;

        private Fighter fighter;
        private int killCount;
        public int expLevel = 1;

        private void Start()
        {
            entity = this.GetComponent<Entity>();
            fighter = this.GetComponent<Fighter>();
            
            //reviveText = FindObjectOfType<HUD>().ReviveText;
            killText = FindObjectOfType<HUD>().KillCountText;
            lvlText = FindObjectOfType<HUD>().LvlText;
            //skullText = FindObjectOfType<HUD>().SkullCounter;
        }

        void Update()
        {
            reviveCount = this.GetComponent<PlayerController>().reviveCounter;
            reviveText.text = "Revivals: " + reviveCount;
            
            killText.text = "Kills: " + fighter.killCount;

            lvlText.text = "EXP LVL: " + entity.EntityLevel;

            if (skullCount > 0)
            {
                skullText.text = "" + skullCount;
            }

            else if (skullCount <= 0)
            {
                skullText.text = "0";
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            bool pickedUp = false;

            switch (other.gameObject.tag)
            {
                case "WhiteSkull":
                    skullCount += 1;
                    whiteSkulls += 1;
                    pickedUp = true;
                    break;
                case "RedSkull":
                    skullCount += 5;
                    redSkulls += 1;
                    pickedUp = true;
                    break;
                case "PurpleSkull":
                    skullCount += 10;
                    purpleSkulls += 1;
                    pickedUp = true;
                    break;
                case "GoldSkull":
                    skullCount += 50;
                    goldSkulls += 1;
                    pickedUp = true;
                    break;
            }
            
            if (pickedUp)
            {
                Debug.Log($"{this.name} Picked up: {other.tag}");
                other.gameObject.SetActive(false);
            }
        }

        // temp values - no magic numbers later
        public void AddSkulls()
        {
            StartCoroutine(Wait(22, 1, 0.2f));
        }
        
        // temp values - no magic numbers later
        public void SubtractSkulls (int skulls)
        {
            StartCoroutine(Wait(22, -1, 0.2f));
        }
        
        IEnumerator Wait(int skulls, int value2, float time )
        {
            for (var i= 0; i<skulls; i++)
            {
                skullCount += value2;
                yield return new WaitForSeconds(time);
            }
        }
    }
}