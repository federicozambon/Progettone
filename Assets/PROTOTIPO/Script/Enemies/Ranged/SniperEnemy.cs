using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SniperEnemy : Enemy
{
    public int clockWise;

    void Start()
    {
        if (Random.value>0.5f)
        {
            clockWise = 1;
        }
        else
        {
            clockWise = -1;
        }

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

    void Update()
    {
        if (isActive)
        {
            Occlusion();

            if (!knockbacked)
            {
                navRef.enabled = true;
            }
            else
            {
                navRef.enabled = false;
            }

            if (navRef && navRef.isActiveAndEnabled)
            {
                //navRef.destination = playerObj.transform.position;
                //navRef.updateRotation = false;
                transform.LookAt(playerObj.transform);
            }
        }
        else
        {
            this.transform.Rotate(Vector3.up, clockWise * 60 * Time.deltaTime);
            navRef.enabled = false;
        }
    }

    /* public override IEnumerator KnockbackTimer(float time)
     {
         yield return new WaitForSeconds(time);
         knockbacked = false;
         StopCoroutine("KnockbackTimer");
     }*/

}
