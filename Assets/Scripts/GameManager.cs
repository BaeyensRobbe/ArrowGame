using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isPaused = false;
    private bool pauseButtonPressed = false;
    private GameOverManager gameOverManager;
    private Animator resumeButtonAnimator;
    private int highScore;
    StartMenuHandler startMenuHandler;
    

    void Start()
    {
        isPaused = false;
        Time.timeScale = 0f;
        gameOverManager = GameObject.Find("GameOver").GetComponent<GameOverManager>();
        resumeButtonAnimator = GameObject.Find("Paused").GetComponent<Animator>();
        startMenuHandler = GameObject.Find("StartMenu").GetComponent<StartMenuHandler>();
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        
    }

    void Update()
    {
        if (!pauseButtonPressed)
        {
            if (Time.timeScale == 0f)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Time.timeScale = 1f;
                }
            }
        }

    }

    public void UpdateHighScore(int newScore)
    {
        if (newScore > highScore)
        {
            highScore = newScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save(); // Save changes to PlayerPrefs
        }
    }

    public void TogglePause()
    {
        
        if (Time.timeScale == 0f)
        {
            isPaused = true;
        }
        else
        {
            isPaused = false;
        }
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f; // Pause or resume the game by adjusting time scale.
        
    }

    public void PauseButton()
    {
        if (!pauseButtonPressed)
        {
            pauseButtonPressed = true;
        }
        else
        {
            pauseButtonPressed = false;
        }


        Time.timeScale = pauseButtonPressed ? 0f : 1f;
        gameOverManager.pauseButton();
        



    }

    public void GameOver()
    {
        TogglePause();
       

    }

    public void RestartGame()
    {
        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator WaitForAnimation()
    {
        // Wait for 0.25 seconds
        yield return new WaitForSeconds(0.25f);

        // Continue with the rest of your code
        UnityEngine.Debug.Log("Animation waited for 0.25 seconds.");
        
    }
}
