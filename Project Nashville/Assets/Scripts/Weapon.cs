using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public string WeaponName;
    public float damage;
    public float firerate;

    public Sprite artwork;
    public Sprite img;
    public Vector3 scale;
    public AudioClip shotSound;

    public GameObject bulletPrefab;

    public void Shoot(){
        switch (WeaponName){
            case "Pistol":
                Instantiate(bulletPrefab,GameObject.Find("Firepoint1").transform.position,Quaternion.identity);
                break;
            default:
                Instantiate(bulletPrefab,GameObject.Find("Firepoint2").transform.position,Quaternion.identity);
                break;
        }  
    }

   /* public void throw(){

    } */

}
