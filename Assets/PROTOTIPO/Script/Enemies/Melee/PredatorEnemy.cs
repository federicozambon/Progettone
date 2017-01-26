using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PredatorEnemy : Enemy
{
    void Start()
    {
        hPoints = 50;
        comboValue = 10;

        if (flyCamRef.endedCutScene)
        {
            isActive = true;
        }
    }

    public void Update()
    {
        if (isActive)
        {
            Occlusion();
            /*
            if (!knockbacked)
            {
                navRef.enabled = true;
            }
            else
            {
                navRef.enabled = false;
            }
            */
            if (navRef.isActiveAndEnabled)
            {
                //navRef.destination = playerObj.transform.position;
                //navRef.updateRotation = false;
                //transform.LookAt(playerObj.transform);
            }
        }
        else
        {
            navRef.enabled = false;
        }
    }

   /* public override IEnumerator KnockbackTimer(float time)
    {
        yield return new WaitForSeconds(time);
        knockbacked = false;
    }*/
}


