using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
  
    float speed = 10f;
    private Vector2 direction;
    private float timeToDie;

    // Start is called before the first frame update
    void Start()
    {
       direction = GameObject.Find("Direction1").transform.position;
       timeToDie = Time.time + 4f;
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
        if (Time.time > timeToDie)
            Destroy(gameObject);
    }
}
