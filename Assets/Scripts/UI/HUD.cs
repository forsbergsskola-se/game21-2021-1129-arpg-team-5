using JetBrains.Annotations;
using Team5.Core;
using TMPro;
using UnityEngine;


namespace Team5.Ui
{
    public class HUD : MonoBehaviour
    {
        // HUD UI
        public TMP_Text ScoreText;
        public TMP_Text ReviveText;
        public TMP_Text HealthText;
        public TMP_Text CashText;
        public TMP_Text KillCountText;
        public TMP_Text LvlText;
        public GameObject SkullHolder;
        public TMP_Text SkullCounter;
        public GameObject InventoryMenu;
        
        // Shop UI
        public GameObject ShopText;
        public TMP_Text ShopDialogue;
        public GameObject SkullIcons;
        public GameObject DialogueHeadMain;
        public GameObject DialogueHeadNPC;

            
        public void HudUIActive(bool health, bool level, bool dialogueBox, bool revives, 
                                bool killCount, bool inventory, bool skulls)
        {
            HealthText.enabled = health;
            LvlText.enabled = level;
            ShopText.SetActive(dialogueBox);

            //ScoreText.enabled = Score;
            ReviveText.enabled = revives;
            KillCountText.enabled = killCount;
            InventoryMenu.SetActive(inventory);
            SkullHolder.SetActive(skulls);
        }

        public void ShopUIActive(bool headPlayer, bool headNpc )
        {
            DialogueHeadMain.SetActive(headPlayer);
            DialogueHeadNPC.SetActive(headNpc);
        }
    }

} 