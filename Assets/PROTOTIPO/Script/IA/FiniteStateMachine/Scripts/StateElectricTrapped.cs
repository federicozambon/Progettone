using UnityEngine;
using System.Collections;
using System;

public class StateElectricTrapped : State
{
    public override void Handle()
    {
        Vector3 playerToBot = (blackRef.botTr.position - blackRef.playerTr.position);
        switch (blackRef.enemyType)
        {
            case "furia":
                blackRef.navRef.destination = this.transform.position + new Vector3(UnityEngine.Random.Range(-15,15), UnityEngine.Random.Range(-15, 15),0);
                break;
            case "fante":
                blackRef.navRef.destination = this.transform.position + new Vector3(UnityEngine.Random.Range(-15, 15), UnityEngine.Random.Range(-15, 15), 0);
                break;
            case "furiaesplosiva":
                blackRef.navRef.destination = this.transform.position + new Vector3(UnityEngine.Random.Range(-15, 15), UnityEngine.Random.Range(-15, 15), 0);
                break;
            case "predatore":
                blackRef.navRef.destination = this.transform.position + new Vector3(UnityEngine.Random.Range(-15, 15), UnityEngine.Random.Range(-15, 15), 0);
                break;
            case "titano":
                blackRef.navRef.destination = this.transform.position + new Vector3(UnityEngine.Random.Range(-15, 15), UnityEngine.Random.Range(-15, 15), 0);
                break;
            case "cecchino":
                blackRef.navRef.destination = this.transform.position + new Vector3(UnityEngine.Random.Range(-15, 15), UnityEngine.Random.Range(-15, 15), 0);
                break;
        }
    }
}