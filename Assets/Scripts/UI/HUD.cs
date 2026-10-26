using TMPro;
using UnityEngine;
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
    private void gameEnd(bool isVictory)
    {
        GameWinPanel.SetActive(true);
        GameEndText.text = isVictory ? "Victory" : "Game Over";
        NextLevelButton.interactable = isVictory;
        RestartButton.interactable = !isVictory;
    }
}
