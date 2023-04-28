using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float yOffset = 1f; // Offset the text slightly above the player

    private Transform player;

    private void Start()
    {
        player = transform.parent; // Get reference to parent object (the player)
    }

    private void Update()
    {
        transform.position = player.position + new Vector3(0f, yOffset, 0f); // Update position to follow player with an offset
    }   
}
