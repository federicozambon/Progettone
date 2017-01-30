using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RangedEnemy : Enemy
{
    public GameObject particlePoolPrefab;
    public GameObject poolP;

    void Start()
    {
        poolP = (GameObject)Instantiate(particlePoolPrefab, this.transform.position, Quaternion.identity);
        GetComponent<RangedEnemyFire>().GetPool();
        hPoints = 25;
        comboValue = 10;
        remainHPoints = hPoints;

        navRef = GetComponent<UnityEngine.AI.NavMeshAgent>();
        remainHPoints = hPoints;
        playerObj = FindObjectOfType<Player>().gameObject;

        if (flyCamRef.endedCutScene)
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
        if (navRef && navRef.isActiveAndEnabled)
        {
            transform.LookAt(new Vector3(playerObj.transform.position.x, this.transform.position.y, playerObj.transform.position.z));
        }
    }
}
