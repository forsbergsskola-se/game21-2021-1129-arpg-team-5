using System;
using JetBrains.Annotations;
using Team5.Control;
using Team5.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team5.Ui
{
    public class HUD : MonoBehaviour
    {
        // HUD UI
        public TMP_Text ScoreText;
        public TMP_Text ReviveText;
        public TMP_Text CashText;
        public TMP_Text KillCountText;
        public TMP_Text LvlText;
        public GameObject SkullHolder;
        public TMP_Text SkullCounter;
        public GameObject InventoryMenu;
        public GameObject PauseMenu;
        public GameObject OptionsMenu;
        public GameObject MainMenu;
        private Button MainMenuButton;

        // Shop UI
        public GameObject ShopText;
        public TMP_Text ShopDialogue;
        public GameObject SkullIcons;
        public GameObject DialogueHeadMain;
        public GameObject DialogueHeadNPC;
        
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

        public void HudUIActive(bool overlays, bool level, bool dialogueBox, bool inventory)
        {
            overlayController.SetActive(overlays);
            LvlText.enabled = level;
            ShopText.SetActive(dialogueBox);
            //ScoreText.enabled = Score;
            InventoryMenu.SetActive(inventory);
        }
        
        public void ShopUIActive(bool headPlayer, bool headNpc )
        {
            // May need multiple heads here for converstations
            // Can be updated for shop objects UI
            DialogueHeadMain.SetActive(headPlayer);
            DialogueHeadNPC.SetActive(headNpc);
        }
    }
} 