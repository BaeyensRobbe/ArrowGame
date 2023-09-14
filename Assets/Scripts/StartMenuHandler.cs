using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuHandler : MonoBehaviour
{
    public GameObject startMenu;
    private bool gameIsPaused = false;
    private GameObject pauseElement;
    // Start is called before the first frame update
    void Start()
    {
        startMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        pauseElement = GameObject.Find("Paused");
        if (pauseElement == null)
        {
            if (Time.timeScale == 0f)
            {
                UnityEngine.Debug.Log("Time scale is 0");
                startMenu.SetActive(true);
            }
            else
            {
                startMenu.SetActive(false);
            }
        }
        
        UnityEngine.Debug.Log(Time.timeScale);
    }

    /*public void TogglePause()
    {
        if (gameIsPaused)
        {
            gameIsPaused = false;
        }
        else
        {
            gameIsPaused = true;
        }
    }*/
}
