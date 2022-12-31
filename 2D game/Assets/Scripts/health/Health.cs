using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public static Health instance;
    public float currentHealth { get; private set; }
    public Animator animator;
    private bool dead;
    public bool isNotTakingDMg = false;
    public float intTimer;
    public float timer = 1;

    
    public float correctRespawnPosition;

    
    //public GameObject REsave;
    public GameObject player;
    //Vector3 strPosition;

   // public GameObject boss;
    //public Transform boss_position1;
    //Vector3 BossStrPosition;

    //public GameObject boundary10;
    //public GameObject Square_lock;

    private void Awake()
    {
       // strPosition = REsave.transform.position;

       // BossStrPosition = boss_position1.transform.position;
        currentHealth = startingHealth;
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
        }
        else
        {
           if (!dead)
           {
                
               //GetComponent<player_movement>().enabled = false;
               ///player_movement.instance.movementSpeed
               animator.SetTrigger("die");
               dead = true;
               Invoke("death",1f);
            }
        }
    }

    void death()
    {
        player.SetActive(false);
         GameObject.Find("Witch").GetComponent<spell_attack>().enabled = false;
       // if(REsave.activeSelf)
         //  Invoke("respawn",0.5f);
        //else
        Invoke("reload",1f);
    }
    void reload()
    {
       
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Destroy(gameObject);
    }
    //void respawn()
    //{
      //  reloadBossEnemy();
       // GameObject playerClone = (GameObject)Instantiate(player);
       // playerClone.transform.position = new Vector3(strPosition.x,strPosition.y+correctRespawnPosition,strPosition.z);
       // playerClone.SetActive(true);
        //Destroy(gameObject);
  //  }

  //  void reloadBossEnemy()
    //{
        
    //    GameObject.Find("Witch").GetComponent<Enemy_boss_behaviour>().enabled = false;
      //  boundary10.SetActive(false);
        //Square_lock.SetActive(false);
       // boss.transform.position = new Vector3(BossStrPosition.x,BossStrPosition.y,BossStrPosition.z);
    //}
}
