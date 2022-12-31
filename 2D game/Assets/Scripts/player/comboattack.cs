using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comboattack : MonoBehaviour
{
    public static comboattack instance;
    public bool isattacking = false;
    public bool strikeOK = false;
    public int strikeNO = 0;
    public Animator animator;
   
    void Start()
    {
        animator = GetComponent<Animator>();
    }
   public void Awake()
   {
        instance = this;
   }
    
   void Update()
   {
        
        Attack();
        
   }

   void Attack()
   {
         if (Input.GetKeyDown(KeyCode.Z) && !isattacking )
        {
            isattacking = true;

            if(strikeNO < 3)
            {
                strikeNO ++;
            }
            else if(strikeNO >= 0)
            {
                strikeNO = 0;
                strikeOK = true;
            }
            
        }
        if(strikeOK = false)
        {
            strikeOK = true;
        }
    }

   
}
