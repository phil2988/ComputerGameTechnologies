using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rigidbody2D sword;
    public Animator animator;

    bool attackY;
    bool attackX;
    // Update is called once per frame
    public PlayerMovement pm;
    void Update()
    {
        Vector2 direction = pm.getLastDirection();
        
        if(direction == Vector2.left)
        {
            Vector2 theScale = animator.transform.localScale;
            theScale.y = 1;
            theScale.x = 1;
            animator.transform.localScale = theScale;
            SetPosLeft();
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                attackX = true;
            }
        }
        else if(direction == Vector2.right)
        {
            Vector2 theScale = animator.transform.localScale;
            theScale.y = -1;
            theScale.x = -1;
            animator.transform.localScale = theScale;
            SetPosRight();
            if(Input.GetKeyDown(KeyCode.RightArrow)) 
            { 
                attackX = true; 
            }
        }
        else if(direction == Vector2.up)
        {
            Vector2 theScale = animator.transform.localScale;
            theScale.y = -1;
            theScale.x = -1;
            animator.transform.localScale = theScale;
            SetPosUp();
            if( Input.GetKeyDown(KeyCode.UpArrow)) 
            {
                attackY = true; 
            }
        }
        else if(direction == Vector2.down)
        {
            Vector2 theScale = animator.transform.localScale;
            theScale.y = 1;
            theScale.x = 1;
            animator.transform.localScale = theScale;
            SetPosDown();
            if( Input.GetKeyDown(KeyCode.DownArrow))
            {
                attackY = true;
            }
        }


        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            attackY = false;
        }


        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            attackX = false;
        }

        animator.SetBool("AttackY", attackY);
        animator.SetBool("AttackX", attackX);
    }

    private void FixedUpdate()
    {   
        //Attack
        
    }

    private void SetPosLeft()
    {
        sword.position = new Vector2((float)(rb.position.x - 0.5), (float)(rb.position.y + 0));
    }

    private void SetPosRight()
    {
        sword.position = new Vector2((float)(rb.position.x + 0.5), (float)(rb.position.y + 0));
    }

    private void SetPosUp()
    {
        sword.position = new Vector2(rb.position.x, (float)(rb.position.y + 0.5));
    }

    private void SetPosDown()
    {
        sword.position = new Vector2(rb.position.x, rb.position.y -1);
    }
}