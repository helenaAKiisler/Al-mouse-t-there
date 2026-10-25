using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class DialogueController : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    
    
    [Header("Dialogue buttons")]
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button skipButton;
    
    [Header("Choice buttons")]
    [SerializeField] private Button questButton;
    [SerializeField] private Button quitButton;
    
    
    [SerializeField] private Dialogue[] dialoguePages;
    
    [TextArea]
    [SerializeField] private string returnText =
        "Oh, you're back. Are you ready to help me now?";
    
    [TextArea]
    [SerializeField] private string questCompletedText =
        "Thank you for your help!";
    
    private NPC currentNPC;
    private int currentPage;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private float typingSpeed = 0.03f;
    public bool IsOpen => dialoguePanel.activeSelf;

    private enum DialogueMode
    {
        Dialogue,
        QuestChoice
    }
    
    private DialogueMode currentMode;
// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialoguePanel.SetActive(false);
        
        nextButton.onClick.AddListener(OnNextPressed);
        prevButton.onClick.AddListener(OnPrevPressed);
        skipButton.onClick.AddListener(OnSkipPressed);
        
        questButton.onClick.AddListener(OnQuestAccepted);
        quitButton.onClick.AddListener(OnQuestDeclined);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartDialogue(NPC npc)
    {
        currentNPC = npc;
        currentPage = 0;
        dialoguePanel.SetActive(true);
        ShowDialogueButtons();
        DisplayCurrentPage();
    }

    public void ShowQuestionAgain(NPC npc)
    {
        currentNPC = npc;
        dialoguePanel.SetActive(true);
        currentMode = DialogueMode.QuestChoice;
        ShowChoiceButtons();
        StartCoroutine(TypeText(returnText));
    }
    
    //Quest dialoog?
    public void ShowCompletedQuestDialogue(NPC npc)
    {
        currentNPC = npc;
        dialoguePanel.SetActive(true);
        currentMode = DialogueMode.Dialogue;
        StartCoroutine(TypeText(questCompletedText));
    }

    private void DisplayCurrentPage()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(
            dialoguePages[currentPage].dialogueText));
    }
    

    
    private void OnNextPressed()
    {
        //If the dialogue text is still appearing, then pressing >> will make the whole text appear all at once
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);

            dialogueText.text = 
                dialoguePages[currentPage].dialogueText;
            
            isTyping = false;

            return;
        }
        
        //If there are no more dialogue text pages to show then current pages doesn't increase
        if (currentPage >= dialoguePages.Length - 1)
        {
            currentNPC.questState = QuestState.WaitingForAnswer;
            OpenQuestChoice();
            return;
        }
        
        currentPage++;
        DisplayCurrentPage();
    }

    private void OnPrevPressed()
    {
        if (currentMode == DialogueMode.QuestChoice)
        {
            currentMode = DialogueMode.Dialogue;

            ShowDialogueButtons();
            
            currentPage = dialoguePages.Length - 1;
            
            DisplayCurrentPage();

            return;
        }

        if (currentPage == 0)
            return;
        
        currentPage--;
        DisplayCurrentPage();
        
    }

    private void OpenQuestChoice()
    {
        currentMode = DialogueMode.QuestChoice;
        ShowChoiceButtons();
        
        typingCoroutine = StartCoroutine(
            TypeText("Would you like to help me?"));
        

    }

    private void OnQuestAccepted()
    {
        currentNPC.AcceptQuest();
        CloseDialogue();
    }

    private void OnQuestDeclined()
    {
        currentNPC.DeclineQuest();
        CloseDialogue();
    }
    
    private void ShowDialogueButtons()
    {
        nextButton.gameObject.SetActive(true);
        prevButton.gameObject.SetActive(true);
        skipButton.gameObject.SetActive(true);

        questButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }
    
    private void ShowChoiceButtons()
    {
        nextButton.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);

        prevButton.gameObject.SetActive(true);

        questButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }
    
    private void HideAllButtons()
    {
        nextButton.gameObject.SetActive(false);
        prevButton.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);

        questButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }
    public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
    }
    
    private void OnSkipPressed()
    {
        OpenQuestChoice();
    }   
    //Function for typing text gradually
     private IEnumerator TypeText(string text)
     {
         
         isTyping = true;
         dialogueText.text = "";
         
         foreach (char c in text)
         {
             //Text appearing by character, pauses after sentences ending
             dialogueText.text += c;
             if (c == '.' || c == '!' || c == '?')
             {
                 yield return new WaitForSeconds(0.5f);
             }
             else
             {
                 yield return new WaitForSeconds(typingSpeed);
             }
         }
 
         isTyping = false;
         
     }
}
