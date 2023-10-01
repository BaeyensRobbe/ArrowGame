using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMovement : MonoBehaviour
{
    private Vector2 lastPosition;
    private Vector2 touchStartPos;
    private Vector2 characterStartPosition;
    private bool isDragging = false;

    public float moveSpeed = 5.0f;
    public float rotationSpeed = 10.0f; // Adjust this to control the rotation speed

    private float targetAngle; // Store the target angle

    private Vector2 screenBounds;
    private float headerHeight;
    private float playerWidth;
    private float playerHeight;
    public RectTransform rectTransform;
    public float offsetMultiplier = 1.5f;

    private void Start()
    {
        characterStartPosition = transform.position;

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        headerHeight = rectTransform.rect.height / 100;
        playerWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        playerHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            characterStartPosition = transform.position;
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector2 touchCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 offset = touchCurrentPos - touchStartPos;
            Vector2 currentPos = transform.position;
            offset *= offsetMultiplier;
            Vector2 newPosition = characterStartPosition + offset;
            

            Vector3 viewPos = newPosition;
            viewPos.x = Mathf.Clamp(viewPos.x, (screenBounds.x - playerWidth) * -1, screenBounds.x - playerWidth);
            viewPos.y = Mathf.Clamp(viewPos.y, (screenBounds.y - playerHeight) * -1, screenBounds.y - headerHeight - playerHeight);
            newPosition = viewPos;

            Vector2 offsetRotation = newPosition - currentPos;

            // Calculate the angle of movement and apply it as rotation
            if (offsetRotation != Vector2.zero)
            {
                targetAngle = Mathf.Atan2(offsetRotation.y, offsetRotation.x) * Mathf.Rad2Deg;
            }

            // Apply a smooth rotation using lerp
            float currentAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
            if (currentPos != newPosition)
            {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, currentAngle));
            }

            lastPosition = transform.position;
            transform.position = newPosition;
        }
    }
}
