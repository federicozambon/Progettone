using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RangedEnemy : Enemy
{
    public GameObject particlePoolPrefab;
    public RangedEnemyFire rangedFireRef;

    void Start()
    {
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
        transform.LookAt(new Vector3(refManager.playerObj.transform.position.x, this.transform.position.y, refManager.playerObj.transform.position.z));
    }
}
