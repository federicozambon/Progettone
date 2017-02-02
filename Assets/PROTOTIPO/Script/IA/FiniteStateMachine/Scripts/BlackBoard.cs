﻿using UnityEngine;
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
        refManager = FindObjectOfType<ReferenceManager>();
	    enemyRef = GetComponentInParent<Enemy>();
        enemyType = enemyRef.enemyType;
        botMovement = GetComponentInParent<Movement>();
        navRef = GetComponentInParent<NavMeshAgent>();
        playerTr = refManager.playerRef.transform;
        botTr = transform.parent.transform;
        attractionTransform = enemyRef.attractionTrap;
	}
}
