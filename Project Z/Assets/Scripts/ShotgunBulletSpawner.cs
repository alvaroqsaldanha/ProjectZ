using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBulletSpawner : MonoBehaviour
{
    public GameObject shotgunBulletPrefab;
    public float bulletSpeed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            // Spawns bullet
            var tempBullet = (GameObject)Instantiate(shotgunBulletPrefab, GameObject.Find("Firepoint2").transform.position, GameObject.Find("Firepoint2").transform.rotation);
            // Gets Rigidbody2D compoenent from spawned bullet
            Rigidbody2D tempBulletRB = tempBullet.GetComponent<Rigidbody2D>();

            // Randomize angle variation between bullets
            float spreadAngle = Random.Range(-30, 30);

            // Take the random angle variation and add it to the initial
            // desiredDirection (which we convert into another angle), which in this case is the players aiming direction
            var x = GameObject.Find("Firepoint2").transform.position.x -  GameObject.Find("Player").transform.position.x;
            var y = GameObject.Find("Firepoint2").transform.position.y -  GameObject.Find("Player").transform.position.y;
            float rotateAngle = spreadAngle + (Mathf.Atan2(y, x) * Mathf.Rad2Deg);
                            
            // Calculate the new direction we will move in which takes into account 
            // the random angle generated
            var MovementDirection = new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad), Mathf.Sin(rotateAngle * Mathf.Deg2Rad)).normalized;

            tempBulletRB.velocity = MovementDirection * bulletSpeed;
            Destroy(tempBullet, 0.25f);
            
        }
        Destroy(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
