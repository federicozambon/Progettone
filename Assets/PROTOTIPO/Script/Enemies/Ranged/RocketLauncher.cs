using UnityEngine;
using System.Collections;

public class RocketLauncher : Weapon
{
    public ReferenceManager refManager;
    public Transform weapon;
    public GameObject projectile;
    AudioSource shootSound;
    private bool enabled = false;
    private bool shoot = true;
  
    public float speed = 10;
    
    bool sparo = true;

    WeaponSelector wSelector;

    private void Awake()
    {
        refManager = GameObject.FindGameObjectWithTag("Reference").GetComponent<ReferenceManager>();
    }

    void Start()
    {
        wSelector = FindObjectOfType<WeaponSelector>();
        shootSound = this.GetComponent<AudioSource>();
    }

    public void Shooting()
    {  
        GameObject newBullet = (GameObject)Instantiate(projectile, this.transform.position, Quaternion.identity);
        newBullet.transform.forward = weapon.transform.forward;
        StartCoroutine(GunShotSound());
    }

    IEnumerator GunShotSound()
    {
        shootSound.clip = AudioContainer.Self.RocketLauncher_Sparo;
        shootSound.Play();
        yield return new WaitForSeconds(0.2f);
    }

    public void Update()
    {
        float rocket = Input.GetAxisRaw("RightTrigger");

        if (rocket <= 0)
        {
            enabled = false;
            shoot = true;
        }
            
        else
        {
            enabled = true;
            
        }

        if (enabled == true && refManager.playerRef.noWeapons == false && shoot == true)
        {
            shoot = false;

            if (refManager.playerRef.rocketAmmo > 0)
            {
                refManager.playerRef.rocketAmmo--;
                refManager.uicontroller.ammo.text = refManager.playerRef.rocketAmmo.ToString();
                Shooting();
            }             
        }

        
    }
}    


