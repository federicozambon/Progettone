using UnityEngine;
using System.Collections;

public class ElectroTrapBehevior : Trap {
    public GameObject particellare1;
    public GameObject particellare2;
    public float timer;
    public bool stoptimer = false;
    public bool start;


	
	

    public override IEnumerator ActivateTrap()
    {

        particellare1.SetActive(true);
        coll.enabled = true;


        yield return new WaitForSeconds(timeToDisable);

        particellare1.SetActive(false);
        particellare2.SetActive(true);
        coll.enabled = false;
        resetTrap = true;
    }


    void Update()
    {
        if (activeTrap)
        {
            activeTrap = false;
            StartCoroutine(ActivateTrap());
        }

        if (resetTrap)
        {
            resetTrap = false;
            myActivatorsController.GetComponent<ActivatorsController>().EnabledActivators();
            
        }
    }


}
