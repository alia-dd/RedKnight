using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_enemy_Behaviour : MonoBehaviour
{
     #region Public Variables 

    public float moveSpeed;
    public float timer; 
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public GameObject triggerArea;
    public float groundCheckRadius;
    public float jumpForce = 16.0f;

    #endregion

    #region Private Variables

    private Animator anim;
    private float distance; //Store the distance b/w enemy and player
    private float intTimer;
    private bool isGrounded;
    private float jumpTimer;
    private bool canJump;

    private Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    #endregion
   void Awake()
   {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
   {
        //distance = Vector2.Distance(transform.position, target.position);
        
        anim.SetBool("isGrounde", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        if (inRange)
        {
            EnemyLogic();
            CheckIfCanJump();
            CheckJump();
        }
        else if (!inRange)
        {
            Stop();
        }
    }

    private void FixedUpdate()
    {
        CheckSurroundings();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            target = collider.transform;
            inRange = true;
        }
    }
    void EnemyLogic()
   {
        Flip();
        Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
       
    }

    void Stop()
    {

        anim.SetBool("G_Slime_idle", true);
    }



    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180;
        }
        else
        {
            Debug.Log("Twist");
            rotation.y = 0;
        }

        transform.eulerAngles = rotation;
    }

    void jumpattack(){
        if(canJump)
        {
           rb.velocity = new Vector2(rb.velocity.x, jumpForce);
           //transform.position  = new Vector2(transform.position.x, jumpForce);
           //Vector2 targetupPosition = new Vector2(target.position.y, transform.position.x);
           //transform.position = Vector2.MoveTowards(transform.position, targetupPosition, jumpForce);
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

        if (isGrounded)
        {
            jumpattack();
        }

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
