using UnityEngine;
using System.Collections;
using System;

public class StateIceTrapped : State
{
    public override void Handle()
    {
        switch (blackRef.enemyType)
        {
            case "furia":
                blackRef.navRef.speed = 0;
                break;
            case "fante":
                blackRef.navRef.speed = 0;
                break;
            case "furiaesplosiva":
                blackRef.navRef.speed = 0;
                break;
            case "predatore":
                blackRef.navRef.speed = 0;
                break;
            case "titano":
                blackRef.navRef.speed = 0;
                break;
            case "cecchino":
                blackRef.navRef.speed = 0;
                break;
        }
    }
}