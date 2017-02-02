using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RangedEnemy : Enemy
{
    public GameObject particlePoolPrefab;
    public GameObject poolP;

    void Start()
    {
        poolP = GameObject.Find("FanteParticlePool");
        GetComponent<RangedEnemyFire>().GetPool();
        hPoints = 25;
        comboValue = 10;
        remainHPoints = hPoints;

        blackRef.navRef = GetComponent<NavMeshAgent>();
        remainHPoints = hPoints;
        refManager.playerObj = FindObjectOfType<Player>().gameObject;

        if (refManager.flyCamRef.endedCutScene)
        {
            isActive = true;
        }
    }

    public override IEnumerator Die()
    {
        poolP.SetActive(false);
        return base.Die();
    }

    public override void Attack()
    {
        if (!GetComponent<RangedEnemyFire>().isShooting)
        {
            StartCoroutine(GetComponent<RangedEnemyFire>().Shooting());
        }
    }

    void Update()
    {
        if (blackRef.navRef && blackRef.navRef.isActiveAndEnabled)
        {
            transform.LookAt(new Vector3(refManager.playerObj.transform.position.x, this.transform.position.y, refManager.playerObj.transform.position.z));
        }
    }
}
