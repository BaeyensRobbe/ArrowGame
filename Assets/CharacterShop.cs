using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShop : MonoBehaviour
{
    public Sprite[] sprites;
    private int skinInShop;

   

    // Start is called before the first frame update
    void Start()
    {
        skinInShop = PlayerPrefs.GetInt("Skin", 0);
        PlayerPrefs.SetInt("skinInShop", skinInShop);
         

    }

    // Update is called once per frame
    void Update()
    {
        if (skinInShop == 0)
        {
            gameObject.GetComponent<Image>().sprite = sprites[0];
        }
        if (skinInShop == 1)
        {
            gameObject.GetComponent<Image>().sprite = sprites[1];
        }
        if (skinInShop == 2)
        {
            gameObject.GetComponent<Image>().sprite = sprites[2];
        }
        if (skinInShop == 3)
        {
            gameObject.GetComponent<Image>().sprite = sprites[3];
        }
    }

    public void NextCharacter()
    {
        if (skinInShop == 3)
        {
            skinInShop = 0;
        }
        else
        {
            skinInShop++;
        }
        PlayerPrefs.SetInt("skinInShop", skinInShop);

    }

    public void PrevCharacter() 
    {
        if (skinInShop == 0)
        {
            skinInShop = 3;
        }
        else
        {
            skinInShop--;
        }
        PlayerPrefs.SetInt("skinInShop", skinInShop);
    }
}
