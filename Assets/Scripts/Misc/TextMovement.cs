using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMovement : MonoBehaviour
{
    public float amplitude = 0.1f; // the amount of movement up and down
    public float speed = 1.0f; // the speed of the movement

    private Vector3 startPos; // the starting position of the text object

    void Start()
    {
        startPos = transform.position; // save the starting position of the text object
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * speed) * amplitude; // calculate the new Y position based on a sine wave
        transform.position = new Vector3(transform.position.x, newY, transform.position.z); // update the text object's position
    }
}