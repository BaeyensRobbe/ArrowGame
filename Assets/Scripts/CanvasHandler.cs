using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHandler : MonoBehaviour
{

    public GameObject StartMenuCanvas;
    public GameObject GameOverMenuCanvas;
    public GameObject PausedCanvas;
    public GameObject header;
    public GameObject player;
    public GameObject Coins;
    // Start is called before the first frame update
    void Start()
    {
        StartMenuCanvas.SetActive(true);
        GameOverMenuCanvas.SetActive(false);
        PausedCanvas.SetActive(false);
    }

    

    public void HandleCanvas(bool isStartMenu, bool isPaused, bool isGameOver, bool isPlaying)
    {
        if (isPlaying)
        {
            header.SetActive(true);
            Coins.SetActive(false);
        }
        

        if (isStartMenu)
        {
            StartMenuCanvas.SetActive(true);
            Coins.SetActive(true);
            player.SetActive(true);
        }
        else
        {
            StartMenuCanvas.SetActive(false);
        }

        if (isPaused)
        {
            PausedCanvas.SetActive(true);
        }
        else
        {
            PausedCanvas.SetActive(false);
        }

        if (isGameOver)
        {
            GameOverMenuCanvas.SetActive(true);
            header.SetActive(false);
            player.SetActive(false);
            Coins.SetActive(true);
        }
        else
        {
            GameOverMenuCanvas.SetActive(false);
        }
    }

    public void SetStart()
    {
        StartMenuCanvas.SetActive(true);
        GameOverMenuCanvas.SetActive(false);
        PausedCanvas.SetActive(false);
    }
}
