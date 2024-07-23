using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeftRightMovement : MonoBehaviour
{
    public float moveSpeed = 10f; // Speed at which the player moves

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from the player
        float moveX = 0f;

        if (Input.GetKey(KeyCode.A) && transform.position.x >= -7.829859f)
        {
            moveX = -1f;
        }
        else if (Input.GetKey(KeyCode.D) && transform.position.x <= 7.84471)
        {
            moveX = 1f;
        }

        movement = new Vector2(moveX, 0f);
    }

    void FixedUpdate()
    {
        // Move the player
        rb.velocity = movement * moveSpeed;
    }
}
