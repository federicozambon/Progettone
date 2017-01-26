using UnityEngine;
using System.Collections;

public class RocketLauncher : Weapon
{

    public Transform weapon;
    public GameObject projectile;
  
    public float speed = 10;
    
    bool sparo = true;

    WeaponSelector wSelector;
    UIController uiElements;
    Player playerElements;

    void Start()
    {
        wSelector = FindObjectOfType<WeaponSelector>();
        uiElements = FindObjectOfType<UIController>();
        playerElements = FindObjectOfType<Player>();
    }

    public void Shooting()
    {  
        GameObject newBullet = (GameObject)Instantiate(projectile, this.transform.position, Quaternion.identity);
        newBullet.transform.forward = weapon.transform.forward;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Rocket") && playerElements.noWeapons == false)
        {
            if (playerElements.rocketAmmo > 0)
            {
                playerElements.rocketAmmo--;
                uiElements.ammo.text = playerElements.rocketAmmo.ToString();
                Shooting();
            }             
        }

        if (Input.GetButtonDown("Fire2"))
        {
            wSelector.ChangeWeapon(1);
            GetComponent<AssaultRifle>().enabled = true;
            weaponArray[0].gameObject.SetActive(true);
            weaponArray[2].gameObject.SetActive(false);
            weaponArray[1].gameObject.SetActive(false);
            this.enabled = false;
        }

        if (Input.GetButtonDown("Previous Weapon"))
        {
            wSelector.ChangeWeapon(2);
            GetComponent<RocketLauncher>().enabled = true;
            weaponArray[0].gameObject.SetActive(false);
            weaponArray[2].gameObject.SetActive(false);
            weaponArray[1].gameObject.SetActive(true);
        }

        if (Input.GetButtonDown("Next Weapon"))
        {
            wSelector.ChangeWeapon(3);
            GetComponent<LaserShotgun>().enabled = true;
            weaponArray[0].gameObject.SetActive(false);
            weaponArray[2].gameObject.SetActive(true);
            weaponArray[1].gameObject.SetActive(false);
            this.enabled = false;
        }
    }
}    


