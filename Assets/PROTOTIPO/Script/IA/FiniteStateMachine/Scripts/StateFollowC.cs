using UnityEngine;
using System.Collections;
using System;

public class StateFollowC : State
{
    public override void Handle()
    {
        switch (blackRef.enemyType)
        {
            case "furia":
                blackRef.navRef.speed = blackRef.botMovement.rangeCspeed;
                blackRef.botMovement.destination = blackRef.playerTr.position;
                blackRef.navRef.stoppingDistance = 0.1f;
                break;
            case "fante":
                blackRef.navRef.speed = blackRef.botMovement.rangeCspeed;
                blackRef.botMovement.destination = blackRef.playerTr.position;
                blackRef.navRef.stoppingDistance = 0.5f;
                break;
            case "furiaesplosiva":
                blackRef.navRef.speed = blackRef.botMovement.rangeCspeed;
                blackRef.botMovement.destination = blackRef.playerTr.position;
                blackRef.navRef.stoppingDistance = 0.1f;
                break;
            case "predatore":
                blackRef.navRef.speed = blackRef.botMovement.rangeCspeed;
                blackRef.botMovement.destination = blackRef.playerTr.position;
                blackRef.navRef.stoppingDistance = 0;
                break;
            case "titano":
                blackRef.navRef.speed = blackRef.botMovement.rangeCspeed;
                blackRef.botMovement.destination = blackRef.playerTr.position;
                blackRef.navRef.stoppingDistance = 0;
                break;
            case "cecchino":
                blackRef.navRef.speed = blackRef.botMovement.rangeCspeed;
                blackRef.botMovement.destination = blackRef.playerTr.position;
                blackRef.navRef.stoppingDistance = 0;
                break;
        }
    }
}

