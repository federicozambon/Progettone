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

        remainHPoints = hPoints;
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

        if (blackRef.navRef && blackRef.navRef.isActiveAndEnabled)
        {
            transform.LookAt(new Vector3(refManager.playerObj.transform.position.x,this.transform.position.y, refManager.playerObj.transform.position.z));
        }
    }
}
