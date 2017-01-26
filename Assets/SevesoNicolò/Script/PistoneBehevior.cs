using UnityEngine;
using System.Collections;

public class PistoneBehevior : MonoBehaviour {
    int RandomCounter;
    public bool stoprandom;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(stoprandom == false)
        {
            RandomCounter = Random.Range(-10, 50);
        }
	if ( RandomCounter <= 0)
        {
            transform.Rotate(0, 1*Time.deltaTime,0);
        }
    }
}
