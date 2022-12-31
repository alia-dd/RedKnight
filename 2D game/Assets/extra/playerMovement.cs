using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerMovement : MonoBehaviour
{
    public static playerMovement instance;
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
   
    public void Awake()
   {
        instance = this;
   }
   void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));
      
		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
            animator.SetBool("IsJumping", true);
          
		} 
        if (Input.GetButtonUp("Jump"))
        {
            jump =false;
        }
       
    }

    

    
    public void OnLanding ()
	{
	    animator.SetBool("IsJumping", false);
	}

 
 
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

}