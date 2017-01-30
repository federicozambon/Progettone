﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Trap: MonoBehaviour
{
    public bool activeTrap= false;
    public bool resetTrap = false;
    public bool playerTrapped = false;
    public float timeToDisable = 50f;
    public float timeToRepeat = 0.5f;
    public float repeatDamage = 1;
    public int healthPointDamage = 1;
    public int playerPointDamage = 1;
    public ParticleSystem particle;
    public Collider coll;
    public List<GameObject> enemies;
    public Player player;
    public GameObject myActivatorsController;
    public bool isMiniTrap = false;

    void Start ()
    {
        
    }

    virtual public IEnumerator ActivateTrap()
    {
        yield return null;
    }

    virtual public IEnumerator ParticleTrap()
    {
        yield return null;
    }


    virtual public IEnumerator Damage()
    {
        yield return null;
    }


    virtual public void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            enemies.Add(coll.gameObject);
        }

        if (coll.gameObject.tag == "Player")
        {
            playerTrapped = true;
        }
    } 
}