
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
        public int maxExp;
        public float updatedExp;

        public Image ExpBar;

        //test
        public float expIncreasedPerSecond;
        public int playerLevel;
        public Text levelText;

       



        void Start()
        {
            playerLevel = 1;
            maxExp = 25;
            updatedExp = 0;
        }

        // Update is called once per frame
        void Update()
        {
            ExpBar.fillAmount = updatedExp / maxExp;

            levelText.text = playerLevel + "";

            if (updatedExp >= maxExp)
            {
                playerLevel++;
                updatedExp = 0;
                maxExp += maxExp;

            }
        }

        public void ExpGain(int ExpValue)
        {
            updatedExp += ExpValue;
        }

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