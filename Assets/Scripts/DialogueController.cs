using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class DialogueController : MonoBehaviour
{
    [SerializeField] private TMP_Text npcName;
    [SerializeField] private TMP_Text dialogueText;
    
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button skipButton;

    [SerializeField] private float typingSpeed = 0.03f;
    [SerializeField] private Dialogue[] dialoguePages;
    
    private bool isTyping = false;
    private int currentPage = 0;
    private Coroutine typingCoroutine;
    

// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextButton.onClick.AddListener(OnNextPressed);
        prevButton.onClick.AddListener(OnPrevPressed);
        skipButton.onClick.AddListener(OnSkipPressed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNextPage()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        
        typingCoroutine =
            StartCoroutine(TypeText(dialoguePages[currentPage].dialogueText));
    }
    

    
    private void OnNextPressed()
    {
        //If the dialogue text is still appearing, then pressing >> will make the whole text appear all at once
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);

            dialogueText.text = dialoguePages[currentPage].dialogueText;
            isTyping = false;
            
        }
        
        //If there are no more dialogue text pages to show then current pages doesn't increase
        if (currentPage >= dialoguePages.Length)
        {
            Debug.Log("End of dialogue");
            return;
        }
        
        currentPage++;
        ShowNextPage();
    }

    private void OnPrevPressed()
    {
        if (currentPage == 0)
        {
            Debug.Log("Start of dialogue");
            return;
        }
        currentPage--;
        ShowNextPage();
        
    }

    private void OnSkipPressed()
    {
        //Right now takes the dialogue to the last possible panel
        //TODO: ChoicePanel to choose whether to start the task or not
        if (currentPage ==  dialoguePages.Length -1)
        {
            Debug.Log("End of dialogue");
            return;
        }

        currentPage = dialoguePages.Length - 1;//index of last possible dialogue panel
        ShowNextPage();

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
