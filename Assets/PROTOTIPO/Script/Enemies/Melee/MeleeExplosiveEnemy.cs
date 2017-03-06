using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MeleeExplosiveEnemy : Enemy
{
    GameObject suicidePool;

    public override void Awake()
    {
        base.Awake();
        suicidePool = GameObject.Find("ExplosiveFuriaParticlePool");
        id = transform.GetSiblingIndex();
        myParticle = suicidePool.transform.GetChild(id);
        myEffect = myParticle.gameObject;
        this.gameObject.SetActive(false);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        isAttacking = false;
        isExploded = false;
        timer = 0;
    }

    public float timer;
    public float attackTimer = 1f;
    public bool isAttacking = false;
    bool isExploded;

    public void StartAttack()
    {
        StartCoroutine(ChargeAttack());
        isAttacking = true;     
    }

    IEnumerator ChargeAttack()
    {
        yield return new WaitForSeconds(0.1f);
        if (isAttacking)
        {
            StartCoroutine(ChargeAttack());
        }
    }

    public Transform transformTr;
    Transform myParticle;
    int id;
    GameObject myEffect;

    public void SuicideParticleActivator(Vector3 position)
    {
        Debug.Log(this.transform.position);
        myEffect.transform.position = position;
        myEffect.gameObject.SetActive(true);
    }

    public override void Attack()
    {
        if (!isExploded)
        {
            isExploded = true;
            SuicideParticleActivator(this.transform.position);
            if (Vector3.Distance(this.transform.position, refManager.playerObj.transform.position) < 4f)
            {
                refManager.playerRef.TakeDamage(damage);
            }
            StartCoroutine(Die());
        }
    }

    public void Update()
    {
        animRef.SetFloat("Speed", navRef.velocity.magnitude);

        if (isAttacking)
        {
            animRef.SetBool("Attack", true);
            timer += Time.deltaTime;
        }
        else
        {
            animRef.SetBool("Attack", false);
        }
        if (timer >= attackTimer)
        {
            Attack();
        }
    }
}


