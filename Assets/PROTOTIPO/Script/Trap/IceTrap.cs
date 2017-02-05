using UnityEngine;
using System.Collections;

public class IceTrap : Trap
{
    public GameObject iceEffect;
    

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
        }

        yield return new WaitForSeconds(timeToRepeat);

        

        StartCoroutine(ActivateTrap()); 
    }

    public override IEnumerator ParticleTrap()
    {
        iceEffect.GetComponent<LazyLoad>().enabled = true;
        

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
 
        }

    }


}
