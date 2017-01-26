using UnityEngine;
using System.Collections;
using System;

public class StateElectricTrapped : State
{
    public override void Handle()
    {
        Vector3 playerToBot = (botTr.position - playerTr.position);
        switch (enemyType)
        {
            case "furia":
                navRef.destination = this.transform.position + new Vector3(UnityEngine.Random.Range(-15,15), UnityEngine.Random.Range(-15, 15),0);
                break;
            case "fante":
                navRef.destination = this.transform.position + new Vector3(UnityEngine.Random.Range(-15, 15), UnityEngine.Random.Range(-15, 15), 0);
                break;
            case "furiaesplosiva":
                navRef.destination = this.transform.position + new Vector3(UnityEngine.Random.Range(-15, 15), UnityEngine.Random.Range(-15, 15), 0);
                break;
            case "predatore":
                navRef.destination = this.transform.position + new Vector3(UnityEngine.Random.Range(-15, 15), UnityEngine.Random.Range(-15, 15), 0);
                break;
            case "titano":
                navRef.destination = this.transform.position + new Vector3(UnityEngine.Random.Range(-15, 15), UnityEngine.Random.Range(-15, 15), 0);
                break;
            case "cecchino":
                navRef.destination = this.transform.position + new Vector3(UnityEngine.Random.Range(-15, 15), UnityEngine.Random.Range(-15, 15), 0);
                break;
        }
    }
}