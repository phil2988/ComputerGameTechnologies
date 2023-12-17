using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    public bool gameOver;

    Vector2 movement;

    private bool down;
    private bool up;
    private bool left;
    private bool right;
    private Vector2 lastDirection;
    // Update is called once per frame
    void Update()
    {

        if (gameOver == true)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            up = true;
            movement.y = 1;
            SetLastDirection(Vector2.up);
        }

        if (Input.GetKeyDown(KeyCode.S))
        { 
            down = true;
            movement.y = -1;
            SetLastDirection(Vector2.down);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            right = true;
            movement.x = 1;
            SetLastDirection(Vector2.right);
        }

        if (Input.GetKeyDown(KeyCode.A))
        { 
            left = true;
            movement.x = -1;
            SetLastDirection(Vector2.left);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            up = false;
            movement.y = 0;
            
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            down = false;
            movement.y = 0;
            
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            right = false;
            movement.x = 0;
           
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            left = false;
            movement.x = 0;
            
        }



        animator.SetBool("Right", right);
        animator.SetBool("Left", left);
        animator.SetBool("Up", up);
        animator.SetBool("Down", down);
        animator.SetFloat("Speed", movement.sqrMagnitude);
       
    }

    private void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);

        //Attack

    }

    private void SetLastDirection(Vector2 vec)
    {
        lastDirection = vec;
    }

    public Vector2 getLastDirection()
    {
        return lastDirection;
    }
}
