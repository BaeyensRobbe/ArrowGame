using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void GameOver()
    {
        // This function is called when the game is over
        SceneManager.LoadScene("GameOverScene"); // Replace with your game over scene's name
    }
}
