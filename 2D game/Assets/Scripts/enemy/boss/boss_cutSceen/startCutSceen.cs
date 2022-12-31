using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startCutSceen : MonoBehaviour
{
   public Animator cameraAnim;
   public Animator anim;
   void Start()
    {
        anim =GameObject.Find("Witch").GetComponent<Animator>();
      
    }
   private void OnTriggerEnter2D(Collider2D collision)
   {
        if (collision.tag == "Player")
        {
            GameObject.Find("player").GetComponent<player_movement>().enabled = false;
            GameObject.Find("player").GetComponent<comboattack>().enabled = false;
            cameraAnim.SetBool("cutSceen", true);
            Invoke(nameof(stopCutSceen),3f);
            anim.SetTrigger("witchTaunt");
        }
       
   }
   void stopCutSceen()
   {
        
        GameObject.Find("Witch").GetComponent<Enemy_boss_behaviour>().enabled = true;
        GameObject.Find("player").GetComponent<player_movement>().enabled = true;
        GameObject.Find("player").GetComponent<comboattack>().enabled = true;
        cameraAnim.SetBool("cutSceen", false);
        Destroy(gameObject);
   }
}
