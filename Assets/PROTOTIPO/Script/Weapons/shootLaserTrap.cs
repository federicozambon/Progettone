using UnityEngine;
using System.Collections;

public class shootLaserTrap : Weapon
{           
    Ray shootRay;                               
    Ray shootRayBlocked;
    RaycastHit shootHit;                         
    RaycastHit shootHitBlocked;
    WeaponSelector wSelector;
    int shootableMask;
    AudioSource shootSound;
    public int playerPointDamage = 1;
    

    void Awake()
    {
        rotRef = FindObjectOfType<Player>();
        gunLight = GetComponent<Light>();
        wSelector = FindObjectOfType<WeaponSelector>();
        shootSound = this.GetComponent<AudioSource>();

        effectsDisplayTime = 0.2f;
        damagePerShot = 20;
        timeBetweenBullets = 0.15f;
        range = 5f;
        collided = false;
        deltaDegrees = 90;
    }

    public void Shoot()
    {
        timer = 0f;
        collided = false;
        gunLight.enabled = true;

        while (!collided)
        {
            gunLine.SetPosition(0, transform.position);
            shootRay.origin = transform.position;
            for (int i = 0; i < deltaDegrees; i++)
            {
                transform.Rotate(deltaDegrees - i * 2, 0, 0);
                shootRay.direction = transform.forward;

                if (Physics.Raycast(shootRay, out shootHit, range))
                {
                    Enemy enemyScript = shootHit.collider.GetComponent<Enemy>();
                    Player player = shootHit.collider.GetComponent<Player>();
                    if (enemyScript == null)
                    {
                    }

                    else if (enemyScript != null)
                    {
                       // spawnedParticle = Instantiate<ParticleSystem>(particleGo);
                        //spawnedParticle.transform.position = enemyScript.headRef.position;
                        //spawnedParticle.Play();
                        gunLine.SetPosition(1, enemyScript.headRef.position);
                        gunLine.enabled = true;
                        enemyScript.TakeDamage(damagePerShot);
                        enemyScript.gameObject.GetComponent<Rigidbody>().AddForce(-enemyScript.gameObject.transform.forward * 20 * Time.deltaTime, ForceMode.Impulse);
                        collided = true;
                    }

                    if (player == null)
                    {
                    }

                    else if (player != null)
                    {
                        //spawnedParticle = Instantiate<ParticleSystem>(particleGo);
                        //spawnedParticle.transform.position = player.transform.position;
                        //spawnedParticle.Play();
                        gunLine.SetPosition(1, player.transform.position);
                        gunLine.enabled = true;

                        player.TakeDamage(playerPointDamage);
                        //player.gameObject.GetComponent<Rigidbody>().AddForce(-player.gameObject.transform.forward * 20 * Time.deltaTime, ForceMode.Impulse);
                        collided = true;
                    }

                    transform.localRotation = Quaternion.identity;
                }
                if (!collided && i == deltaDegrees - 1)
                {
                    shootRayBlocked.origin = this.transform.position;
                    shootRayBlocked.direction = transform.forward;
                    Debug.Log(shootRayBlocked.direction);

                    collided = true;
                    Physics.Raycast(shootRayBlocked, out shootHitBlocked, 100);
                    gunLine.SetPosition(1, shootHitBlocked.point);
                    //spawnedParticle = Instantiate<ParticleSystem>(particleGo);
                    //spawnedParticle.transform.position = shootHitBlocked.point;
                    gunLine.enabled = true;
                }
            }
        }
    }
}

