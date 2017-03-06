using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SniperEnemy : Enemy
{
    public GameObject particlePoolPrefab;
    public GameObject poolP;
    public Coroutine attacking;


    public override void Attack()
    {
        if (attacking == null)
        {
            attacking = StartCoroutine(GetComponent<SniperEnemyFire>().Shooting());
        }
    }

    public override void Die()
    {
        //poolP.SetActive(false);
        base.Die();
    }

    void Update()
    {
        //Occlusion();
        animRef.SetFloat("Speed", navRef.velocity.magnitude);
        

        if (blackRef.navRef && blackRef.navRef.isActiveAndEnabled)
        {
            transform.LookAt(new Vector3(refManager.playerObj.transform.position.x,this.transform.position.y, refManager.playerObj.transform.position.z));
        }
    }
}
