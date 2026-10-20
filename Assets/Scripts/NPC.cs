using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class NPC : MonoBehaviour
{
    public bool player_detected = false; //is player close enough to the npc to interact
    public GameObject dialoguePanel;
    public TMP_Text text;

    public DialogueController dialogueController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        dialoguePanel.SetActive(false);
    }
    void Start()
    {
        text.gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Talk()
    {
        dialoguePanel.SetActive(true);
        text.gameObject.SetActive(false);
        dialogueController.ShowNextPage();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            player_detected = true;
            if (!dialoguePanel.activeSelf)
                //If Dialogue has already started then the guide text won't appear
            {
                text.gameObject.SetActive(true);
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            player_detected = false;
            text.gameObject.SetActive(false);
            dialoguePanel.SetActive(false);
        }
       
    }
}
