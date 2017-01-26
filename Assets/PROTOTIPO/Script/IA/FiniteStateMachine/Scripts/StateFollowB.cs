using UnityEngine;
using System.Collections;
using System;

public class StateFollowB : State
{
    public override void Handle()
    {
        switch (enemyType)
        {
            case "furia":
                navRef.speed = botMovement.rangeBspeed;
                botMovement.destination = playerTr.position;
                navRef.stoppingDistance = 0.1f;
                break;
            case "fante":
                navRef.speed = botMovement.rangeBspeed;
                botMovement.destination = playerTr.position;
                //shoot
                break;
            case "furiaesplosiva":
                navRef.speed = botMovement.rangeBspeed;
                botMovement.destination = playerTr.position;
                navRef.stoppingDistance = 0.1f;
                break;
            case "predatore":
                navRef.speed = botMovement.rangeBspeed;
                botMovement.destination = playerTr.position;
                navRef.stoppingDistance = 0;
                break;
            case "titano":
                navRef.speed = botMovement.rangeBspeed;
                botMovement.destination = playerTr.position;
                navRef.stoppingDistance = 0;
                break;
            case "cecchino":
                navRef.speed = botMovement.rangeBspeed;
                botMovement.destination = playerTr.position;
                navRef.stoppingDistance = 0;
                break;
        }
    }
}

