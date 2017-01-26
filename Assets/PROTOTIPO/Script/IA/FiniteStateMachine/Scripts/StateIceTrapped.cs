using UnityEngine;
using System.Collections;
using System;

public class StateIceTrapped : State
{
    public override void Handle()
    {
        switch (enemyType)
        {
            case "furia":
                navRef.speed = 0;
                break;
            case "fante":
                navRef.speed = 0;
                break;
            case "furiaesplosiva":
                navRef.speed = 0;
                break;
            case "predatore":
                navRef.speed = 0;
                break;
            case "titano":
                navRef.speed = 0;
                break;
            case "cecchino":
                navRef.speed = 0;
                break;
        }
    }
}