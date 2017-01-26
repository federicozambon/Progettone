using UnityEngine;
using System.Collections;
using System;

public class StateFollowC : State
{
    public override void Handle()
    {
        switch (enemyType)
        {
            case "furia":
                navRef.speed = botMovement.rangeCspeed;
                botMovement.destination = playerTr.position;
                navRef.stoppingDistance = 0.1f;
                break;
            case "fante":
                navRef.speed = botMovement.rangeCspeed;
                botMovement.destination = playerTr.position;
                navRef.stoppingDistance = 1.5f;
                break;
            case "furiaesplosiva":
                navRef.speed = botMovement.rangeCspeed;
                botMovement.destination = playerTr.position;
                navRef.stoppingDistance = 0.1f;
                break;
            case "predatore":
                navRef.speed = botMovement.rangeCspeed;
                botMovement.destination = playerTr.position;
                navRef.stoppingDistance = 0;
                break;
            case "titano":
                navRef.speed = botMovement.rangeCspeed;
                botMovement.destination = playerTr.position;
                navRef.stoppingDistance = 0;
                break;
            case "cecchino":
                navRef.speed = botMovement.rangeCspeed;
                botMovement.destination = playerTr.position;
                navRef.stoppingDistance = 0;
                break;
        }
    }
}

