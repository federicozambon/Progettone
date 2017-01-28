using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RangedEnemy : Enemy
{
    public int clockWise;
    public GameObject particlePoolPrefab;
    public GameObject poolP;

    void Start()
    {
        poolP = (GameObject)Instantiate(particlePoolPrefab, this.transform.position, Quaternion.identity);
        GetComponent<RangedEnemyFire>().GetPool();
        hPoints = 25;
        comboValue = 10;
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
        StartCoroutine(GetComponent<RangedEnemyFire>().Shooting());
    }

    void Update()
    {
        //Occlusion();
        if (navRef && navRef.isActiveAndEnabled)
        {
            transform.LookAt(new Vector3(playerObj.transform.position.x, this.transform.position.y, playerObj.transform.position.z));
        }
    }
}
