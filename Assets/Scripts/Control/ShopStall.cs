using System;
using System.Collections;
using Team5.Control;
using Team5.Core;
using Team5.Movement;
using Team5.Ui;
using TMPro;
using UnityEngine;

public class ShopStall : MonoBehaviour, IInteractable
{
    
    [SerializeField] private float distanceToShop;
    [SerializeField] private Texture2D shopCursor;
    
    private Transform playerTargetPosition;
    private Transform playerTargetPositionTwo;
    private GameObject player;
    private Vector3 TargetPosition; 
    public Texture2D mouseTexture => shopCursor;
    private GameObject ShopText;
    private GameObject ShopTalk;
    private int Skulls;
    public GameObject GoldenSkull;
    private TMP_Text Dialogue;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ShopTalk = FindObjectOfType<HUD>().ShopText;
        playerTargetPosition = transform.Find("PlayerTargetPosition").transform;
        playerTargetPositionTwo = transform.Find("PlayerTargetPositionTwo").transform;
        ShopText = this.gameObject.transform.Find("Shop Text").gameObject;
        Dialogue = FindObjectOfType<HUD>().ShopDialogue;
    }
    
    private void Update()
    {
        Skulls = player.GetComponent<PlayerUI>().skullCount;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player)
        {
            ShopTalk.SetActive(true);

            if (Skulls == 0)
            {
                Dialogue.text = "Come back with skulls!";
            }
            
            else if (Skulls == 22)
            {
                Dialogue.text = "You win the golden skull!";
                GoldenSkull.transform.position = new Vector3(239, 5, -100);
                player.GetComponent<PlayerUI>().lose22();
            }

            else if (Skulls > 0 && Skulls < 22)
            {
                Dialogue.text = $"You need {(22 - Skulls)} more skulls!";
            }

            else
            {
                Dialogue.text = "I'm fresh out of stock!";
            }
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject == player)
        {
            ShopTalk.SetActive(false);
        }
    }


    public void OnHover()
    {
        ShopText.SetActive(true);
        Debug.Log($"{ShopText.name} {ShopText.activeInHierarchy}");
    }

    void OnMouseExit()
    {
        ShopText.SetActive(false);
        Debug.Log($"{ShopText.name} {ShopText.activeInHierarchy}");
    }
    
    
    public void OnClick(Vector3 mouseClickVector)
    {
        var reachable = GameObject.Find("Player").GetComponent<Move>().TargetReachable(playerTargetPosition.position);
        var reachableTwo = GameObject.Find("Player").GetComponent<Move>().TargetReachable(playerTargetPositionTwo.position);

        if (reachable && reachableTwo)
        {
            var distance = Vector3.Distance(player.transform.position, playerTargetPosition.position);
            var distanceTwo = Vector3.Distance(player.transform.position, playerTargetPositionTwo.position);

            TargetPosition = distance < distanceTwo ? playerTargetPosition.position : playerTargetPositionTwo.position;
        }
        
        else if (reachable)
            TargetPosition = playerTargetPosition.position;
        
        else if (reachableTwo)
            TargetPosition = playerTargetPositionTwo.position;
        
        if (Vector3.Distance(player.transform.position, TargetPosition) > distanceToShop)
            GameObject.Find("Player").GetComponent<Move>().StartMoveAction(TargetPosition);
    }
}
