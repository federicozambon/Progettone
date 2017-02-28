using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MeleeEnemy : Enemy
{
    public Transform face;

    RaycastHit losRayHit;
    public bool isShooting;

    void Update()
    {
        if (!isShooting)
        {
            animRef.SetBool("Attack", false);
        }
        animRef.SetFloat("Speed", blackRef.navRef.velocity.magnitude);
    }

    public override void Awake()
    {      
        base.Awake();
        face = headRef;
        poolP = GameObject.Find("FuriaParticlePool");
        id = transform.GetSiblingIndex();
        myParticle = poolP.transform.GetChild(id);
        myEffect = myParticle.GetComponent<EffectSettings>();
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
        AttackParticleActivator(face.transform.position);
        refManager.playerRef.TakeDamage(damage);
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

        while (Physics.Linecast(face.transform.position, refManager.playerObj.transform.position, out losRayHit))
        {
            if (losRayHit.collider.gameObject.tag == "Player" && Vector3.Distance(face.transform.position, refManager.playerObj.transform.position) < 3)
            {
                animRef.SetBool("Attack", true);
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

    public GameObject poolP;
    Transform myParticle;
    int id;
    EffectSettings myEffect;

    public void AttackParticleActivator(Vector3 position)
    {
        myEffect.Target = headRef.gameObject;
        myEffect.transform.position = headRef.position;
        myEffect.gameObject.SetActive(true);
    }
}


