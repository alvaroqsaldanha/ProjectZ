using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int maxHealth = 100;
    public int health;
    public Healthbar healthbar;

    public Weapon currentWeapon;
    public List<Weapon> weapons = new List<Weapon>();
    public int weaponIndex = 0;
    private int weaponLimit = 2;
    public GameObject throwablePrefab;

    public AudioSource audioSource;

    public Animator animator;

    public Sprite playerTest;
    public Transform firepoint;

    private float timeToFire = 0f;
    private float timeToScroll = 0f;
    private float timeToThrow = 0f;

    public Transform attackPoint;
    public float attackRange = 5f;
    public LayerMask layers;

    void Start()
    {
        health = maxHealth;
        healthbar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButton(0)) && (currentWeapon != null)){
            if (Time.time > timeToFire){
                audioSource.clip = currentWeapon.shotSound;
                audioSource.Play();
                currentWeapon.Shoot();
                timeToFire = Time.time + 1 / currentWeapon.firerate;
            }
        }
        
        if (Input.GetMouseButton(1) && (currentWeapon != null) && (Time.time > timeToThrow)){
                GameObject throwed = Instantiate(throwablePrefab, new Vector3(firepoint.position.x,firepoint.position.y,firepoint.position.z) , Quaternion.identity);
                throwed.gameObject.GetComponent<SpriteRenderer>().sprite = currentWeapon.img;
                throwed.gameObject.GetComponent<Transform>().localScale = currentWeapon.scale;
                weapons.RemoveAt(weaponIndex);
                weaponIndex--;
                if (weaponIndex == -1) weaponIndex = weapons.Count - 1;
                if (weapons.Count == 0){
                    weaponIndex = 0;
                    GetComponent<SpriteRenderer>().sprite = playerTest;
                    currentWeapon = null;
                } 
                else{
                    currentWeapon = weapons[weaponIndex];
                    GetComponent<SpriteRenderer>().sprite = currentWeapon.artwork;
                }
                timeToThrow = Time.time + 0.5f;            
        }

        if (Input.GetKeyDown(KeyCode.Space)){
            animator.enabled = true;
            animator.SetTrigger("ola");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,layers);

            foreach (Collider2D enemy in hitEnemies){
                if (!enemy.isTrigger)
                    enemy.gameObject.GetComponent<Enemy>().getMeleeHit(80);
            }


        }
        
        if((Input.GetAxis("Mouse ScrollWheel") != 0) && (weapons.Count > 1) && (Time.timeSinceLevelLoad > timeToScroll)){
            if(Input.GetAxis("Mouse ScrollWheel") > 0){
                weaponIndex++;
                if (weaponIndex == weaponLimit) weaponIndex = 0;
            }
            if(Input.GetAxis("Mouse ScrollWheel") < 0){
                weaponIndex--;
                if (weaponIndex == -1) weaponIndex = weapons.Count - 1;
            }
            currentWeapon = weapons[weaponIndex];   
            GetComponent<SpriteRenderer>().sprite = currentWeapon.artwork;
            timeToScroll = Time.timeSinceLevelLoad + 0.2f;
        }
    }

    public void TakeDamage(int damage){

        health -= damage;
        healthbar.setHealth(health);
        
    }

    public int getWeaponIndex(){
        return weaponIndex;
    }

    public int getWeaponLimit(){
        return weaponLimit;
    }

    public void setWeaponIndex(int index){
        weaponIndex = index;
    }

    public int getWeaponCount(){
        return weapons.Count;
    }

    public void OnTriggerEnter2D(Collider2D collider){
        if ((collider.gameObject.tag == "healthpack") && (health < 100)){
            health += 20;
            if (health > 100) health = 100;
            healthbar.setHealth(health);
            Destroy(collider.gameObject);
        }
    }

    public void disableAnimator(){
        animator.enabled = false;
        if (getWeaponCount() > 0)
            GetComponent<SpriteRenderer>().sprite = currentWeapon.artwork;
        else
            GetComponent<SpriteRenderer>().sprite = playerTest;
    }
}
