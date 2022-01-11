using System;
using JetBrains.Annotations;
using Team5.Control;
using Team5.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

namespace Team5.Ui
{
    public class HUD : MonoBehaviour
    {
        // HUD UI
        public GameObject MoneyUI;
        public GameObject ShopUI;
        public TMP_Text CoinText;
        public TMP_Text EggUiText;
        public TMP_Text KillCountText;
        public TMP_Text LvlText;
        public GameObject InventoryMenu;
        public GameObject HealthBar;
        public GameObject ExpBar;
        public GameObject PlayerIcon;
        
        //Menu UI
        public GameObject PauseMenu;
        public GameObject OptionsMenu;
        public GameObject MainMenu;
        private Button MainMenuButton;
        public GameObject ControlMasterMenu;

        // Shop UI
        public GameObject ShopText;
        public TMP_Text ShopDialogue;
        public GameObject NPCName;
        public Button Button1;
        public Button Button2;
        public Button Button3;
        public Button ContinueButton;
        public Button RepeatButton;
        public Image DialogueHeadNPC;
        
        // Overlays
        public GameObject overlayController;

        private void Awake()
        {
            MainMenuButton = MainMenu.GetComponent<Button>();
        }

        private void Update()
        {
            // Press Escape key to de/activate Pause Menu

            var sceneController = FindObjectOfType<SceneController>();

            if (Input.GetKeyDown(KeyCode.Escape) && PauseMenu.activeInHierarchy == false && OptionsMenu.activeInHierarchy == false)
            {
                PauseMenu.SetActive(true);
            }

            else if ((Input.GetKeyDown(KeyCode.Escape) && PauseMenu.activeInHierarchy))
            {
                PauseMenu.SetActive(false);
                sceneController.ResumeGame();
            }
            
            else if ((Input.GetKeyDown(KeyCode.Escape) && OptionsMenu.activeInHierarchy))
            {
                sceneController.OptionsClose();
            }

            // Pauses and resumes game if pause menu active

            if (PauseMenu.activeInHierarchy)
            {
                sceneController.PauseGame();
            }
        }

        public void ControlToggle()
        {
            if (ControlMasterMenu.activeInHierarchy)
            {
                ControlMasterMenu.SetActive(false);
            }
            else
            {
                ControlMasterMenu.SetActive(true);
            }
        }

        public void HudUIActive(bool overlays, bool inventory, bool playerIcon, bool exp, 
            bool health, bool moneyUI, bool shopUI, bool dialogueBox)
        
        {
            overlayController.SetActive(overlays);
            PlayerIcon.SetActive(playerIcon);
            HealthBar.SetActive(health);
            ExpBar.SetActive(exp);
            MoneyUI.SetActive(moneyUI);
            ShopUI.SetActive(shopUI);
            ShopText.SetActive(dialogueBox);
            InventoryMenu.SetActive(inventory);
        }
    }
} 