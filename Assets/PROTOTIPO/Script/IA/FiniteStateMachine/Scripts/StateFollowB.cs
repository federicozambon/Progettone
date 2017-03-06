using UnityEngine;
using System.Collections;
using System;

public class StateFollowB : State
{
    public override void Handle()
    {
        switch (blackRef.enemyType)
        {
            case "furia":
                blackRef.navRef.speed = blackRef.botMovement.rangeBspeed;
                blackRef.botMovement.destination = blackRef.playerTr.position;
                blackRef.navRef.stoppingDistance = 0.1f;
                break;
            case "fante":
                blackRef.navRef.speed = blackRef.botMovement.rangeBspeed;
                blackRef.botMovement.destination = blackRef.playerTr.position;
                //shoot
                break;
            case "furiaesplosiva":
                blackRef.navRef.speed = blackRef.botMovement.rangeBspeed;
                blackRef.botMovement.destination = blackRef.playerTr.position;
                blackRef.navRef.stoppingDistance = 0.1f;
                break;
            case "predatore":
                blackRef.navRef.speed = blackRef.botMovement.rangeBspeed;
                blackRef.botMovement.destination = blackRef.playerTr.position;
                blackRef.navRef.stoppingDistance = 0.1f;
                break;
            case "titano":
                blackRef.navRef.speed = blackRef.botMovement.rangeBspeed;
                blackRef.botMovement.destination = blackRef.playerTr.position;
                blackRef.navRef.stoppingDistance = 0;
                break;
            case "cecchino":
                blackRef.navRef.speed = blackRef.botMovement.rangeBspeed;
                blackRef.botMovement.destination = this.transform.position;
                blackRef.navRef.stoppingDistance = 0;
                break;
        }
    }
}

