using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Activators : MonoBehaviour {

    public GameObject myTrap;
    public MeshRenderer mr;
    public bool active;
    public bool isEnabled;
    public ActivatorsController aController;

	void Start ()
    {
        mr = GetComponent<MeshRenderer>(); 
        mr.material.color = Color.red;        
    }
	
	void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player" && isEnabled == true)
        {
            Debug.Log("Sono dentro");
            isEnabled = false;
            active = false;

            StartCoroutine(ActiveTrap());

            aController.DisabledActivators();
        }           
    }

    public IEnumerator ActiveTrap()
    {
        yield return new WaitForSeconds(2);
        myTrap.GetComponent<Trap>().activeTrap = true;
    }

	void Update ()
    {
	    if(active == true)
        {
            active = false;
            mr.material.color = Color.green;
            isEnabled = true;
        }
	}
}
