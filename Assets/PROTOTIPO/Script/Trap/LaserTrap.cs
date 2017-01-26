using UnityEngine;
using System.Collections;

public class LaserTrap : Trap
{

    DialogueSystem dialoghi;
    public GameObject palo;
    public shootLaserTrap shootLt;
    bool laser = false;

    void Start ()
    {
        
        player = FindObjectOfType<Player>();
        dialoghi = FindObjectOfType<DialogueSystem>();
        

       
    }
	
	public override IEnumerator ActivateTrap()
    {

        shootLt.Shoot();

            yield return new WaitForSeconds(timeToRepeat);

        StartCoroutine(ActivateTrap()); 
    }

    public override IEnumerator ParticleTrap()
    {
       
        laser = true;

        

        yield return new WaitForSeconds(timeToDisable);

       
        resetTrap = true;
        laser = false;
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
            StopAllCoroutines();
            
        }

        if (laser == true)
            palo.transform.Rotate(0, 1, 0);

    }


}
