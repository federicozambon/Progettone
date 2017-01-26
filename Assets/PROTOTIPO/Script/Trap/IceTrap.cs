using UnityEngine;
using System.Collections;

public class IceTrap : Trap
{
    public GameObject iceEffect;
    

    public float slowdown = 1f;

    void Start ()
    {

        player = FindObjectOfType<Player>();
        coll = this.GetComponent<SphereCollider>();
        coll.enabled = false;
    }
	
	public override IEnumerator ActivateTrap()
    {
        activeTrap = false;

        foreach (var enemy in enemies)
        {
            if (enemy.GetComponent<Enemy>().trapped == false)
            {
                enemy.GetComponent<Enemy>().trapped = true;
                enemy.GetComponent<Enemy>().isActiveIceTrap = true;
                enemy.GetComponent<Enemy>().TrapController(timeToDisable);
            }

        }

        if (playerTrapped == true)
        {
            player.speed = 2;
        }

        yield return new WaitForSeconds(timeToRepeat);

        

        StartCoroutine(ActivateTrap()); 
    }

    public override IEnumerator ParticleTrap()
    {
        iceEffect.GetComponent<LazyLoad>().enabled = true;
        coll.enabled = true;

        yield return new WaitForSeconds(timeToDisable);

        if (playerTrapped == true)
        {
            playerTrapped = false;
            player.speed = 6;
        }

        if (isMiniTrap)
            Destroy(this.gameObject);

        if (isMiniTrap == false)
        {
            iceEffect.GetComponent<LazyLoad>().enabled = false;

            myActivatorsController.GetComponent<ActivatorsController>().enabledAllActivators = true;

            coll.enabled = false;
            resetTrap = true;
        }
        
    }



    void Update()
    {
        if (activeTrap == true)
        {
            
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
