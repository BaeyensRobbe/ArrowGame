using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopButtonHandler : MonoBehaviour
{
    [Header("Buttons")]
    public GameObject PlayButton;
    public GameObject BuyButton;
    public TextMeshProUGUI BuyText;


    [Space]

    [Header("Prices")]
    public int priceSkin1;
    public int priceSkin2;
    public int priceSkin3;


    private int skinInShop;
    private int price;
    private string textOfChar;
    
    void Start()
    {

        skinInShop = PlayerPrefs.GetInt("Skin");
        
        if (PlayerPrefs.GetInt(skinInShop.ToString(), 0) > 0)
        {
            PlayButton.SetActive(true);
            BuyButton.SetActive(false);
        }
        else
        {
            PlayButton.SetActive(false);
            BuyButton.SetActive(true);
        }
        if (skinInShop == 0)
        {
            PlayButton.SetActive(true);
            BuyButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        skinInShop = PlayerPrefs.GetInt("skinInShop", 0);
        if (skinInShop == 1)
        {
            textOfChar = priceSkin1.ToString();
        }
        if (skinInShop == 2)
        {
            textOfChar = priceSkin2.ToString();
        }
        if (skinInShop == 3)
        {
            textOfChar = priceSkin3.ToString();
        }
        BuyText.text = textOfChar;


       

        if (PlayerPrefs.GetInt(skinInShop.ToString(), 0) > 0)
        {
            PlayButton.SetActive(true);
            BuyButton.SetActive(false);
        }
        else
        {
            PlayButton.SetActive(false);
            BuyButton.SetActive(true);
        }
        if (skinInShop == 0)
        {
            PlayButton.SetActive(true);
            BuyButton.SetActive(false);
        }
    }

    public void Play()
    {
        skinInShop = PlayerPrefs.GetInt("skinInShop", 0);
        PlayerPrefs.SetInt("Skin", skinInShop);
        SceneManager.LoadScene("Game");
    }

    public void Buy()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        if (skinInShop == 1)
        {
            price = priceSkin1;
            if (coins >= price)
            {
                PlayerPrefs.SetInt("Coins", coins - price);
                PlayerPrefs.SetInt("1", 1);

                PlayButton.SetActive(true);
                BuyButton.SetActive(false);

            }
            else
            {
                UnityEngine.Debug.Log("Not enough coins");
            }
            PlayerPrefs.Save();
        }
        if ( skinInShop == 2)
        {
            price = priceSkin2;
            if (coins >= price)
            {
                PlayerPrefs.SetInt("Coins", coins - price);
                PlayerPrefs.SetInt("2", 1);

                PlayButton.SetActive(true);
                BuyButton.SetActive(false);
            }
            else
            {
                UnityEngine.Debug.Log("Not enough coins");
            }
            PlayerPrefs.Save();
        }
        if (skinInShop == 3)
        {
            price = priceSkin3;
            if (coins >= price)
            {
                PlayerPrefs.SetInt("Coins", coins - price);
                PlayerPrefs.SetInt("3", 1);

                PlayButton.SetActive(true);
                BuyButton.SetActive(false);
            }
            else
            {
                UnityEngine.Debug.Log("Not enough coins");
            }
            PlayerPrefs.Save();
        }
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("1", 0);
        PlayerPrefs.SetInt("2", 0);
        PlayerPrefs.SetInt("3", 0);
        PlayerPrefs.SetInt("Coins", 20000);
    }
}
