using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_health : MonoBehaviour
{
    public float startingHealth ;
    public float currentHealth { get; private set; }
    private bool dead;

    public Rigidbody2D rb;

    public float knockBackForce = 10;
    public float knockBackForceUp = 2;
    public bool isNotTakingDMg = false;
    public float intTimer;
    public float timer = 0.5f;

    private float respawnDelay = 5;
    public float correctRespawnPosition;

    public GameObject enemy;
    Vector3 strPosition;

    public Animator animator;

   // private UnityEngine.Object enemyRef;

    void Start()
    {
        strPosition = transform.position;
       // enemy = this.gameObject;
        //enemyRef = Resources.Load((GameObject)enemy);
        currentHealth = startingHealth;
        rb = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
   }
   void Update()
   {
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
            knockBack();
        }
        else
        {
           if (!dead)
           {
             //GetComponent<Enemy_behaviour>().enabled = false;
               // GetComponent<Slime_enemy_Behaviour>().enabled = false;
             animator.SetTrigger("die");
             knockBack();
             dead = true;
             Invoke("death",1f);
           }
           
        }

    }
    void death()
    {
        enemy.SetActive(false);
        Invoke("respawn",respawnDelay);
    }

    void respawn()
    {
        GameObject enemyClone = (GameObject)Instantiate(enemy);
        enemyClone.transform.position = new Vector3(strPosition.x,strPosition.y+correctRespawnPosition,strPosition.z);
        enemyClone.SetActive(true);
        Destroy(enemy);
    }
  
   
    public void knockBack()
    {
       Transform attacker = getClosesDamageSource();
       Vector2 knockBackDireciton = new Vector2(transform.position.x - attacker.transform.position.x, 0);
       rb.velocity = new Vector2(knockBackDireciton.x,knockBackForceUp)*knockBackForce;
    }

    public Transform getClosesDamageSource()
    {
        GameObject DamageSource = GameObject.FindGameObjectWithTag("Weapon");
        float closestDistans = Mathf.Infinity;
        Transform currentClosestDamageSource = null;

        float currentDistans;
        currentDistans = Vector3.Distance(transform.position, DamageSource.transform.position);
        if(currentDistans < closestDistans)
        {
            closestDistans = currentDistans;
            currentClosestDamageSource = DamageSource.transform;
        }
        
        return currentClosestDamageSource;
    }

    
    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(hitbox.position, hitboxRadius);

        
    }

}
