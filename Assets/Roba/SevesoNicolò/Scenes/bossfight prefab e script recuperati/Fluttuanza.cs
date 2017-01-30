using UnityEngine;
using System.Collections;

public class Fluttuanza : MonoBehaviour {
    public GameObject a;
    public GameObject b;
    public float n;
    private Vector3 startPos, endPos;

    


	// Use this for initialization
	void Start ()
    {
        startPos = this.transform.position;
        endPos = startPos + new Vector3(0, 20, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(startPos, endPos, Mathf.PerlinNoise(Time.time, Time.time)/5);


	}
}
