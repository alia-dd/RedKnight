using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_boss_behaviour : MonoBehaviour
{
    public static Enemy_boss_behaviour instance;

    public float moveSpeed;
    public float timer; //Timer between position change
    public Transform position1;
    public Transform position2;
    public Transform position3;
    public Transform target;

    private Rigidbody2D rb;
    private Animator anim;
    private bool attackMode;
    private bool setNextposition;
    private bool ismoving;
    private float distance;//bw player and enemy
    private float intTimer;
    private Transform currentPosition;
    private Transform nextPosition;
    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<Enemy_boss_behaviour>().enabled = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        intTimer = 0;
        currentPosition = position1;
        nextPosition = position2;  
        anim.SetBool("STGAttack1", true);
    }

    // Update is called once per frame
    void Update()
    {
      Flip();

      intTimer += Time.deltaTime;
      if (intTimer > timer) {
            changePosition();
       }
      
      if(setNextposition){
            NextBossPosition();
            setNextposition = false;
       }
       UpdateAnimations();
    }
    public void changePosition()
    {
        MovementToNextposition();   
    }
    void MovementToNextposition()
    {
        Vector2 newposition = new Vector2(nextPosition.position.x,nextPosition.position.y);

        transform.position = Vector2.MoveTowards(transform.position, newposition, moveSpeed * Time.deltaTime);
        ismoving = true;
        if(transform.position == nextPosition.position){
            intTimer = 0;
            currentPosition = nextPosition;
            setNextposition = true;
            ismoving = false;
            anim.SetTrigger("attack");
        }
        if(Boss_health.instance.currentHealth <= 3 )
        {
            moveSpeed = 15;
        } 
        if(Boss_health.instance.currentHealth == 0 )
        {
            moveSpeed = 0;
        }
    }

   
    void NextBossPosition()
    {
       if(currentPosition == position1)
        {
            nextPosition = position2;
        }   
        else if(currentPosition == position2)
        {
            nextPosition = position3;
        }   
        else if(currentPosition == position3)
        {
            nextPosition = position1;
        }   
    }

    private void UpdateAnimations()
    {
        anim.SetBool("ismoving", ismoving);
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
}
   
