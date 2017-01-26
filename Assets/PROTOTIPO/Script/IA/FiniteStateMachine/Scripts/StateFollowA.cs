using UnityEngine;
using System.Collections;
using System;

public class StateFollowA : State
{
    public override void EnterStateLogic()
    {
        botMovement.destination = playerTr.position;
    }

    public override void Handle()
    {
      
        switch (enemyType)
        {
            case "furia":
                navRef.speed = botMovement.rangeAspeed;
                botMovement.destination = playerTr.position;
                navRef.stoppingDistance = 0.1f;
                //attack             
                break;
            case "fante":
                Vector3 playerToBot = (-playerTr.position + transform.position);
                navRef.speed = botMovement.rangeAspeed;
                botMovement.destination = -playerToBot.normalized;
                navRef.stoppingDistance = 2f;
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
                navRef.speed = botMovement.rangeAspeed;
                botMovement.destination = playerTr.position;
                navRef.stoppingDistance = 0;
                break;
            case "cecchino":
                navRef.speed = botMovement.rangeAspeed;
                botMovement.destination = playerTr.position;
                navRef.stoppingDistance = 0;
                break;
        }
    }
}

