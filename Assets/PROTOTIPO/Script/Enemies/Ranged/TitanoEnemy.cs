using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitanoEnemy : Enemy
{
    public GameObject particlePoolPrefab;
    public GameObject poolP;
    TitanoEnemyFire fireRef;
    public Coroutine attacking;



    public override void Awake()
    {
        base.Awake();
        fireRef = GetComponent<TitanoEnemyFire>();
    }

    public override void Die()
    {
        base.Die();
    }

    public override void Attack()
    {
        if (attacking == null)
        {
            attacking = StartCoroutine(fireRef.Shooting());
        }
    }

    void Update()
    {
        animRef.SetFloat("Speed", navRef.velocity.magnitude);

        if (navRef && navRef.isActiveAndEnabled)
        {
            transform.LookAt(new Vector3(refManager.playerObj.transform.position.x, this.transform.position.y, refManager.playerObj.transform.position.z));
        }
    }
}
