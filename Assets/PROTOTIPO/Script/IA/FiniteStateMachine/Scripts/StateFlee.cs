using UnityEngine;
using System.Collections;
using System;

public class StateFlee : State
{
    public override void Handle()
    {
        Vector3 playerToBot = (botTr.position - playerTr.position);
        switch (enemyType)
        {
            case "furia":
                navRef.destination = playerToBot;
                break;
            case "fante":
                navRef.destination = playerToBot;
                break;
            case "furiaesplosiva":
                navRef.destination = playerToBot;
                break;
            case "predatore":
                navRef.destination = playerToBot;
                break;
            case "titano":
                navRef.destination = playerToBot;
                break;
            case "cecchino":
                navRef.destination = playerToBot;
                break;
        }
    }
}