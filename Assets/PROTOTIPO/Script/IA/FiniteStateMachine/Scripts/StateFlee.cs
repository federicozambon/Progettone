using UnityEngine;
using System.Collections;
using System;

public class StateFlee : State
{
    public override void Handle()
    {
        Vector3 playerToBot = (blackRef.botTr.position - blackRef.playerTr.position);
        switch (blackRef.enemyType)
        {
            case "furia":
                blackRef.navRef.destination = playerToBot;
                break;
            case "fante":
                blackRef.navRef.destination = playerToBot;
                break;
            case "furiaesplosiva":
                blackRef.navRef.destination = playerToBot;
                break;
            case "predatore":
                blackRef.navRef.destination = playerToBot;
                break;
            case "titano":
                blackRef.navRef.destination = playerToBot;
                break;
            case "cecchino":
                blackRef.botMovement.destination = this.transform.position;
                break;
        }
    }
}