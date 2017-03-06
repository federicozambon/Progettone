﻿using UnityEngine;
using System.Collections;

public class FireTrap: Trap
{   
    DialogueSystem dialoghi;
    public Player playerRef;
    public ReferenceManager refManager;
    Coroutine myCo;

    void Start ()
    {      
        particle = this.GetComponent<ParticleSystem>();
        
        refManager = GameObject.FindGameObjectWithTag("Reference").GetComponent<ReferenceManager>();
        playerRef = refManager.playerObj.GetComponent<Player>();
    }

    public override IEnumerator ActivateTrap()
    {
        foreach (var coll in colliders)
        {
            if (coll.gameObject.tag == "Enemy")
            {
                Enemy nemico = coll.GetComponent<Enemy>();
                nemico.TakeDamage(healthPointDamage);
            }

            if (coll.gameObject.tag == "Player")
            {
                playerRef.TakeDamage(playerPointDamage);
            }
        }

        yield return new WaitForSeconds(timeToRepeat);

        myCo = StartCoroutine(ActivateTrap());
    }

    public override IEnumerator ParticleTrap()
    {
        particle.Play();
        playSound = false;
        aController.playSound(myDie);

        yield return new WaitForSeconds(timeToDisable);
        StopCoroutine(myCo);

        particle.Stop();
        yield return new WaitForSeconds(3);
        myActivatorsController.GetComponent<ActivatorsController>().enabledAllActivators = true;

      
        resetTrap = true;      
    }

    public override IEnumerator MiniTrap()
    {
        foreach (var coll in colliders)
        {
            if (coll.gameObject.tag == "Enemy")
            {
                Enemy nemico = coll.GetComponent<Enemy>();
                nemico.TakeDamage(healthPointDamage);
            }

            if (coll.gameObject.tag == "Player")
            {

                playerRef.TakeDamage(playerPointDamage);

            }
        }
        yield return new WaitForSeconds(timeToDisable);
    }

    Coroutine Co;

    void Update()
    {
        if (activeTrap)
        {
            activeTrap = false;
            StartCoroutine(ParticleTrap());
            myCo = StartCoroutine(ActivateTrap());
        }

        if (isMiniTrap)
        {
        
            particle.Play();
            playSound = false;
            aController.playSound(myDie);
            this.transform.parent.GetComponent<MeshRenderer>().enabled = false;
            isMiniTrap = false;
            if (Co == null)
            {
                Co = StartCoroutine(MiniTrap());
            }
      

            Destroy(this.transform.parent.gameObject);
        }   

        if (resetTrap)
        {
            resetTrap = false;
            StopAllCoroutines();         
        }
    }
}
