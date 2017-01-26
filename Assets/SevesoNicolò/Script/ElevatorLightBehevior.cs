using UnityEngine;
using System.Collections;

public class ElevatorLightBehevior : MonoBehaviour {

   public float speedrotation = 50;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(0, speedrotation * Time.deltaTime, 0);
	}
}
