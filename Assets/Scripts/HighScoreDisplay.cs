using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class HighScoreDisplay : MonoBehaviour
{
    public UnityEngine.UI.Text highScoreText;

    void Start()
    {
        highScoreText.text = "BEST " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    void Update()
    {
        highScoreText.text = "BEST " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
