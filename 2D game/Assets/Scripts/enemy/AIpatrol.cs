using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIpatrol : MonoBehaviour
{
    
    public float walkSpeed, range;
    private float disToPlayer;
    [SerializeField] private float damage;

    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn;

    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public Transform player;
    public Animator animator;

    private bool cooling;
    private float intTimer;


    // Start is called before the first frame update
    void Start()
    {
        mustPatrol = true;
        animator.SetBool("isPatroling", true);
        //Physics2D.IgnoreLayerCollision(7,9);
    }

    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }
        disToPlayer = Vector2.Distance(transform.position, player.position);
        if(disToPlayer <= range)
        {
            if(player.position.x > transform.position.x && transform.localScale.x < 0 || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }
            if(bodyCollider.IsTouchingLayers(groundLayer))
            {
                mustPatrol = false;
            }
            if(disToPlayer < 1.5)
            {
                mustPatrol = false;
                animator.SetBool("isPatroling", false);
                rb.velocity = Vector2.zero;
                Attack();
            }
            ///
        }
        else{
            animator.SetBool("attack", false);
            mustPatrol = true;
            animator.SetBool("isPatroling", true);
         }
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.6f, groundLayer);
        }
    }


    void Patrol()
    {   
        if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }


     void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }

    void Attack()
    {
        animator.SetBool("attack", true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
