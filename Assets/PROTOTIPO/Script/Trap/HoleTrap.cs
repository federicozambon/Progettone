using UnityEngine;
using System.Collections;

public class HoleTrap : Trap
{
    public GameObject iceEffect;
    public HolePanel hPanel;
    

    public float slowdown = 1f;

    void Start ()
    {

        player = FindObjectOfType<Player>();
        coll = this.GetComponent<BoxCollider>();
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
                enemy.GetComponent<Enemy>().isActiveAttractionTrap = true;
                enemy.GetComponent<Enemy>().TrapController(timeToDisable);
            }

        }


        yield return new WaitForSeconds(timeToRepeat);

        

        StartCoroutine(ActivateTrap()); 
    }

    public override IEnumerator ParticleTrap()
    {
        
        coll.enabled = true;

        yield return new WaitForSeconds(timeToDisable);

       
        
            

            myActivatorsController.GetComponent<ActivatorsController>().enabledAllActivators = true;

            coll.enabled = false;
            resetTrap = true;
       
        
    }



    void Update()
    {
        if (activeTrap == true)
        {
            
            StartCoroutine(ParticleTrap());
            StartCoroutine(ActivateTrap());
            hPanel.active = true;
        }

        if (resetTrap == true)
        {
            resetTrap = false;
            enemies.Clear();

            
        }

    }


}
