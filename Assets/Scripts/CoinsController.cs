using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class CoinsController : MonoBehaviour
{
    public UnityEngine.UI.Text coinText;
    
    private int coinCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        coinCount = PlayerPrefs.GetInt("Coins", 0);
        coinText.text = coinCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementCoinCount(int amount)
    {
        /*UnityEngine.Debug.Log("Incrementing coin count by " + amount);*/
        coinCount += amount;
        PlayerPrefs.SetInt("Coins", coinCount);
        coinText.text =  coinCount.ToString();
    }

}
