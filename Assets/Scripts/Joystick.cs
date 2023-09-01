using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;
    private float minX, maxX, minY, maxY;

    

    public float buffer = 0.1f;

    private SpriteRenderer spriteRenderer;

    /*public Transform circle;
    public Transform outerCircle;*/

    void Start()
    {
        spriteRenderer = player.GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

            /*circle.transform.position = pointA * -1;
            outerCircle.transform.position = pointA * -1;
            circle.GetComponent<SpriteRenderer>().enabled = true;
            outerCircle.GetComponent<SpriteRenderer>().enabled = true;*/
        }
        if (Input.GetMouseButton(0))
        {
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        else
        {
            touchStart = false;
        }

        CalculateCameraBoundaries();

    }
    private void FixedUpdate()
    {
        if (touchStart)
        {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            moveCharacter(direction);

            
        }
        

    }

    void moveCharacter(Vector2 direction)
    {
        Vector3 newPosition = player.position + new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        player.position = newPosition;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the sprite
        spriteRenderer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void CalculateCameraBoundaries()
    {
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        minX = bottomLeft.x + buffer;
        maxX = topRight.x - buffer;
        minY = bottomLeft.y + buffer;
        maxY = topRight.y - buffer;
    }

    
}