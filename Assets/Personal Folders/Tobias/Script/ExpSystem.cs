
using System.Collections;
using Team5.Control;
using Team5.Core;
using Team5.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Team5.Ui.ExpSystem
{
    public class ExpSystem : MonoBehaviour
    {
        public int playerLevel = 1;
        public int maxExp = 25;
        public float updatedExp = 0f;
        public Image ExpBar;
        
        public float expAddPerSec = 0.01f;
        public int expChunksAddAtOnce = 1;
        public TMP_Text levelText;
        
        

        // Updates level constantly with checks
        // Note: make sure player level != 0 in inspector or will increase exponentially
        void Update()
        {
            ExpBar.fillAmount = updatedExp / maxExp;
            levelText.text = $"LVL {playerLevel}";

            if (updatedExp >= maxExp)
            {
                playerLevel++;
                updatedExp = 0;
                maxExp += maxExp;
            }
        }
        
        // how we add exp
        public void ExpGain(int ExpValue)
        {
            StartCoroutine(Wait(ExpValue, expChunksAddAtOnce, expAddPerSec));
            //updatedExp += ExpValue;
        }
        
        // exp increments over time instead of in chunks
        IEnumerator Wait(int total, int addedValueChunk, float time)
        {
            for (var i= 0; i<total; i++)
            {
                updatedExp += addedValueChunk;
                yield return new WaitForSeconds(time);
            }
        }
        
        // methods for other scripts to call and add exp
        public void DefaultKillExp(int DefaultKillXp)
        {
            ExpGain(DefaultKillXp);
        }

        public void DestroyExp(int DestroyXp)
        {
            ExpGain(DestroyXp);
        }

    }

}