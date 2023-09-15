using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{

    private GameObject gameOvercanvas;
    private bool pauseButtonPressed = false;
    public GameObject pausedCanvas;
    // Start is called before the first frame update
    void Start()
    {
        gameOvercanvas = GameObject.Find("TapToPlay");
        pausedCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (!pauseButtonPressed)
        {
            if (Time.timeScale == 0f)
            {
                gameOvercanvas.SetActive(true);
            }
            else
            {
                gameOvercanvas?.SetActive(false);
            }
        }
        
    }

    public void pauseButton()
    {
        if (pauseButtonPressed)
        {
            pauseButtonPressed = false;
            pausedCanvas.SetActive(false);
        }
        else
        {
            pauseButtonPressed = true;
            pausedCanvas.SetActive(true);


        }
    }

    

}
