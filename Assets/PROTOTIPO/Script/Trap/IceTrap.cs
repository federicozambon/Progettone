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
                colliders[i].GetComponent<Player>().speed = 3;

                if (player.Co == null)
                {
                    player.Co = StartCoroutine(player.DefrostPlayer());
                }
                else
                {
                    StopCoroutine(player.Co);
                    player.Co = StartCoroutine(player.DefrostPlayer());
                }
        
                playerTrapped = true;
            }
            if (colliders[i].GetComponent<Enemy>())
            {
                enemyList.Add(colliders[i].GetComponent<Enemy>());
                colliders[i].GetComponent<Enemy>().navRef.Stop();
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

        StopCoroutine(myCo);

        foreach (var item in enemyList)
        {
            item.GetComponent<Enemy>().navRef.Resume();
        }
        enemyList.Clear();

        if (isMiniTrap)
            Destroy(this.gameObject);

        if (isMiniTrap == false)
        {
            iceEffect.GetComponent<LazyLoad>().enabled = false;

            yield return new WaitForSeconds(3);
            myActivatorsController.GetComponent<ActivatorsController>().enabledAllActivators = true;

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
