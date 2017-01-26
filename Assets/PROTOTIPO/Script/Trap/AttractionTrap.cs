using UnityEngine;
using System.Collections;

public class AttractionTrap : Trap
{

    public float slowdown = 1f;
    public float strengthOfAttraction = 5.0f;

    public GameObject thisTrap;

    void Start ()
    {
        
        coll = GetComponent<BoxCollider>();
        player = FindObjectOfType<Player>();
        coll.enabled = false;
        thisTrap.GetComponent<EffectSettings>().IsVisible = false;
        



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
                enemy.GetComponent<Enemy>().attractionTrap = this.transform;
                Debug.Log(enemy.GetComponent<Enemy>().attractionTrap);
            }

        }
            yield return new WaitForSeconds(timeToRepeat);
        StartCoroutine(ActivateTrap()); 
    }

    public override IEnumerator ParticleTrap()
    {
        
        coll.enabled = true;
        thisTrap.GetComponent<EffectSettings>().IsVisible = true;

        yield return new WaitForSeconds(timeToDisable);

        
        
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<Enemy>().isActiveAttractionTrap = false;
                enemy.GetComponent<Enemy>().trapped = false;
            }

        }

        myActivatorsController.GetComponent<ActivatorsController>().enabledAllActivators = true;

        coll.enabled = false;
        thisTrap.GetComponent<EffectSettings>().IsVisible = false;
        resetTrap = true;
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

        foreach (var enemy in enemies)
        {
            // Vector3 direction = enemy.transform.position - transform.position;
            // enemy.GetComponent<Rigidbody>().AddForce(strengthOfAttraction * direction);
            Vector3 magnet = this.transform.position - enemy.gameObject.transform.position;
           // float index = (range - magnet.magnitude) / range;
            enemy.gameObject.GetComponent<Rigidbody>().AddForce( magnet);
        }

    }


}
