using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSizer : MonoBehaviour
{
    [SerializeField]
    public GameObject container;
    public GameObject background;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float width = background.GetComponent<RectTransform>().rect.width;
        float height = background.GetComponent<RectTransform>().rect.height;
        Vector2 size = new Vector2((width) - 20, (height/4) - 8);
        container.GetComponent<GridLayoutGroup>().cellSize = size;
    }
}
