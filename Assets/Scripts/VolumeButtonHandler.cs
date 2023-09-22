using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeButtonHandler : MonoBehaviour
{
    [SerializeField] public GameObject volumeButton;
    GameManager gameManager;

    public Sprite volumeOn;
    public Sprite volumeOff;

    private bool IsSoundOn = true;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    void Update()
    {
        IsSoundOn = gameManager.GetIsSoundOn();
        if (!IsSoundOn)
        {

            volumeButton.GetComponent<Image>().sprite = volumeOff;
        }
        else
        {
            volumeButton.GetComponent<Image>().sprite = volumeOn;

        }
    }
    


}
