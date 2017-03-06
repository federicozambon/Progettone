using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PredatorEnemy : Enemy
{
    public Transform face;
    public Transform face1;
    public Transform face2;

    RaycastHit losRayHit;
    public bool isShooting;


    void Update()
    {
        myEffect1.transform.position = face1.position;
        myEffect2.transform.position = face2.position;
        if (!isShooting)
        {
            //animRef.SetBool("Attack", false);
        }
        animRef.SetFloat("Speed", blackRef.navRef.velocity.magnitude);
    }

    public override void Awake()
    {
        base.Awake();
        face = headRef;
        poolP = GameObject.Find("PredatorParticlePool");
        id = transform.GetSiblingIndex();
        myParticle1 = poolP.transform.GetChild(id);
        myParticle2 = poolP.transform.GetChild(20+id);
        myEffect1 = myParticle1.gameObject;
        myEffect2 = myParticle2.gameObject;
    }

    public float attackTimer = 2;

    public void StartAttack()
    {
        if (!isShooting)
        {
            StartCoroutine(Shooting());
        }
    }

    public override void Attack()
    {
      
        refManager.playerRef.TakeDamage(damage);
        StartCoroutine(AttackCd());
    }

    public IEnumerator AttackCd()
    {
        AttackParticleActivator(face.transform.position);
        yield return new WaitForSeconds(2);
        animRef.SetBool("Attack", false);
        isShooting = false;
    }
    public IEnumerator Shooting()
    {
        float timer = 0;
        isShooting = true;

        while (Physics.Linecast(face.transform.position, refManager.playerObj.transform.position, out losRayHit))
        {
            if (losRayHit.collider.gameObject.tag == "Player" && Vector3.Distance(face.transform.position, refManager.playerObj.transform.position) < 3)
            {
               
              
                //transform.LookAt(new Vector3(playerGo.transform.position.x, this.transform.position.y, playerGo.transform.position.z));
                timer += Time.deltaTime;
                yield return null;
                if (timer > attackTimer)
                {
                    animRef.SetBool("Attack", true);
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
        if (GetComponent<PredatorEnemy>().hPoints <= 0)
        {
            StopAllCoroutines();
        }
    }

    public GameObject poolP;
    Transform myParticle1;
    Transform myParticle2;
    int id;
    GameObject myEffect1;
    GameObject myEffect2;

    public void AttackParticleActivator(Vector3 position)
    {
        myEffect1.gameObject.SetActive(true);
        myEffect2.gameObject.SetActive(true);
    }
}
