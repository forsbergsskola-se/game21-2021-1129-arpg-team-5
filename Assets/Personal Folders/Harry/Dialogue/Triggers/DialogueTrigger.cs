using Team5.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour {
    
    public Dialogue dialogue;
    private DialogueManager dialogueManager;
    private Image head;
    public Sprite sprite;
    public bool purple;

    private void Awake()
    {
        head = FindObjectOfType<HUD>().DialogueHeadNPC.GetComponent<Image>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
    
    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
        head.sprite = sprite;
    }
}