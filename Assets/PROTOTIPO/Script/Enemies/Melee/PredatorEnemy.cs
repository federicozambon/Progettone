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
            if (navRef.isActiveAndEnabled)
            {
            }
        }
        else
        {
            navRef.enabled = false;
        }
    }
}


