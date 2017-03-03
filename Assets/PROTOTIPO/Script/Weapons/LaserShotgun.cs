using UnityEngine;
using System.Collections;

public class LaserShotgun : Weapon
{
    public Ray[] shootRay;
    RaycastHit[] shootHit;
    Ray shootRayBlocked;
    RaycastHit shootHitBlocked;
    Ray aimRay;
    RaycastHit aimHit;
    WeaponSelector wSelector;
    public Transform[] transformTr;
    Player player;
    AudioSource shootSound;
    public int startingDamage;

    void Awake()
    {
        shootHit = new RaycastHit[6];
        rotRef = FindObjectOfType<Player>();
        playerGo = rotRef.gameObject;
        
        wSelector = FindObjectOfType<WeaponSelector>();
        assaultRifle = GetComponent<AssaultRifle>();
        laserShotgun = this;

        effectsDisplayTime = 0.2f;
        damagePerShot = 15;
        startingDamage = damagePerShot;
        timeBetweenBullets = 0.6f;
        range = 10f;
        collided = false;
        deltaDegrees = 90;

        player = FindObjectOfType<Player>();
        shootSound = this.GetComponent<AudioSource>();
    }
    
    

    void Update()
    {
        if (Input.GetButtonDown("Grenade"))
        {
            wSelector.ChangeWeapon(1);
            GetComponent<AssaultRifle>().enabled = true;
            weaponArray[0].gameObject.SetActive(true);
            weaponArray[1].gameObject.SetActive(false);
            this.enabled = false;
        }
    
         timer += Time.deltaTime;

        if (rotRef.rotating && timer >= timeBetweenBullets && Time.timeScale != 0 && player.noWeapons == false)
        {
            Shoot();
            StartCoroutine(GunShotSound());
        }
    }

    IEnumerator GunShotSound()
    {
        shootSound.clip = AudioContainer.Self.Shotgun_Sparo;
        shootSound.Play();
        yield return new WaitForSeconds(0.2f);
    }

    int counter = 18;
    public float maxDispersion = 12f;
 


    public void Shoot()
    {
        timer = 0f;
        collided = false;

        float maxDispersionCoef = maxDispersion * 2 / 5;
        transform.Rotate(0, 0, 0);
        while (!collided)
        {
            for (int i = 0; i < deltaDegrees; i++)
            {
                aimRay.origin = transform.position;
                transform.localEulerAngles = new Vector3(-45, 0, 0);
                transform.Rotate(i + 1, 0, 0);
                aimRay.direction = transform.forward;


                for (int f = 0; f < 6; f++)
                {
                    float offsetX = Random.value, offsetY = Random.value, offsetZ = Random.value;

                    if (Physics.Linecast(transform.position, transform.position + new Vector3(range * Mathf.Cos(Mathf.Deg2Rad*( - maxDispersion -player.transform.eulerAngles.y + 270 + maxDispersionCoef * f)), 0, range * Mathf.Sin(Mathf.Deg2Rad * (-maxDispersion - player.transform.eulerAngles.y + 270 + maxDispersionCoef * f))), out shootHit[f]))
                    {
                        Enemy hitted = shootHit[f].collider.GetComponent<Enemy>();
                        if (hitted != null)
                        {
                            collided = true;
                            ParticleActivator(transform.position + new Vector3(range * Mathf.Cos(Mathf.Deg2Rad * (-maxDispersion - player.transform.eulerAngles.y + 90 + maxDispersionCoef * f)), 0, range * Mathf.Sin(Mathf.Deg2Rad * (-maxDispersion - player.transform.eulerAngles.y + 90 + maxDispersionCoef * f))), counter);
                            if (counter < 29)
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
                if (collided)
                {
                    break;
                }
             
        
                transform.localRotation = Quaternion.identity;
                transform.localEulerAngles = new Vector3(0, 0, 0);

                if (!collided && i == deltaDegrees - 1)
                {
                    for (int f = 0; f < 6; f++)
                    {
                        float offsetX = Random.value, offsetY = Random.value, offsetZ = Random.value;
                        shootRayBlocked.origin = this.transform.position;
                        shootRayBlocked.direction = transform.forward;

                        if (Physics.Raycast(shootRayBlocked, out shootHitBlocked, range))
                        {
                            ParticleActivator(transform.position + new Vector3(range * Mathf.Cos(Mathf.Deg2Rad * (-maxDispersion - player.transform.eulerAngles.y + 90 + maxDispersionCoef * f)), 0, range * Mathf.Sin(Mathf.Deg2Rad * (-maxDispersion - player.transform.eulerAngles.y + 90 + maxDispersionCoef * f))), counter);
                            if (counter < 29)
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
                                ParticleActivator(transform.position + new Vector3(range * Mathf.Cos(Mathf.Deg2Rad * (-maxDispersion - player.transform.eulerAngles.y +90 + maxDispersionCoef * f)), 0, range * Mathf.Sin(Mathf.Deg2Rad * (-maxDispersion - player.transform.eulerAngles.y + 90 + maxDispersionCoef * f))), counter);
                            if (counter < 29)
                            {
                                counter++;
                            }
                            else
                            {
                                counter = 0;
                            }
                        }               
                        collided = true;
                       
                    }
                }
                transform.localRotation = Quaternion.identity;
                transform.localEulerAngles = new Vector3(0, 0, 0);
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

