using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    ButtonManager buttonManager;

    public void ChangeToShopScene()
    {
        SceneManager.LoadScene("Shop");
    }


    public void ChangeToGameScene()
    {
        SceneManager.LoadScene("Game");
        
    }
}
