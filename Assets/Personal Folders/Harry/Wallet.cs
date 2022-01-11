using System.Collections;
using System.Collections.Generic;
using Team5.Entities;
using Team5.Ui;
using TMPro;
using UnityEngine;

namespace Team5.Ui
{
    public class Wallet : MonoBehaviour
    {
        private TMP_Text CoinCounter;
        public int Coins { get; private set; } = 0;
        public int BronzeCoins { get; private set; } = 0;
        public int SilverCoins { get; private set; } = 0;
        public int GoldCoins { get; private set; } = 0;

        public int BronzeValue = 1;
        public int SilverValue = 5;
        public int GoldValue = 10;

        void Start()
        {
            CoinCounter = FindObjectOfType<HUD>().CoinText;
        }

        void Update()
        {
            if (Coins > 0)
            {
                CoinCounter.text = "" + Coins;
            }

            else if (Coins <= 0)
            {
                CoinCounter.text = "0";
            }
        }

        void OnTriggerEnter(Collider other)
        {
            bool pickedUp = false;

            switch (other.gameObject.tag)
            {
                case "BronzeCoin":
                    Coins += BronzeValue;
                    BronzeCoins += 1;
                    pickedUp = true;
                    break;
                case "SilverCoin":
                    Coins += SilverValue;
                    SilverCoins += 1;
                    pickedUp = true;
                    break;
                case "GoldCoin":
                    Coins += GoldValue;
                    GoldCoins += 1;
                    pickedUp = true;
                    break;
            }

            if (pickedUp)
            {
                Debug.Log($"{this.name} Picked up: {other.gameObject.tag}");
                other.gameObject.SetActive(false);
            }
        }

        public void SubtractMoney(int amount)
        {
            Coins = (Coins - amount);
        }
    }
}
