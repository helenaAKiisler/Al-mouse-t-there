
using TMPro;
using UnityEngine;


public class NPC : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    
    public bool player_detected; //is player close enough to the npc to interact

    public DialogueController dialogueController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Quest quest;
    public QuestState questState = QuestState.NotMet;
    
    void Start()
    {
        text.gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        if (!player_detected)
        {
            return;
        }
        
        text.gameObject.SetActive(false);
        switch (questState)
        {
            case QuestState.NotMet:
                StartInteraction();
                break;
            case QuestState.WaitingForAnswer:
                ShowReturnDialogue();
                break;
            case QuestState.QuestStarted:
                //quest dialoog?
                break;
            case QuestState.QuestCompleted:
                ShowCompletedQuestDialogue();
                break;
        }
    }

    private void ShowCompletedQuestDialogue()
    {
        dialogueController.ShowCompletedQuestDialogue(this);
    }

    private void StartInteraction()
    {
        dialogueController.StartDialogue(this);
    }

    private void ShowReturnDialogue()
    {
        dialogueController.ShowQuestionAgain(this);
    }
    
    public void AcceptQuest()
    {
        questState = QuestState.QuestStarted;
        Debug.Log("Quest started");
        
        if (quest != null)
            quest.StartQuest();
    }

    public void DeclineQuest()
    {
        questState = QuestState.WaitingForAnswer;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            player_detected = true;
            if (!dialogueController.IsOpen)
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
            dialogueController.CloseDialogue();
        }
       
    }
}
