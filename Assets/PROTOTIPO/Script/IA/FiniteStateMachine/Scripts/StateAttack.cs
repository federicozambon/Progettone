using UnityEngine;
using System.Collections;
using System;

public class StateAttack : State
{
    public override void EnterStateLogic()
    {
        switch (enemyType)
        {
            case "furia":
                navRef.updateRotation = true;
                break;
            case "fante":
                navRef.updateRotation = false;
                break;
            case "furiaesplosiva":
                navRef.updateRotation = false;
                break;
            case "predatore":
                navRef.updateRotation = false;
                break;
            case "titano":
             
                break;
            case "cecchino":
                navRef.updateRotation = false;
                break;
        }
    }

    public override void ExitStateLogic()
    {
        switch (enemyType)
        {
            case "furia":
                break;
            case "fante":
                navRef.updateRotation = true;
                break;
            case "furiaesplosiva":
                navRef.updateRotation = true;
                break;
            case "predatore":
                navRef.updateRotation = true;
                break;
            case "titano":

                break;
            case "cecchino":
                navRef.updateRotation = true;
                break;
        }
    }

    public override void Handle()
    {
        switch (enemyType)
        {
            case "furia":
                botMovement.destination = playerTr.position;
                navRef.speed = 10;
                navRef.updateRotation = true;
                navRef.stoppingDistance = 0f;
                navRef.angularSpeed = 1080;   
                break;
            case "fante":
                botMovement.destination = this.transform.position;
                enemyRef.Attack();
                break;
            case "furiaesplosiva":
                navRef.stoppingDistance = 0.1f;
                navRef.speed = botMovement.rangeAspeed;
                botMovement.destination = playerTr.position;
                enemyRef.Attack();
                break;
            case "predatore":
                enemyRef.Attack();
                break;
            case "titano":
                enemyRef.Attack();
                break;
            case "cecchino":
                enemyRef.Attack();
                break;
        }
    }
}