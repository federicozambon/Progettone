using UnityEngine;
using System.Collections;
using System;

public class StateAttractionTrapped : State
{
    public override void Handle()
    {
        switch (blackRef.enemyType)
        {
            case "furia":

                blackRef.navRef.destination = blackRef.attractionTransform.position;
                break;
            case "fante":
                blackRef.navRef.destination = blackRef.attractionTransform.position;
                break;
            case "furiaesplosiva":
                blackRef.navRef.destination = blackRef.attractionTransform.position;
                break;
            case "predatore":
                blackRef.navRef.destination = blackRef.attractionTransform.position;
                break;
            case "titano":
                blackRef.navRef.destination = blackRef.attractionTransform.position;
                break;
            case "cecchino":
                blackRef.navRef.destination = blackRef.attractionTransform.position;
                break;
        }
    }
}