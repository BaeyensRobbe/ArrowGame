using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterRotation : MonoBehaviour
{
    private Transform characterTransform;
    private Vector3 targetDirection;
    void Start()
    {
        characterTransform = GetComponent<Transform>();
    }
    void Update()
    {
        // Get the direction the character is moving
        targetDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // Rotate the character towards the target direction
        characterTransform.LookAt(characterTransform.position + targetDirection);
    }
}
