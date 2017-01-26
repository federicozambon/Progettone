using UnityEngine;
using System.Collections;
using System;

public class StateSearch : State
{
    public Vector3 randomDestination;
    public float timeToWait = 5;

    public override void Handle()
    {
        switch (enemyType)
        {
            case "furia":
                botMovement.destination = randomDestination;
                break;
            case "fante":
                botMovement.destination = randomDestination;
                break;
            case "furiaesplosiva":
                botMovement.destination = randomDestination;
                break;
            case "predatore":
                botMovement.destination = randomDestination;
                break;
            case "titano":
                botMovement.destination = randomDestination;
                break;
            case "cecchino":
                botMovement.destination = randomDestination;
                break;
        }
    }

    public override void ExitStateLogic()
    {
        StopAllCoroutines();
    }

    public IEnumerator RandomizeDestination()
    {
        randomDestination = transform.position + new Vector3(UnityEngine.Random.Range(-15, 15), UnityEngine.Random.Range(-15, 15), playerTr.position.z);
        yield return new WaitForSeconds(timeToWait);
        StartCoroutine(RandomizeDestination());
    }
}
