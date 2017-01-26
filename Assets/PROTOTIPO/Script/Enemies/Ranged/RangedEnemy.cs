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

    public override void Attack()
    {
        StartCoroutine(GetComponent<RangedEnemyFire>().Shooting());
    }

    void Update()
    {
        if (isActive)
        {
            //Occlusion();

            if (!knockbacked)
            {
                navRef.enabled = true;
            }
            else
            {
                navRef.enabled = false;
            }
        }
    }
}
