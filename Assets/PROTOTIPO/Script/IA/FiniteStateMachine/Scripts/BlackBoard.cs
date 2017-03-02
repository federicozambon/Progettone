using UnityEngine;
using System.Collections;
using FSM;

public class BlackBoard : MonoBehaviour
{
    public Enemy enemyRef;
    public Movement botMovement;
    public Transform playerTr;
    public Transform botTr;
    public NavMeshAgent navRef;
    public string enemyType;
    public Transform attractionTransform;
    public float timer;
    public ReferenceManager refManager;
    public TextMesh textMesh;

    public StateIdle stateIdle;
    public StateFlee stateFlee;
    public StateSearch stateSearch;
    public StateFollowA stateFollowA;
    public StateFollowB stateFollowB;
    public StateFollowC stateFollowC;
    public StateIceTrapped stateIceTrap;
    public StateAttractionTrapped stateAttractionTrap;
    public StateElectricTrapped stateElectricTrap;
    public StateAttack stateAttack;

    void Awake ()
    {
        refManager = GameObject.FindGameObjectWithTag("Reference").GetComponent<ReferenceManager>();
	    enemyRef = GetComponent<Enemy>();
        enemyType = enemyRef.enemyType;
        botMovement = GetComponent<Movement>();
        navRef = GetComponent<NavMeshAgent>();
        playerTr = refManager.playerRef.transform;
        botTr = enemyRef.transform;
        attractionTransform = enemyRef.attractionTrap;
	}
}
