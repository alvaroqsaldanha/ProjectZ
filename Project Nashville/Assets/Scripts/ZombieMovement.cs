using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ZombieMovement : MonoBehaviour
{
    public Transform player;
    public float zombieRange = 10f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed;
    public float timeZombieCanAttack = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
        gameObject.GetComponent<AIDestinationSetter>().target = player;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((Vector2.Distance(transform.position,player.position) < zombieRange) && (Vector2.Distance(transform.position,player.position) > 1f)){
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg - 90f;;
            rb.rotation = angle;
             GetComponent<AIPath>().enabled = true;
            //direction.Normalize();
            //movement = direction;
           // rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        }
        else{
             GetComponent<AIPath>().enabled = false;
        }
        
    }

    void OnTriggerStay2D(Collider2D collider){
        if ((collider.gameObject.tag == "Player") && (Time.time > timeZombieCanAttack)){
            collider.gameObject.GetComponent<Player>().TakeDamage(20);
            timeZombieCanAttack = Time.time + 1f;
        }
    }

}
