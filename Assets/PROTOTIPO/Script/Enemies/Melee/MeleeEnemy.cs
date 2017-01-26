using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MeleeEnemy : Enemy
{
    void Start()
    {
        hPoints = 50;
        comboValue = 10;
    }

    public float timer;
    public float attackTimer = 0.2f;
    public bool isAttacking = false;

    public void StartAttack()
    {
        isAttacking = true;
        fxRef.enabledParticle();
    }

    public override void Attack()
    {
        if (Vector3.Distance(this.transform.position, playerObj.transform.position) < 3)
        {
            timer = 0;
            Debug.LogError("attaccato");
            fxRef.ParticleExplosion();
            playerObj.GetComponent<Player>().TakeDamage(damage);
            isAttacking = false;
        }

    }

    public void Update()
    {
        if (Vector3.Distance(this.transform.position, playerObj.transform.position) > 3)
        {
            //isAttacking = false;
            //fxRef.StopParticle();
            timer = 0;
        }
        if (isAttacking)
        {
            //transform.LookAt(playerObj.transform);
            timer += Time.deltaTime;
        }
        if (timer >= attackTimer)
        {
            Attack();
        }

        if (isActive)
        {
            Occlusion();
        }
    }

   /* public override IEnumerator KnockbackTimer(float time)
    {
        yield return new WaitForSeconds(time);
        knockbacked = false;
    }*/
}


