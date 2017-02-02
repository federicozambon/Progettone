using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MeleeExplosiveEnemy : Enemy
{
    GameObject suicidePool;
    public Coroutine destroying;

    public override void Awake()
    {
        base.Awake();
        suicidePool = GameObject.Find("ExplosiveFuriaParticlePool");
        hPoints = 50;
        comboValue = 10;
    }

    public float timer;
    public float attackTimer = 0.4f;
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

    public void SuicideParticleActivator(Vector3 position)
    {
        for (int i = 0; i < 10; i++)
        {
            if (!suicidePool.GetComponentsInChildren<EffectSettings>(true)[i].gameObject.activeInHierarchy)
            {
                suicidePool.GetComponentsInChildren<EffectSettings>(true)[i].transform.position = this.transform.position;
                suicidePool.GetComponentsInChildren<EffectSettings>(true)[i].gameObject.SetActive(true);
                break;
            }
        }
    }

    public override void Attack()
    {
        if (!isExploded)
        {
            isExploded = true;
            SuicideParticleActivator(this.transform.position);
            Debug.LogError("esploso");
            if (Vector3.Distance(this.transform.position, refManager.playerObj.transform.position) < 4f)
            {
                refManager.playerObj.GetComponent<Player>().TakeDamage(damage);
            }
            StartCoroutine(Die());
        }
    }

    public void Update()
    {
        if (isAttacking)
        {
            timer += Time.deltaTime;
        }
        if (timer >= attackTimer)
        {
            Attack();
        }
    }
}


