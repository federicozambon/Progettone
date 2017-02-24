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
        hPoints = 250;
        comboValue = 1000;
        remainHPoints = hPoints;
    }

    public override IEnumerator Die()
    {
        poolP.SetActive(false);
        return base.Die();
    }

    public override void Attack()
    {
        if (!GetComponent<TitanoEnemyFire>().isShooting)
        {
            StartCoroutine(GetComponent<TitanoEnemyFire>().Shooting());
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
