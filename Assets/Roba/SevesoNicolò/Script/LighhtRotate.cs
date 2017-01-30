using UnityEngine;
using System.Collections;

public class LighhtRotate : MonoBehaviour {

    public float speedrotation = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(0f, speedrotation * Time.deltaTime, 0f);
	}
}
