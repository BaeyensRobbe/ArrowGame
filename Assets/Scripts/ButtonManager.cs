using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private int[] Characters;
    public GameObject[] PlayButtons;
    public GameObject[] PriceButtons;
    SceneChanger sceneManager;

    public GameObject rocketPriceButton;
    public GameObject rocketPlayButton;

    public GameObject ufoPriceButton;
    public GameObject ufoPlayButton;

    public GameObject doubleCoinsPriceButton;
    public GameObject doubleCoinsPlayButton;

    public GameObject spaceShipPriceButton;
    public GameObject spaceShipPlayButton;


    // Start is called before the first frame update
    void Start()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneChanger>();
        checkRocketButtons();
        checkSpaceShipButtons();
        checkUfoButtons();
        checkDoubeCoinsButtons();

        


        /*GameObject[] playButtons = GameObject.FindGameObjectsWithTag("Play");
        GameObject[] priceButtons = GameObject.FindGameObjectsWithTag("Price");
        if (playButtons != null)
        {
            UnityEngine.Debug.Log("PlayButtons not null");
            UnityEngine.Debug.Log(playButtons[0]);
            UnityEngine.Debug.Log(playButtons.Length);
        }


        

        for (int i = 0; i < priceButtons.Length; i++)
        {
            string parentName = priceButtons[i].transform.parent.name;
            int available = PlayerPrefs.GetInt(parentName, 0);
            string playButtonName = string.Concat(parentName, "Play");
            string priceButtonName = string.Concat(parentName, "Price");
            GameObject playButton = GameObject.Find(playButtonName);
            GameObject priceButton = GameObject.Find(priceButtonName);
            UnityEngine.Debug.Log(playButtonName);
            UnityEngine.Debug.Log(priceButtonName);
            if (available == 0)
            {
                UnityEngine.Debug.Log("Available is 0");
               
                
                priceButton.SetActive(true);
                playButton.SetActive(false);
            }
            else
            {
                UnityEngine.Debug.Log("Available is 1");
                priceButton.SetActive(false);
                playButton.SetActive(true);
            }
        }*/
        


        /*int arrayLength = 5;
        Characters = new int[arrayLength];

        Characters[0] = 1;

        Characters[1] = PlayerPrefs.GetInt("SpaceShip", 0);*/

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseDefault()
    {
        PlayerPrefs.SetInt("Skin", 0);
        sceneManager.ChangeToGameScene();
    }
    public void UseSpaceShip()
    {
        PlayerPrefs.SetInt("Skin", 1);
        sceneManager.ChangeToGameScene();
    }
    public void UseRocket()
    {
        PlayerPrefs.SetInt("Skin", 2);
        sceneManager.ChangeToGameScene();
    }
    public void UseUfo()
    {
        PlayerPrefs.SetInt("Skin", 3);
        sceneManager.ChangeToGameScene();
    }
    public void UseDoubleCoins()
    {
        PlayerPrefs.SetInt("Skin", 4);
        sceneManager.ChangeToGameScene();
    }


    public void buySpaceShip(int price)
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        if (coins >= price)
        {
            PlayerPrefs.SetInt("Coins", coins - price);
            PlayerPrefs.SetInt("SpaceShip", 1);
            PlayButtons[0].SetActive(true);
            PriceButtons[0].SetActive(false);
        }
        else
        {
            UnityEngine.Debug.Log("Not enough coins");
        }
        PlayerPrefs.Save();
    }
    public void buyRocket(int price)
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        if (coins >= price)
        {
            PlayerPrefs.SetInt("Coins", coins - price);
            PlayerPrefs.SetInt("Rocket", 1);
            PriceButtons[1].SetActive(false);
            PlayButtons[1].SetActive(true);
        }
        else
        {
            UnityEngine.Debug.Log("Not enough coins");
        }
        PlayerPrefs.Save();
    }

    public void buyUfo(int price)
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        if (coins >= price)
        {
            PlayerPrefs.SetInt("Coins", coins - price);
            PlayerPrefs.SetInt("Ufo", 1);
            PriceButtons[2].SetActive(false);
            PlayButtons[2].SetActive(true);
        }
        else
        {
            UnityEngine.Debug.Log("Not enough coins");
        }
        PlayerPrefs.Save();
    }

    public void buyDoubleCoins(int price)
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        if (coins >= price)
        {
            PlayerPrefs.SetInt("Coins", coins - price);
            PlayerPrefs.SetInt("DoubleCoins", 1);
            PriceButtons[3].SetActive(false);
            PlayButtons[3].SetActive(true);
        }
        else
        {
            UnityEngine.Debug.Log("Not enough coins");
        }
        PlayerPrefs.Save();
    }


    public void reset()
    {
        PlayerPrefs.SetInt("Coins", 10000);
        PlayerPrefs.SetInt("SpaceShip", 0);
        PlayerPrefs.SetInt("Rocket", 0);
        PlayerPrefs.SetInt("Ufo", 0);
        PlayerPrefs.SetInt("DoubleCoins", 0);

        foreach (GameObject button in PlayButtons)
        {
            button.SetActive(false);
        }
        foreach (GameObject button in PriceButtons)
        {
            button.SetActive(true);
        }
    }

    private void checkRocketButtons()
    {
        if (PlayerPrefs.GetInt("Rocket", 0) == 1)
        {
            rocketPriceButton.SetActive(false);
            rocketPlayButton.SetActive(true);
        }
        else
        {
            rocketPriceButton.SetActive(true);
            rocketPlayButton.SetActive(false);
        }
    }

    private void checkSpaceShipButtons()
    {
        if (PlayerPrefs.GetInt("SpaceShip", 0) == 1)
        {
            spaceShipPriceButton.SetActive(false);
            spaceShipPlayButton.SetActive(true);
        }
        else
        {
            spaceShipPriceButton.SetActive(true);
            spaceShipPlayButton.SetActive(false);
        }
    }

    private void checkUfoButtons()
    {
        if (PlayerPrefs.GetInt("Ufo", 0) == 1)
        {
            ufoPriceButton.SetActive(false);
            ufoPlayButton.SetActive(true);
        }
        else
        {
            ufoPriceButton.SetActive(true);
            ufoPlayButton.SetActive(false);
        }
    }

    private void checkDoubeCoinsButtons()
    {
        if(PlayerPrefs.GetInt("DoubleCoins", 0) == 1)
        {
            doubleCoinsPriceButton.SetActive(false);
            doubleCoinsPlayButton.SetActive(true);
        }
        else
        {
            doubleCoinsPriceButton.SetActive(true);
            doubleCoinsPlayButton.SetActive(false);
        }
    }
}
