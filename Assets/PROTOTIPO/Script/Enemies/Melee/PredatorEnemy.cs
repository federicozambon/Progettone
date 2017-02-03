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
        this.gameObject.SetActive(false);
    }
}


