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
    RocketLauncher rocketRef;
    EffectSettings[] effectPool;


    public Transform[] transformTr;
    bool isPoolFull = false;

    void Awake()
    {
        effectPool = new EffectSettings[9];
        for (int i = 0; i < 9; i++)
        {
            effectPool[i] = pool.GetComponentsInChildren<EffectSettings>(true)[i];
            ParticleActivator(this.transform.position);
        }

        rocketRef = GetComponent<RocketLauncher>();
        transform.localRotation = Quaternion.identity;
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

    public Coroutine Co;

    public void Update()
    {
        timer += Time.deltaTime;

        if (rotRef.rotating && timer >= timeBetweenBullets && Time.timeScale != 0 && player.noWeapons == false && !rocketRef.aiming)
        {
            Shoot();
            StartCoroutine(GunShotSound());
        }
   
        if (Input.GetButtonDown("Grenade"))
        {
            laserShotgun.enabled = true;
            this.enabled = false;     
            wSelector.ChangeWeapon(2);
            
            weaponArray[0].gameObject.SetActive(false);
            weaponArray[1].gameObject.SetActive(true);
        }        
    } 

    IEnumerator GunShotSound()
    {
        shootSound.clip = AudioContainer.Self.Laser_Sparo;
        shootSound.Play();
        yield return new WaitForSeconds(0.2f);
    }

    int counter = 10;

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
                        ParticleActivator(enemyScript.headRef.transform.position);                  
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
                        ParticleActivator(shootHitBlocked.point);
                    }
                    else
                    {
                        ParticleActivator(transform.position + (transform.forward * range));
                    }                
                }
            }
        }
    }

    public GameObject pool;

    public void ParticleActivator(Vector3 position)
    {
        if (counter < 8)
        {
            counter++;
        }
        else
        {
            counter = 0;
        }
        transformTr[counter].position = position;          
        effectPool[counter].transform.position = this.transform.position;
        effectPool[counter].Target = transformTr[counter].gameObject;
        effectPool[counter].gameObject.SetActive(true);
    }
}



