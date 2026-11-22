using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameObject GameWinPanel;
    public TextMeshProUGUI GameEndText;
    [SerializeField] private GameObject dockText;
    [SerializeField] private GameObject interactText;
    public Button NextLevelButton;
    public Button RestartButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gameObject.SetActive(true);
        GameWinPanel.SetActive(false);
        dockText.SetActive(false);
        interactText.SetActive(false);
        

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void gameEnd(bool isVictory)
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        GameWinPanel.SetActive(true);
        GameEndText.text = isVictory ? "Victory" : "Game Over";
        NextLevelButton.interactable = isVictory;
        RestartButton.interactable = !isVictory;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnDockInteraction()
    {
        gameObject.SetActive(true);
        dockText.SetActive(true);
    }

    public void OnDockEndInteraction()
    {
        gameObject.SetActive(false);
        dockText.SetActive(false);
    }

    public void OnInteract()
    {
        Debug.Log("INTERACT HUD ON");
        gameObject.SetActive(true);
        interactText.SetActive(true);
    }
    public void OnInteractEnd()
    {
        Debug.Log("INTERACT HUD OFF");
        gameObject.SetActive(false);
        interactText.SetActive(false);
    }

}
