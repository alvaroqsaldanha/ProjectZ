using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
 public Weapon weapon;

 private void OnTriggerEnter2D(Collider2D collider){

     if (collider.gameObject.tag == "Player"){
         int check = 0;
         int weaponLimit = collider.gameObject.GetComponent<Player>().getWeaponLimit();
         int weaponIndex = collider.gameObject.GetComponent<Player>().getWeaponIndex();
         int weaponCount = collider.gameObject.GetComponent<Player>().getWeaponCount(); 
         for (int i = 0; i <= weaponCount - 1; i++)
         {
            if ((collider.gameObject.GetComponent<Player>().weapons[i] != null) && (weapon.name == collider.gameObject.GetComponent<Player>().weapons[i].name)){
                check = 1;
                break;
            }           
         }
         if (check == 0){
            if (weaponCount < weaponLimit){
                collider.gameObject.GetComponent<Player>().weapons.Add(weapon);
                collider.gameObject.GetComponent<Player>().setWeaponIndex(weaponCount);
            }
            else if (weaponCount == weaponLimit) {
                collider.gameObject.GetComponent<Player>().weapons[weaponIndex] = weapon;
            }
            collider.gameObject.GetComponent<Player>().currentWeapon = weapon;
            collider.gameObject.GetComponent<SpriteRenderer>().sprite = weapon.artwork;
            Destroy(gameObject);
            }
        }
     }
 
}
