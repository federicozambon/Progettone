using UnityEngine;
using System.Collections;
using System;

public class StateSearch : State
{
    public Vector3 randomDestination;
    public float timeToWait = 5;

    public override void Handle()
    {
        switch (blackRef.enemyType)
        {
            case "furia":
                blackRef.botMovement.destination = randomDestination;
                break;
            case "fante":
                blackRef.botMovement.destination = randomDestination;
                break;
            case "furiaesplosiva":
                blackRef.botMovement.destination = randomDestination;
                break;
            case "predatore":
                blackRef.botMovement.destination = randomDestination;
                break;
            case "titano":
                blackRef.botMovement.destination = randomDestination;
                break;
            case "cecchino":
                blackRef.botMovement.destination = this.transform.position;
                break;
        }
    }

    public override void ExitStateLogic()
    {
        StopAllCoroutines();
    }

    public IEnumerator RandomizeDestination()
    {
        randomDestination = transform.position + new Vector3(UnityEngine.Random.Range(-15, 15), UnityEngine.Random.Range(-15, 15), blackRef.playerTr.position.z);
        yield return new WaitForSeconds(timeToWait);
        StartCoroutine(RandomizeDestination());
    }
}
