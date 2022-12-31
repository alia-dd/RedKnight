using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_boss_cut_scene : MonoBehaviour
{
    public bool isplayingaCutsceen;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isplayingaCutsceen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isplayingaCutsceen)
            chackIfsceenDone();
    }
    void chackIfsceenDone(){
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("witch_Taunt"))
        {
            GetComponent<Enemy_boss_behaviour>().enabled = true;
        }
    }
}

