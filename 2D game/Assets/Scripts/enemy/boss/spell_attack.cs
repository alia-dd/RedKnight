using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spell_attack : MonoBehaviour
{
    public bool isattack1;
    public bool isattack2;
    public bool isattack3;

    private float timer1;
    private float timer2;
    private float timer3;
    public float intTimer;
    public float timer = 3;

    public GameObject first_spell;
    public GameObject second_spell;
    public GameObject third_spell;
    public Transform spell_position;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        isattack1 = true;
        anim = GetComponent<Animator>();
        timer1 =  Random.Range(3, 5);
        timer2 = 1;
        timer3 =  Random.Range(5, 9);

        intTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        checkStage();
        if(isattack1 && anim.GetCurrentAnimatorStateInfo(0).IsName("witch_attack_casting1")){
            intTimer += Time.deltaTime;
            if (intTimer > timer) {
                    intTimer = 0;
                    spell_one();
            }
        }
        if(isattack2 && anim.GetCurrentAnimatorStateInfo(0).IsName("witch_attack_casting2")){
            intTimer += Time.deltaTime;
            if (intTimer > timer) {
                    intTimer = 0;
                    spell_two();
            }
        }
        if(isattack3 && anim.GetCurrentAnimatorStateInfo(0).IsName("witch_attack_casting3")){
            intTimer += Time.deltaTime;
            if (intTimer > timer) {
                    intTimer = 0;
                    spell_three();
            }
        }
      
    }
    void spell_one()
    {
        Instantiate(first_spell, spell_position.position, Quaternion.identity);
    }
    void spell_two()
    {
        Instantiate(second_spell, spell_position.position, Quaternion.identity);
    }
    void spell_three()
    {
        Instantiate(third_spell, spell_position.position, Quaternion.identity);
    }

    void checkStage()
    {
        float Boss_currentHealth =  Boss_health.instance.currentHealth;
        if(Boss_currentHealth > 7)
        {  
            isattack1 = true;
        }
        else if(Boss_currentHealth < 7 && Boss_currentHealth > 3)
        {  
            
            isattack1 = false;
            isattack2 = true;
        }
        else if(Boss_currentHealth <= 3 )
        {
            isattack2 = false;
            isattack3 = true;
        }
    }
}

