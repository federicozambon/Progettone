using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SniperEnemy : Enemy
{
    public GameObject particlePoolPrefab;
    public GameObject poolP;
    public Coroutine attacking;

    void Start()
    {
        poolP = (GameObject)Instantiate(particlePoolPrefab, this.transform.position, Quaternion.identity);
        GetComponent<SniperEnemyFire>().GetPool();
        hPoints = 25;
        comboValue = 650;
        remainHPoints = hPoints;

        navRef = GetComponent<NavMeshAgent>();
        remainHPoints = hPoints;
        playerObj = FindObjectOfType<Player>().gameObject;
    }

    public override void Attack()
    {
        if (attacking == null)
        {
            attacking = StartCoroutine(GetComponent<SniperEnemyFire>().Shooting());
        }
    }

    public override IEnumerator Die()
    {
        poolP.SetActive(false);
        return base.Die();
    }

    void Update()
    {
          //Occlusion();

        if (navRef && navRef.isActiveAndEnabled)
        {
            transform.LookAt(new Vector3(playerObj.transform.position.x,this.transform.position.y,playerObj.transform.position.z));
        }
    }
}
