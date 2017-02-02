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

        if (refManager.flyCamRef.endedCutScene)
        {
            isActive = true;
        }
    }

    public void Update()
    {
        if (isActive)
        {
            if (blackRef.navRef.isActiveAndEnabled)
            {
            }
        }
        else
        {
            blackRef.navRef.enabled = false;
        }
    }
}


