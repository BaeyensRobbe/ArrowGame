using System.Diagnostics;
using UnityEngine;

public class PrintSpeed : MonoBehaviour
{
    public Rigidbody2D rb; // Reference to the Rigidbody2D component

    void Start()
    {
        // Start invoking the PrintSpeedEverySecond method every second
        InvokeRepeating("PrintSpeedEverySecond", 0f, 1f);
    }

    void PrintSpeedEverySecond()
    {
        // Calculate the speed magnitude and print it to the console
        float speed = rb.velocity.magnitude;
        UnityEngine.Debug.Log("Object's Speed: " + speed + " units per second");
    }
}
