using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class TimerScript : MonoBehaviour
{
    public float time = 0f;
    private float score = 0f;
    public UnityEngine.UI.Text timeText;
    GameManager gamemanager;

    void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        time += Time.deltaTime;
        /*timeText.text = Mathf.Floor(time).ToString();*/
        score += Time.deltaTime;
        timeText.text = Mathf.Floor(score).ToString();
    }

    public void ResetTimer()
    {
        gamemanager.UpdateHighScore((int)score);
        PlayerPrefs.SetInt("CurrentScore", (int)score);
        score = 0f;
    }

    

    
}