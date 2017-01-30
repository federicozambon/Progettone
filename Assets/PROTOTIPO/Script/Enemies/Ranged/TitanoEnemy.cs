using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitanoEnemy : Enemy
{
    public GameObject particlePoolPrefab;
    public GameObject poolP;

    void Start()
    {
        poolP = (GameObject)Instantiate(particlePoolPrefab, this.transform.position, Quaternion.identity);
        GetComponent<TitanoEnemyFire>().GetPool();
        hPoints = 250;
        comboValue = 1000;
        remainHPoints = hPoints;

        navRef = GetComponent<NavMeshAgent>();
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
        if (!GetComponent<TitanoEnemyFire>().isShooting)
        {
            StartCoroutine(GetComponent<TitanoEnemyFire>().Shooting());
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
