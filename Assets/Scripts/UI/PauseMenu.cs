using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPause(InputValue value)
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);
        if (!isPaused)
        {
            Pause();
        }
        else if (isPaused)
        {
            Resume();
        }
    }

    public void Pause()
    {
        isPaused = !isPaused;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        isPaused = !isPaused;
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
