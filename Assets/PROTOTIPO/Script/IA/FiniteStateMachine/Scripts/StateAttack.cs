using UnityEngine;
using System.Collections;
using System;

public class StateAttack : State
{
    public override void EnterStateLogic()
    {
        switch (blackRef.enemyType)
        {
            case "furia":
                //navRef.updateRotation = true;
                break;
            case "fante":
                blackRef.navRef.updateRotation = false;
                break;
            case "furiaesplosiva":
                blackRef.navRef.updateRotation = false;
                break;
            case "predatore":
                //blackRef.navRef.updateRotation = false;
                break;
            case "titano":
             
                break;
            case "cecchino":
                blackRef.navRef.updateRotation = false;
                break;
        }
    }

    public override void ExitStateLogic()
    {
        switch (blackRef.enemyType)
        {
            case "furia":
                break;
            case "fante":
                blackRef.navRef.updateRotation = true;
                break;
            case "furiaesplosiva":
                blackRef.navRef.updateRotation = true;
                break;
            case "predatore":
                //blackRef.navRef.updateRotation = true;
                break;
            case "titano":

                break;
            case "cecchino":
                blackRef.navRef.updateRotation = true;
                break;
        }
    }

    public override void Handle()
    {
        switch (blackRef.enemyType)
        {
            case "furia":
                blackRef.botMovement.destination = blackRef.playerTr.position;
                blackRef.navRef.speed = 12;
                blackRef.navRef.updateRotation = true;
                blackRef.navRef.stoppingDistance = 0f;
                blackRef.navRef.angularSpeed = 1080;   
                break;
            case "fante":
                blackRef.botMovement.destination = this.transform.position;
                blackRef.enemyRef.Attack();
                break;
            case "furiaesplosiva":
                blackRef.navRef.stoppingDistance = 0.1f;
                blackRef.navRef.speed = blackRef.botMovement.rangeAspeed;
                blackRef.botMovement.destination = blackRef.playerTr.position;
                blackRef.enemyRef.Attack();
                break;
            case "predatore":
                blackRef.botMovement.destination = blackRef.playerTr.position;
                blackRef.navRef.speed = 12;
                blackRef.navRef.updateRotation = true;
                blackRef.navRef.stoppingDistance = 0f;
                blackRef.navRef.angularSpeed = 1080;
                break;
            case "titano":
                blackRef.enemyRef.Attack();
                break;
            case "cecchino":
                blackRef.enemyRef.Attack();
                break;
        }
    }
}