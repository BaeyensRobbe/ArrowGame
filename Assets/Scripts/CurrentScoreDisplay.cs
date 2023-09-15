using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentScoreDisplay : MonoBehaviour
{
    public UnityEngine.UI.Text scoreText;

    void Start()
    {
        scoreText.text = "SCORE \n" + PlayerPrefs.GetInt("CurrentScore", 0).ToString();
    }

    /*void Update()
    {
        scoreText.text = "SCORE \n" + PlayerPrefs.GetInt("CurrentScore", 0).ToString();
    }*/
}
