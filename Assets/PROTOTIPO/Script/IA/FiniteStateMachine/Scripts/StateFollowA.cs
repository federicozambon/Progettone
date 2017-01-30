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
        switch (enemyType)
        {
            case "furia":
                navRef.speed = botMovement.rangeAspeed;
                botMovement.destination = playerTr.position;
                navRef.stoppingDistance = 0.1f;           
                break;
            case "fante":
                navRef.speed = 15;
                botMovement.destination = enemyRef.transform.position-enemyRef.transform.forward*10;
                navRef.stoppingDistance = 0.1f;
                break;
            case "furiaesplosiva":
                navRef.speed = botMovement.rangeAspeed;
                botMovement.destination = playerTr.position;
                navRef.stoppingDistance = 0.1f;
                break;
            case "predatore":
                navRef.speed = botMovement.rangeAspeed;
                botMovement.destination = playerTr.position;
                navRef.stoppingDistance = 0;
                break;
            case "titano":
                navRef.speed = 25;
                botMovement.destination = enemyRef.transform.position - enemyRef.transform.forward * 10;
                navRef.stoppingDistance = 0.1f;
                break;
            case "cecchino":
                navRef.speed = botMovement.rangeAspeed;
                botMovement.destination = playerTr.position;
                navRef.stoppingDistance = 0;
                break;
        }
    }
}

