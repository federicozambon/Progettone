using UnityEngine;
using System.Collections;

public class BlackBoard : MonoBehaviour {

    public Enemy enemyRef;
    public Movement botMovement;
    public Transform playerTr;
    public Transform botTr;
    public NavMeshAgent navRef;
    public string enemyType;
    public Transform attractionTransform;
    public float timer;

    void Awake () {
	    enemyRef = GetComponentInParent<Enemy>();
        enemyType = enemyRef.enemyType;
        botMovement = GetComponentInParent<Movement>();
        navRef = GetComponentInParent<NavMeshAgent>();
        playerTr = FindObjectOfType<Player>().transform;
        botTr = transform.parent.transform;
        attractionTransform = enemyRef.attractionTrap;
	}
}
