﻿using UnityEngine;
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

    void Awake()
    {
        shootHit = new RaycastHit[6];
        rotRef = FindObjectOfType<Player>();
        playerGo = rotRef.gameObject;
        
        wSelector = FindObjectOfType<WeaponSelector>();
        assaultRifle = GetComponent<AssaultRifle>();
        laserShotgun = this;

        effectsDisplayTime = 0.2f;
        damagePerShot = 40;
        timeBetweenBullets = 0.4f;
        range = 30f;
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
            weaponArray[2].gameObject.SetActive(false);
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

    public void Shoot()
    {
        timer = 0f;
        collided = false;

        transform.Rotate(0, 0, 0);
        while (!collided)
        {
            aimRay.origin = transform.position;
            for (int i = 0; i < deltaDegrees; i++)
            {
                transform.localEulerAngles = new Vector3(-45, 0, 0);
                transform.Rotate(i + 1, 0, 0);
                aimRay.direction = transform.forward;

                if (Physics.Raycast(aimRay, out aimHit, range))
                {
                    Debug.DrawRay(aimRay.origin, aimRay.direction);
                    Enemy enemyScript = aimHit.collider.GetComponent<Enemy>();
                    Destructble destructbleScript = aimHit.collider.GetComponent<Destructble>();
                    if (enemyScript == null)
                    {
                    }
                    else if (enemyScript != null)
                    {
                        for (int f = 0; f < 6; f++)
                        {
                            float offsetX = Random.value, offsetY = Random.value, offsetZ = Random.value;
          
                            if (Physics.Linecast(transform.position, aimHit.point + new Vector3(offsetX, offsetY, offsetZ), out shootHit[f]))
                            {
                                ParticleActivator(aimHit.point + new Vector3(offsetX, offsetY, offsetZ));
                                Enemy enemyHitScript = shootHit[f].collider.GetComponent<Enemy>();

                                if (enemyHitScript != null)
                                {
                                    enemyHitScript.TakeDamage(damagePerShot);
                                }
                            }
                        }
                    }
                    if (destructbleScript == null)
                    {
                    }

                    else if (destructbleScript != null)
                    {
                        for (int f = 0; f < 6; f++)
                        {
                            float offsetX = Random.value, offsetY = Random.value, offsetZ = Random.value;

                            if (Physics.Linecast(transform.position, aimHit.point + new Vector3(offsetX * 3, offsetY * 3, offsetZ * 3), out shootHit[f]))
                            {
                                Destructble destructiblehitScript = shootHit[f].collider.GetComponent<Destructble>();

                                if (destructiblehitScript != null)
                                {
                                    ParticleActivator(destructbleScript.transform.position + new Vector3(offsetX * 3, offsetY * 3, offsetZ * 3));
                                    destructbleScript.TakeDamage(damagePerShot);
                                   
                                    collided = true;
                                }
                            }
                        }
                    }
                    transform.localRotation = Quaternion.identity;
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                if (!collided && i == deltaDegrees - 1)
                {
                    for (int f = 0; f < 6; f++)
                    {
                        float offsetX = Random.value, offsetY = Random.value, offsetZ = Random.value;
                        shootRayBlocked.origin = this.transform.position;
                        shootRayBlocked.direction = transform.forward;

                        Physics.Raycast(shootRayBlocked, out shootHitBlocked, 100);

                        ParticleActivator(shootHitBlocked.point + new Vector3(offsetX * 3, offsetY * 3, offsetZ * 3));
                        collided = true;
                    }
                }
                transform.localRotation = Quaternion.identity;
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    public GameObject pool;

    

    public void ParticleActivator(Vector3 position)
    {
        for (int i = 0; i < 13; i++)
        {
            if (!pool.GetComponentsInChildren<EffectSettings>(true)[i].gameObject.activeInHierarchy)
            {
                transformTr[i].position = position;
                pool.GetComponentsInChildren<EffectSettings>(true)[i].transform.position = this.transform.position;
                pool.GetComponentsInChildren<EffectSettings>(true)[i].Target = transformTr[i].gameObject;
                pool.GetComponentsInChildren<EffectSettings>(true)[i].gameObject.SetActive(true);
                break;
            }
        }
    }
}

