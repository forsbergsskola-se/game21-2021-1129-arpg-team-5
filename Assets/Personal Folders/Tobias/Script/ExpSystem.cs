
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
        public int maxExp = 25;
        public float updatedExp = 0;
        public Image ExpBar;
        
        public float expIncreasedPerSecond = 0.01f;
        public int expChunksIncreasedAtOnce = 1;
        public int playerLevel = 1;
        public Text levelText;
        
        // Updates level constantly with checks
        void Update()
        {
            ExpBar.fillAmount = updatedExp / maxExp;
            levelText.text = $"LEVEL {playerLevel}";

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
            StartCoroutine(Wait(ExpValue, expChunksIncreasedAtOnce, expIncreasedPerSecond));
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