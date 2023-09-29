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

    private void Start()
    {
        characterStartPosition = transform.position;
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
            Vector2 offsetLast = touchCurrentPos - lastPosition;

            // Calculate the angle of movement and apply it as rotation
            if (offsetLast != Vector2.zero)
            {
                float angle = Mathf.Atan2(offsetLast.y, offsetLast.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            }

            Vector2 newPosition = characterStartPosition + offset;
            lastPosition = transform.position;
            transform.position = newPosition;
        }
    }
}
