using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [TextArea] [SerializeField] private string choiceText = "Would you like to help me?";
    [TextArea] [SerializeField] private string returnText = "Oh, you're back. Are you ready to help me now?";
    [TextArea] [SerializeField] private string questCompletedText = "Thank you for your help!";

    private enum DialogueMode { Dialogue, QuestChoice }

    private DialogueMode currentMode;
    private NPC currentNPC;
    private int currentPage;
    private bool isTyping;
    private Coroutine typingCoroutine;
    private const float TypingSpeed = 0.03f;

    public bool IsOpen => dialoguePanel.activeSelf;

    private void Start()
    {
        dialoguePanel.SetActive(false);

        nextButton.onClick.AddListener(OnNextPressed);
        prevButton.onClick.AddListener(OnPrevPressed);
        skipButton.onClick.AddListener(OpenQuestChoice);

        questButton.onClick.AddListener(OnQuestAccepted);
        quitButton.onClick.AddListener(CloseDialogue);   // declining just closes, the NPC is already "WaitingForAnswer"
    }

    
    public void StartDialogue(NPC npc)
    {
        currentNPC = npc;
        currentPage = 0;
        currentMode = DialogueMode.Dialogue;
        dialoguePanel.SetActive(true);
        ShowDialogueButtons();
        DisplayCurrentPage();
    }

    public void ShowQuestionAgain(NPC npc)
    {
        currentNPC = npc;
        currentMode = DialogueMode.QuestChoice;
        dialoguePanel.SetActive(true);
        ShowChoiceButtons();
        StartTyping(returnText);
    }

    
    public void ShowMessage(NPC npc, string message, float closeAfter = 2.5f)
    {
        currentNPC = npc;
        currentMode = DialogueMode.Dialogue;
        dialoguePanel.SetActive(true);
        HideAllButtons();

        StopTyping();
        typingCoroutine = StartCoroutine(MessageRoutine(message, closeAfter));
    }

    public void ShowCompletedQuestDialogue(NPC npc)
    {
        ShowMessage(npc, questCompletedText);
    }

    public void CloseDialogue()
    {
        StopTyping();
        dialoguePanel.SetActive(false);
    }

    
    private void OnNextPressed()
    {
        // Still typing: show the whole page at once
        if (isTyping)
        {
            StopTyping();
            dialogueText.text = dialoguePages[currentPage].dialogueText;
            return;
        }

        if (currentPage >= dialoguePages.Length - 1)
        {
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

        if (currentPage == 0) return;

        currentPage--;
        DisplayCurrentPage();
    }

    private void OnQuestAccepted()
    {
        CloseDialogue();            
        currentNPC.AcceptQuest();
    }

    private void OpenQuestChoice()
    {
        currentNPC.OfferQuest();   

        currentMode = DialogueMode.QuestChoice;
        ShowChoiceButtons();
        StartTyping(choiceText);
    }
    
    private void DisplayCurrentPage()
    {
        StartTyping(dialoguePages[currentPage].dialogueText);
    }

    private void StartTyping(string line)
    {
        StopTyping();
        typingCoroutine = StartCoroutine(TypeText(line));
    }

    private void StopTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
        isTyping = false;
    }

    private IEnumerator MessageRoutine(string message, float closeAfter)
    {
        yield return TypeText(message);
        yield return new WaitForSeconds(closeAfter);
        CloseDialogue();
    }

    private IEnumerator TypeText(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            bool sentenceEnd = c == '.' || c == '!' || c == '?';
            yield return new WaitForSeconds(sentenceEnd ? 0.5f : TypingSpeed);
        }

        isTyping = false;
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
}
