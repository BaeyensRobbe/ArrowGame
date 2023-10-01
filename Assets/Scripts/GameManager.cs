using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool isPaused = false;
    private bool pauseButtonPressed = false;
    private bool isPlaying = false;
    private bool isGameOver = false;
    private bool isStartMenu = true;

    private bool isSoundOn = true;

    private int highScore;
    public float minSwipeDistance = 50f; // Minimum distance required for a swipe

    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private Vector2 currentPosition;

    private bool isTouching;

    public AudioSource backgroundMusic;

 
    CanvasHandler canvasHandler;
    public Image freezePanel;
    ShootingProjectile shootingProjectile;
    private bool freezeAnimationPaused = false;
    
    
    

    void Start()
    {
        isGameOver = false;
        isPaused = false;
        isPlaying = false;
        isStartMenu = true;
        Time.timeScale = 0f;

        //call function to set startposition of canvasses
        
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        canvasHandler = GameObject.Find("GameManager").GetComponent<CanvasHandler>();
        shootingProjectile = GameObject.Find("Player").GetComponent<ShootingProjectile>();
        

    }

    void Update()
    {

        if (isStartMenu)
        {
            if (Time.timeScale == 0f)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    touchStartPos = Input.mousePosition;
                    isTouching = true;
                }
                

                if (Input.GetMouseButtonUp(0))
                {
                    touchEndPos = Input.mousePosition;
                    float swipeDistance = Vector2.Distance(touchStartPos, touchEndPos);

                    if (swipeDistance >= minSwipeDistance)
                    {
                        
                        UnityEngine.Debug.Log("Slight swipe detected!");
                        StartGame();
                    }
                } else if (isTouching)
                {
                    currentPosition = Input.mousePosition;
                    if (currentPosition != touchStartPos)
                    {
                        touchEndPos = Input.mousePosition;
                        float swipeDistance = Vector2.Distance(touchStartPos, touchEndPos);

                        if (swipeDistance >= minSwipeDistance)
                        {

                            UnityEngine.Debug.Log("Slight swipe detected!");
                            StartGame();
                        }
                    }
                }


                /*if (Input.GetMouseButtonDown(0))
                {
                    StartGame();
                }*/
            }
        }
        CanvasHandler();

    }

    public bool GetIsSoundOn()
    {
        return isSoundOn;
    }


    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;

        // Mute or unmute all audio in the scene
        AudioListener.pause = !isSoundOn;
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
        /*UnityEngine.Debug.Log("Animation waited for 0.25 seconds.");*/
        
    }

    public void ToStartMenu()
    {
        isGameOver = false;
        isPaused = false;
        isPlaying = false;
        isStartMenu = true;
        freezeAnimationPaused = false;
    }

    public void StartGame()
    {
        isStartMenu = false;
        isPlaying = true;
        isPaused = false;
        isGameOver = false;
        Time.timeScale = 1f;
        StartCoroutine(FadeInBackgroundMusic());
        freezeAnimationPaused = false;
    }

    public void EndGame()
    {
        isPlaying = false;
        isPaused = false;
        isGameOver = true;
        isStartMenu = false;
        Time.timeScale = 0f;
        backgroundMusic.Stop();
        freezePanel.gameObject.SetActive(false);
        shootingProjectile.AkPowerUpDeactivate();
        freezeAnimationPaused = false;

    }

    public void PauseGame()
    {
        isStartMenu = false;
        isPlaying = false;
        isPaused = true;
        isGameOver = false;
        Time.timeScale = 0f;
        backgroundMusic.Pause();
        if (freezePanel.gameObject.activeSelf)
        {
            freezePanel.gameObject.SetActive(false);
            freezeAnimationPaused = true;
        }
        
    }

    public void ResumeGame()
    {
        isStartMenu = false;
        isPlaying = true;
        isPaused = false;
        isGameOver = false;

        if (freezeAnimationPaused)
        {
            freezePanel.gameObject.SetActive(true);
        }

        Time.timeScale = 1f;
        StartCoroutine(FadeInBackgroundMusic());
        
    }

    public void CanvasHandler()
    {
        canvasHandler.HandleCanvas(isStartMenu, isPaused, isGameOver, isPlaying);
    }

    private void SetCanvasStart()
    {
        canvasHandler.SetStart();
    }

    private IEnumerator FadeInBackgroundMusic()
    {
        
        float startVolume = 0.0f;
        float endVolume = 1.0f;
        float fadeDuration = 2.0f; // Adjust the fade-in duration as needed
        float currentTime = 0.0f;

        // Start playing the background music
        backgroundMusic.Play();

        while (currentTime < fadeDuration)
        {
            currentTime += Time.unscaledDeltaTime;
            float normalizedTime = currentTime / fadeDuration;
            backgroundMusic.volume = Mathf.Lerp(startVolume, endVolume, normalizedTime);
            yield return null;
        }

        backgroundMusic.volume = endVolume; // Ensure the volume is set to the final value
    }
}
