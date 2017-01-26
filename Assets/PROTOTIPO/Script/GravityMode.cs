using UnityEngine;
using System.Collections;

public class GravityMode : MonoBehaviour {

	
	void Start ()
    {
	
	}
	
	
	void OnTriggerEnter (Collider coll)
    {
	    if (coll.gameObject.tag == "Enemy")
        {
            coll.GetComponent<Rigidbody>().isKinematic = false;
        }
	}
}
