using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spell_script : MonoBehaviour
{
    public float speed;
    private GameObject target;
    private Rigidbody2D rb;
    private float timer;

    [SerializeField] private float seconds;
    [SerializeField] private float damage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = target.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > seconds)
        {
            Destroy(gameObject);
        }
    }

  
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.tag == "Player")
        {
             Destroy(gameObject);
            collision.GetComponent<Health>().TakeDamage(damage);
        }
   }
}
