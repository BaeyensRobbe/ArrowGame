using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoinsMeshController : MonoBehaviour
{
    public TextMeshProUGUI coinTextMesh;
    void Start()
    {
        coinTextMesh.text = PlayerPrefs.GetInt("Coins", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        coinTextMesh.text = PlayerPrefs.GetInt("Coins", 0).ToString();
    }
}
