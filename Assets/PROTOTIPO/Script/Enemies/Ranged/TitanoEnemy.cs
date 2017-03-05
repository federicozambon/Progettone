using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitanoEnemy : Enemy
{
    public GameObject particlePoolPrefab;
    public GameObject poolP;
    TitanoEnemyFire fireRef;



    void Start()
    {
        fireRef = GetComponent<TitanoEnemyFire>();
        remainHPoints = hPoints;
    }

    public override IEnumerator Die()
    {
        return base.Die();
    }

    public override void Attack()
    {
        if (!fireRef.isShooting)
        {
            StartCoroutine(fireRef.Shooting());
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
