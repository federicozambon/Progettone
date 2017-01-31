using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MeleeExplosiveEnemy : Enemy
{
    public Coroutine destroying;
    void Start()
    {
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

    public override void Attack()
    {
        if (!isExploded)
        {
            isExploded = true;
            fxRef.gameObject.SetActive(true);
            Debug.LogError("esploso");
            if (Vector3.Distance(this.transform.position, playerObj.transform.position) < 4f)
            {
                playerObj.GetComponent<Player>().TakeDamage(damage);
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


