using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameObject GameWinPanel;
    public TextMeshProUGUI GameEndText;

    public Button NextLevelButton;
    public Button RestartButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void gameEnd(bool isVictory)
    {
        Time.timeScale = 0;
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
}
