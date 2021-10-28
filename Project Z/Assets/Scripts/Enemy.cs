using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    public float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 1){
            Destroy(gameObject);
        }
        
    }

    public void getMeleeHit(int damage){
        health -= damage;
        Debug.Log(health);
    }

    void OnCollisionEnter2D(Collision2D collider){
        if (collider.gameObject.tag == "bullet"){
            health -= GameObject.Find("Player").GetComponent<Player>().currentWeapon.damage;
            Debug.Log("hit");
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.tag == "Thrown"){
            health -= 50;
            Destroy(collider.gameObject);
        }
    }
}
