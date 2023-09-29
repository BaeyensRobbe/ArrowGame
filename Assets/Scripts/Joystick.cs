/*using System.Collections;
using System.Collections.Generic;
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

    private ChainSpawner chainspawner;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        chainspawner = GameObject.Find("PowerUpSpawner").GetComponent<ChainSpawner>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = true;
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }

        if (Input.GetMouseButton(0))
        {
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
        else
        {
            // If the joystick is not touched, stop the player's movement
            moveCharacter(Vector2.zero);
        }
    }

    void moveCharacter(Vector2 direction)
    {
        Vector3 newPosition = player.position + new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            spriteRenderer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (chainspawner.IsNotBlockedByUI(newPosition))
        {
            // Clamp the new player position to stay within camera boundaries
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
            player.position = newPosition;


        }
        else
        {
            // The new position is blocked by UI in the vertical direction, so only move horizontally
            newPosition = player.position + new Vector3(direction.x, 0, 0) * speed * Time.deltaTime;

            // Check if the new horizontal position is not blocked by UI elements
            if (chainspawner.IsNotBlockedByUI(newPosition))
            {
                // Update the player's position in the horizontal direction
                player.position = newPosition;
            }
        }
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
}*/