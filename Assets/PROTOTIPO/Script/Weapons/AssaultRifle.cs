using UnityEngine;
using System.Collections;

public class AssaultRifle : Weapon
{
    Ray shootRay;
    Ray shootRayBlocked;
    RaycastHit shootHit;
    RaycastHit shootHitBlocked;
    WeaponSelector wSelector;
    int shootableMask;
    AudioSource shootSound;
    Player player;
    public int startingDamage;


    public Transform[] transformTr;
    bool isPoolFull = false;

    void Awake()
    {   
        rotRef = FindObjectOfType<Player>();
        playerGo = rotRef.gameObject;

        wSelector = FindObjectOfType<WeaponSelector>();
        shootSound = this.GetComponent<AudioSource>();
        assaultRifle = this;

        laserShotgun = GetComponent<LaserShotgun>();       

        effectsDisplayTime = 0.2f;
        damagePerShot = 12;
        startingDamage = damagePerShot;
        timeBetweenBullets = 0.15f;
        range = 15f;
        collided = false;
        deltaDegrees = 90;

        player = FindObjectOfType<Player>();
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (rotRef.rotating && timer >= timeBetweenBullets && Time.timeScale != 0 && player.noWeapons == false)
        {
            Shoot();
            StartCoroutine(GunShotSound());
        }
   
        if (Input.GetButtonDown("Grenade"))
        {
            wSelector.ChangeWeapon(2);
            laserShotgun.enabled = true;
            weaponArray[0].gameObject.SetActive(false);
            weaponArray[1].gameObject.SetActive(true);
            this.enabled = false;
        }        
    } 

    IEnumerator GunShotSound()
    {
        shootSound.clip = AudioContainer.Self.Laser_Sparo;
        shootSound.Play();
        yield return new WaitForSeconds(0.2f);
    }

    int counter = 9;

    public void Shoot()
    {
        timer = 0f;
        collided = false;

        transform.Rotate(0, 0, 0);
        while (!collided)
        {
            shootRay.origin = transform.position;
            for (int i = 0; i < deltaDegrees; i++)
            {
                transform.localEulerAngles = new Vector3(-45, 0, 0);
                transform.Rotate(i + 1, 0, 0);
                shootRay.direction = transform.forward;

                if (Physics.Raycast(shootRay, out shootHit, range))
                {
                    Enemy enemyScript = shootHit.collider.GetComponent<Enemy>();

                    if (enemyScript != null)
                    {          
                        ParticleActivator(enemyScript.headRef.transform.position, counter);
                        if (counter < 9)
                        {
                            counter++;
                        }
                        else
                        {
                            counter = 0;
                        }
                        collided = true;
                        break;
                    }          
                }
                transform.localRotation = Quaternion.identity;
                transform.localEulerAngles = new Vector3(0, 0, 0);

                if (!collided && i == deltaDegrees - 1)
                {
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                    shootRayBlocked.origin = this.transform.position;
                    shootRayBlocked.direction = transform.forward;

                    collided = true;
                    if (Physics.Raycast(shootRayBlocked, out shootHitBlocked, range))
                    {
                        ParticleActivator(shootHitBlocked.point, counter);
                        if (counter < 9)
                        {
                            counter++;
                        }
                        else
                        {
                            counter = 0;
                        }
                    }
                    else
                    {
                        ParticleActivator(transform.position + (transform.forward * range),counter);
                        if (counter < 9)
                        {
                            counter++;
                        }
                        else
                        {
                            counter = 0;
                        }
                    }                
                }
            }
        }
    }

    public GameObject pool;

    public void ParticleActivator(Vector3 position, int looper)
    {
        transformTr[looper].position = position;          
        pool.GetComponentsInChildren<EffectSettings>(true)[looper].transform.position = this.transform.position;
        pool.GetComponentsInChildren<EffectSettings>(true)[looper].Target = transformTr[looper].gameObject;
        pool.GetComponentsInChildren<EffectSettings>(true)[looper].gameObject.SetActive(true);
    }
}



