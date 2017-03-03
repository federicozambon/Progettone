using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IceTrap : Trap
{
    public GameObject iceEffect;
    Coroutine myCo;
    public List<Enemy> enemyList;

    public float slowdown = 1f;

    void Start ()
    {       
        player = FindObjectOfType<Player>();      
    }
	
	public override IEnumerator ActivateTrap()
    {
        activeTrap = false;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<Player>() != null)
            {
                colliders[i].GetComponent<Player>().speed = 2;
                playerTrapped = true;
            }
            if (colliders[i].GetComponent<Enemy>())
            {
                enemyList.Add(colliders[i].GetComponent<Enemy>());
                colliders[i].GetComponent<Enemy>().isActiveIceTrap = true;
            }
        }

        yield return new WaitForSeconds(timeToRepeat);

        myCo = StartCoroutine(ActivateTrap()); 
    }

    public override IEnumerator ParticleTrap()
    {
        iceEffect.GetComponent<LazyLoad>().enabled = true;

        playSound = false;
        aController.playSound(myDie);

        yield return new WaitForSeconds(timeToDisable);

        if (playerTrapped == true)
        {
            playerTrapped = false;
            player.speed = 6;
        }

        foreach (var item in enemyList)
        {
            item.isActiveIceTrap = false;
        }
        enemyList.Clear();

        if (isMiniTrap)
            Destroy(this.gameObject);

        if (isMiniTrap == false)
        {
            iceEffect.GetComponent<LazyLoad>().enabled = false;

            yield return new WaitForSeconds(3);
            myActivatorsController.GetComponent<ActivatorsController>().enabledAllActivators = true;

            StopCoroutine(myCo);
            resetTrap = true;
        }
        else
        {        
            iceEffect.GetComponent<LazyLoad>().enabled = false;
            Destroy(this.transform.parent.gameObject);
        }       
    }

    void Update()
    {
        if (activeTrap == true)
        {        
            StartCoroutine(ParticleTrap());
            myCo = StartCoroutine(ActivateTrap());
        }

        if (isMiniTrap == true)
        {
            this.transform.parent.GetComponent<MeshRenderer>().enabled = false;
            if (playSound == true)
            {
                playSound = false;
                aController.playSound(myDie);
            }         
        }

        if (resetTrap == true)
        {
            resetTrap = false;
        }
    }
}
