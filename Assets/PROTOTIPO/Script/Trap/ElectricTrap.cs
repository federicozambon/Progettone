using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElectricTrap : Trap
{

    DialogueSystem dialoghi;
    public List<GameObject> rayObj;
    public Player playerRef;

	void Start ()
    {
        //particle = this.GetComponent<ParticleSystem>();
        coll = this.GetComponent<SphereCollider>();   
        //coll.enabled = false;
    }

    
    public override void OnTriggerEnter(Collider coll)
    {
        //inserito metodo override per evitare che gli attivatori venissero utilizzati dalla trappola per triggerare il player e fargli danno
    }


    public override IEnumerator ActivateTrap()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.GetComponent<Enemy>().trapped == false)
            {
                enemy.GetComponent<Enemy>().trapped = true;
                enemy.GetComponent<Enemy>().isActiveAttractionTrap = true;
                enemy.GetComponent<Enemy>().TrapController(timeToDisable);
            }

        }

        yield return new WaitForSeconds(timeToRepeat);

        if (playerTrapped == true)
        {
            playerRef.TakeDamage(playerPointDamage);

        }



            yield return new WaitForSeconds(timeToRepeat);

        StartCoroutine(ActivateTrap()); 
    }

    public override IEnumerator ParticleTrap()
    {
        playSound = false;
        aController.playSound(myDie);

        foreach (var ray in rayObj)
        {
            ray.GetComponent<RayElectricTrap>().coll.enabled = true;
            ray.GetComponent<RayElectricTrap>().mr.enabled = true;

        }

        yield return new WaitForSeconds(timeToDisable);


        foreach (var ray in rayObj)
        {
            ray.GetComponent<RayElectricTrap>().coll.enabled = false;
            ray.GetComponent<RayElectricTrap>().mr.enabled = false;

        }

        myActivatorsController.GetComponent<ActivatorsController>().enabledAllActivators = true;
    }

   

    void Update()
    {
        if (activeTrap == true)
        {
            activeTrap = false;
            StartCoroutine(ParticleTrap());
            StartCoroutine(ActivateTrap());
        }

        if (resetTrap == true)
        {
            resetTrap = false;
            enemies.Clear();
        }

    }


}
