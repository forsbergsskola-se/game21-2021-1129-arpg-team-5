using JetBrains.Annotations;
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
        public GameObject SkullCounter;
        public GameObject InventoryMenu;
        
        // Shop UI
        public GameObject ShopText;
        public TMP_Text ShopDialogue;
        public GameObject SkullIcons;
        public GameObject DialogueHeadMain;
        public GameObject DialogueHeadNPC;

        
        public void HudUIActive(bool Health, bool Level, bool Score, bool Revives, 
                                bool KillCount, bool Inventory, bool Skulls)
        {
            HealthText.enabled = Health;
            LvlText.enabled = Level;
            ScoreText.enabled = Score;
            ReviveText.enabled = Revives;
            KillCountText.enabled = KillCount;
            InventoryMenu.SetActive(Inventory);
            SkullCounter.SetActive(Skulls);
        }
    }

} 