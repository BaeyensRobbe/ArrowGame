using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Sprite chainsawSprite;
    public SpriteRenderer playerSpriteRenderer; // Reference to the player's sprite renderer
    private Sprite originalPlayerSprite; // Store the original player sprite
    private float chainsawRotationSpeed = 90f; // Rotation speed in degrees per second

    public CircleCollider2D largerCollider;

    private void Start()
    {
        // Store the original player sprite
        originalPlayerSprite = playerSpriteRenderer.sprite;
        largerCollider.enabled = false;
    }

    // Call this method to change the player's sprite to the chainsaw sprite for a specified duration
    public void spriteToChainsaw(float powerUpTime)
    {
        StartCoroutine(ChangeSpriteAndRotateForDuration(powerUpTime));
    }

    private IEnumerator ChangeSpriteAndRotateForDuration(float duration)
    {
        // Enable the chainsaw sprite renderer to show the chainsaw sprite
        playerSpriteRenderer.sprite = chainsawSprite;
        largerCollider.enabled = true;

        // Calculate the total rotation angle based on the duration and rotation speed
        float totalRotation = 0f;
        while (totalRotation < 360f) // Rotate for a full circle (360 degrees)
        {
            // Calculate the rotation step for this frame
            float rotationStep = chainsawRotationSpeed * Time.deltaTime;

            // Rotate the player (chainsaw)
            transform.Rotate(Vector3.forward, rotationStep);

            // Update the total rotation angle
            totalRotation += rotationStep;

            yield return null; // Wait for the next frame
        }

        

        // Wait for the specified duration (excluding rotation time)
        float remainingDuration = duration - (360f / chainsawRotationSpeed);
        if (remainingDuration > 0f)
        {
            yield return new WaitForSeconds(remainingDuration);
        }

        largerCollider.enabled = false;

        // Revert back to the original player sprite and reset rotation
        playerSpriteRenderer.sprite = originalPlayerSprite;
        transform.rotation = Quaternion.identity; // Reset rotation
    }
}
