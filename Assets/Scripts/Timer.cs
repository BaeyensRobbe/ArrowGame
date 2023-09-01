using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Timer : MonoBehaviour
{
    public float time = 0f;
    public UnityEngine.UI.Text timeText;
    void Update()
    {
        time += Time.deltaTime;
        timeText.text = Mathf.Floor(time).ToString();
    }
}