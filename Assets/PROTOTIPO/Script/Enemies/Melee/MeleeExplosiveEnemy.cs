using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MeleeExplosiveEnemy : Enemy
{
    void Start()
    {
        myColor = toOutline.material.color;
        hPoints = 50;
        comboValue = 10;
    }

    public float timer;
    public float attackTimer = 0.4f;
    public bool isAttacking = false;

    public void StartAttack()
    {
        StartCoroutine(ChargeAttack());
        isAttacking = true;     
    }

    public Color myColor;

    IEnumerator ChargeAttack()
    {
        toOutline.material.color += new Color(0.15f,0,0);
        yield return new WaitForSeconds(0.1f);
        if (isAttacking)
        {
            StartCoroutine(ChargeAttack());
        }
        else
        {
            toOutline.material.color = myColor;
        }
    }

    public override void Attack()
    {
        myColor = toOutline.material.color;
        fxRef.gameObject.SetActive(true);
        waveRef.killedCounter++;
        Debug.LogError("esploso");
        //fxRef.ParticleExplosion();
        if (Vector3.Distance(this.transform.position, playerObj.transform.position) < 4f)
        {
            playerObj.GetComponent<Player>().TakeDamage(damage);
        }

        isAttacking = false;
        timer = 0;
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.75f);
        Destroy(this.gameObject);
    }

    public void Update()
    {
        if (Vector3.Distance(this.transform.position, playerObj.transform.position) > 3)
        {

        }
        if (isAttacking)
        {
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


