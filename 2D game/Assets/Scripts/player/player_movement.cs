using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public static player_movement instance;

    private bool isWalking;
    private bool isFacingRight = true;
    private bool isGrounded;
    private bool canJump;
    private bool isAttemptingToJump;
    
    private float movementInputDirection = 0f;
    private float jumpTimer;
    public float movementSpeed = 10.0f;
    public float jumpForce = 16.0f;
    public float groundCheckRadius;
    public float variableJumpHeightMultiplier = 0.5f;
    public float jumpTimerSet = 0.15f;

    private Rigidbody2D rb;
    private Animator animator;


    public Transform groundCheck;
    public LayerMask whatIsGround;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckJump();
    }
    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }



    private void CheckMovementDirection()
    {
        if(isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if(!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }
         if(Mathf.Abs(rb.velocity.x) >= 0.01f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

     private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }




     private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            
            if(isGrounded )
            {
                Jump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*variableJumpHeightMultiplier);
        }
    }

    private void Jump()
    {
        if(canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimer = 0;
            isAttemptingToJump = false;
        }
    }

    private void CheckIfCanJump()
    {
        if(isGrounded )
        {
            canJump = true;
        }

        else
        {
            canJump = false;
        }
      
    }

     private void CheckJump()
    {
        if(jumpTimer > 0)
        {
            if (isGrounded)
            {
                Jump();
            }
        }
        if(isAttemptingToJump)
        {
            jumpTimer -= Time.deltaTime;
        }
    }

     private void ApplyMovement()
    {
        rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
    }

    private void UpdateAnimations()
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isGrounde", isGrounded);
        animator.SetFloat("yVelocity", rb.velocity.y);
       
    }
    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        
    }
}

