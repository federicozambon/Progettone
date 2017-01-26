using UnityEngine;
using System.Collections;
using System;

public class StateAttractionTrapped : State
{
    public override void Handle()
    {
        switch (enemyType)
        {
            case "furia":

                navRef.destination = attractionTransform.position;
                break;
            case "fante":
                navRef.destination = attractionTransform.position;
                break;
            case "furiaesplosiva":
                navRef.destination = attractionTransform.position;
                break;
            case "predatore":
                navRef.destination = attractionTransform.position;
                break;
            case "titano":
                navRef.destination = attractionTransform.position;
                break;
            case "cecchino":
                navRef.destination = attractionTransform.position;
                break;
        }
    }
}