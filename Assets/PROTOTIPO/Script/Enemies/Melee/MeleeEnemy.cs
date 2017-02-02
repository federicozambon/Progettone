using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MeleeEnemy : Enemy
{
    public Transform face;
    public GameObject bulletPrefab;
    GameObject playerGo;

    public GameObject particlePoolPrefab;
    public GameObject poolP;

    RaycastHit losRayHit;
    public bool isShooting;

    public override void Awake()
    {
        base.Awake();
        face = headRef;
        //poolP = GameObject.Find("FuriaPool");
        hPoints = 50;
        comboValue = 10;
    }

    public float attackTimer = 1;

    public void StartAttack()
    {
        if (!isShooting)
        {
            StartCoroutine(Shooting());
        }  
    }

    public override void Attack()
    {
        Debug.LogError("attaccato");
        AttackParticleActivator(face.transform.position);
        refManager.playerObj.GetComponent<Player>().TakeDamage(damage);
        StartCoroutine(AttackCd());
    }

    public IEnumerator AttackCd()
    {
        yield return new WaitForSeconds(2);
        isShooting = false;
    }

    public IEnumerator Shooting()
    {
        float timer = 0;
        isShooting = true;

        while (Physics.Linecast(face.transform.position, playerGo.transform.position, out losRayHit))
        {
            if (losRayHit.collider.gameObject.tag == "Player" && Vector3.Distance(face.transform.position, playerGo.transform.position) < 3)
            {
                //transform.LookAt(new Vector3(playerGo.transform.position.x, this.transform.position.y, playerGo.transform.position.z));
                timer += Time.deltaTime;
                yield return null;
                if (timer > attackTimer)
                {
                    Attack();      
                    break;
                }
            }
            else
            {
                timer = 0;
                isShooting = false;
                break;
            }
        }
        if (GetComponent<MeleeEnemy>().hPoints <= 0)
        {
            StopAllCoroutines();
        }
    }

    public Transform transformTr;

    public void AttackParticleActivator(Vector3 position)
    {
        if (pool)
        {
            if (!pool.GetComponentInChildren<EffectSettings>(true).gameObject.activeInHierarchy)
            {
                transformTr = GetComponentsInChildren<Transform>()[1];
                transformTr.position = position;
                pool.GetComponentInChildren<EffectSettings>(true).transform.position = face.transform.position;
                pool.GetComponentInChildren<EffectSettings>(true).Target = transformTr.gameObject;
                pool.GetComponentInChildren<EffectSettings>(true).gameObject.SetActive(true);
            }
        }
    }
}


