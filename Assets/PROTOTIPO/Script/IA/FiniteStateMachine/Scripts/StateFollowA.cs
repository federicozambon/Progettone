using UnityEngine;
using System.Collections;
using System;

public class StateFollowA : State
{
    public override void EnterStateLogic()
    {
        //botMovement.destination = playerTr.position;
    }

    public override void Handle()
    {    
        switch (blackRef.enemyType)
        {
            case "furia":
                blackRef.navRef.speed = blackRef.botMovement.rangeAspeed;
                blackRef.botMovement.destination = blackRef.playerTr.position;
                blackRef.navRef.stoppingDistance = 0.1f;           
                break;
            case "fante":
                blackRef.navRef.speed = 15;
                blackRef.botMovement.destination = blackRef.enemyRef.transform.position- blackRef.enemyRef.transform.forward*10;
                blackRef.navRef.stoppingDistance = 0.1f;
                break;
            case "furiaesplosiva":
                blackRef.navRef.speed = blackRef.botMovement.rangeAspeed;
                blackRef.botMovement.destination = blackRef.playerTr.position;
                blackRef.navRef.stoppingDistance = 0.1f;
                break;
            case "predatore":
                blackRef.navRef.speed = blackRef.botMovement.rangeAspeed;
                blackRef.botMovement.destination = blackRef.playerTr.position;
                blackRef.navRef.stoppingDistance = 0;
                break;
            case "titano":
                blackRef.navRef.speed = 25;
                blackRef.botMovement.destination = blackRef.enemyRef.transform.position - blackRef.enemyRef.transform.forward * 10;
                blackRef.navRef.stoppingDistance = 0.1f;
                break;
            case "cecchino":
                blackRef.navRef.speed = blackRef.botMovement.rangeAspeed;
                blackRef.botMovement.destination = blackRef.playerTr.position;
                blackRef.navRef.stoppingDistance = 0;
                break;
        }
    }
}

