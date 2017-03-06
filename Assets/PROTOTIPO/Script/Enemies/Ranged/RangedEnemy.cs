using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RangedEnemy : Enemy
{
    public GameObject particlePoolPrefab;
    public RangedEnemyFire rangedFireRef;

    public override void Awake()
    {
        base.Awake();
        rangedFireRef = GetComponent<RangedEnemyFire>();
        remainHPoints = hPoints;
    }

    public override void Die()
    {
        rangedFireRef.isShooting = false;
        base.Die();
    }

    public override void Attack()
    {
        if (!rangedFireRef.isShooting)
        {
            StartCoroutine(rangedFireRef.Shooting());
        }
    }

    void Update()
    {
        if (Vector3.Dot(transform.forward, navRef.velocity)>0)
        {
            animRef.SetFloat("Blend", blackRef.navRef.velocity.magnitude);
        }
        else
        {
            animRef.SetFloat("Blend", -blackRef.navRef.velocity.magnitude);
        }

        if (!rangedFireRef.isShooting)
        {
            animRef.SetBool("Attack", false);
        }
        //Debug.LogWarning(Mathf.Clamp01(navRef.velocity.magnitude));

        transform.LookAt(new Vector3(refManager.playerObj.transform.position.x, this.transform.position.y, refManager.playerObj.transform.position.z));
    }
}
