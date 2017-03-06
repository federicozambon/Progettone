using UnityEngine;
using System.Collections;

public class RocketLauncher : Weapon
{
    public ReferenceManager refManager;
    public Transform weapon;
    public GameObject projectile;
    AudioSource shootSound;
    private bool enabled = false;
    private bool shoot = false;
    public bool startGame = false;
    LineRenderer lineRef;
    public int startingDamage = 150;

  
    public float speed = 10;
    
    bool sparo = true;

    WeaponSelector wSelector;

    private void Awake()
    {
        damagePerShot = startingDamage;
        refManager = GameObject.FindGameObjectWithTag("Reference").GetComponent<ReferenceManager>();
        lineRef = GetComponent<LineRenderer>();
    }

    void Start()
    {
        wSelector = FindObjectOfType<WeaponSelector>();
        shootSound = this.GetComponent<AudioSource>();
    }

    public void Shooting()
    {  
        GameObject newBullet = (GameObject)Instantiate(projectile, this.transform.position, Quaternion.identity);
        newBullet.GetComponent<RocketLife>().damagePerShot = damagePerShot;
        newBullet.transform.forward = weapon.transform.forward;
        StartCoroutine(GunShotSound());
    }

    IEnumerator GunShotSound()
    {
        shootSound.clip = AudioContainer.Self.RocketLauncher_Sparo;
        shootSound.Play();
        yield return new WaitForSeconds(0.2f);
    }

    public bool aiming = false;
    public bool canFire = true;

    public void Update()
    {
        RaycastHit hit;
        float rocket = Input.GetAxisRaw("RightTrigger");

        if (refManager.playerRef.rocketAmmo > 0 && canFire && refManager.playerRef.currentHealth > 0)
        {
            if (aiming)
            {
                lineRef.enabled = true;
                if (Physics.Raycast(this.transform.position, transform.forward, out hit, 20))
                {
                    lineRef.SetPosition(0, this.transform.position);
                    lineRef.SetPosition(1, hit.point);
                }
                else
                {
                    lineRef.SetPosition(0, this.transform.position);
                    lineRef.SetPosition(1, this.transform.position+this.transform.forward*20);
                }
            }

            if (rocket > 0.2f)
            {
                aiming = true;
            }

            if (aiming && rocket < 0.1f)
            {
                lineRef.enabled = false;
                aiming = false;
                shoot = true;
            }

            if (refManager.playerRef.noWeapons == false && shoot == true && startGame == true)
            {
                shoot = false;
                refManager.playerRef.rocketAmmo--;
                refManager.uicontroller.ammo.text = refManager.playerRef.rocketAmmo.ToString();
                Shooting();
            }
        }        
    }
}    


