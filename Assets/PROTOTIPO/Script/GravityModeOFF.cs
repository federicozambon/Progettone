using UnityEngine;
using System.Collections;

public class GravityModeOFF : MonoBehaviour {

	
	void Start ()
    {
	
	}
	
	
	void OnTriggerEnter (Collider coll)
    {
	    if (coll.gameObject.tag == "Enemy")
        {
            coll.GetComponent<Rigidbody>().isKinematic = true;
        }
	}
}
