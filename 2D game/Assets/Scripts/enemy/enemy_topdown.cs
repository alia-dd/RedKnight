using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_topdown : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float damageEnemy;
   
    private bool movingup;
    private float upEdge;
    private float downEdge;

    private void Awake()
    {
        upEdge = transform.position.y + movementDistance;
        downEdge = transform.position.y - movementDistance;
    }
    private void Update()
    {
        if (movingup)
        {
            if (transform.position.y < upEdge)
            {
                transform.position = new Vector3( transform.position.x, transform.position.y + 3 * Time.deltaTime, transform.position.z);
            }
            else
                movingup = false;
        }
        else
        {
            if (transform.position.y > downEdge)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 3 * Time.deltaTime, transform.position.z);
            }
            else
                movingup = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
        else if(collision.tag == "Enemy")
        {
            collision.GetComponent<enemy_health>().TakeDamage(damageEnemy);
        }
        else if(collision.tag == "Witch")
        {
            collision.GetComponent<Boss_health>().TakeDamage(damageEnemy);
        }
    }

}
