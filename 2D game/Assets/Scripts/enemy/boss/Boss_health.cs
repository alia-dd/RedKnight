using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss_health : MonoBehaviour
{
    public static Boss_health instance;    

    public float startingHealth = 100f;
    public float currentHealth { get; private set; }
    private bool dead;
    public bool isNotTakingDMg = false;
    public float intTimer;
    public float timer = 0.5f;
    
    public bool fill = false;
    public Animator animator;
    public Animator anim;
    public GameObject boss;

    
    public string titleMenu;
    void Start()
    {
        instance = this;
        currentHealth = startingHealth;
        anim = GameObject.Find("end").GetComponent<Animator>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
         if(GetComponent<Enemy_boss_behaviour>().enabled)
         {
            fill = true;
         }
        if(isNotTakingDMg)
        {
            intTimer += Time.deltaTime;
            if (intTimer > timer) {
                 intTimer = 0;
                 isNotTakingDMg = false;
            }
        }
    }
     public void TakeDamage(float _damage)
    {

        if(isNotTakingDMg)
            return;

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        isNotTakingDMg = true;
        if (currentHealth > 0)
        {
            animator.SetTrigger("hurt");
        
            if(currentHealth < 7 && currentHealth > 3)
            {
                 animator.SetBool("STGAttack1", false);
                 animator.SetBool("STGAttack2", true);
           
            }
            else if(currentHealth <= 3)
            {
                animator.SetBool("STGAttack2", false);
                animator.SetBool("STGAttack3", true);
            
            }
        }
        else if(currentHealth <= 0)
        {
           if (!dead)
           {
                
               //GetComponent<Enemy_behaviour>().enabled = false;
               // GetComponent<Slime_enemy_Behaviour>().enabled = false;
               animator.SetTrigger("die");
               dead = true;
               Invoke("death",1f);
            }
        }
    }
    void death()
    {
        boss.SetActive(false);
        Invoke("end",2f);
    }
    void end()
    {
        anim.SetTrigger("end");
        Invoke("done",3f);
    }
    void done()
    {
         SceneManager.LoadScene(titleMenu);
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(hitbox.position, hitboxRadius);

        
    }
}
