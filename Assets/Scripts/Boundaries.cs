using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 screenBounds;
    private float headerHeight;
    private float playerWidth;
    private float playerHeight;
    public RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        headerHeight = rectTransform.rect.height / 100;
        playerWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        playerHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, (screenBounds.x - playerWidth) * -1, screenBounds.x - playerWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, (screenBounds.y - playerHeight) * -1 , screenBounds.y - headerHeight - playerHeight);
        transform.position = viewPos;
    }
}
