using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RangedEnemy : Enemy
{
    public GameObject particlePoolPrefab;
    public RangedEnemyFire rangedFireRef;
    Animator animRef;

    void Start()
    {
        animRef = GetComponent<Animator>();
        rangedFireRef = GetComponent<RangedEnemyFire>();
        hPoints = 25;
        comboValue = 10;
        remainHPoints = hPoints;

        if (refManager.flyCamRef.endedCutScene)
        {
            isActive = true;
        }
    }

    public override IEnumerator Die()
    {
        return base.Die();
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

        if (!rangedFireRef.isShooting)
        {
            animRef.SetFloat("Blend", 0);
            animRef.SetBool("Attack", true);
        }
        else
        {
            animRef.SetFloat("Blend", Mathf.Clamp01(navRef.velocity.magnitude));
            animRef.SetBool("Attack", false);
        }
        Debug.LogWarning(Mathf.Clamp01(navRef.velocity.magnitude));

        transform.LookAt(new Vector3(refManager.playerObj.transform.position.x, this.transform.position.y, refManager.playerObj.transform.position.z));
    }
}
