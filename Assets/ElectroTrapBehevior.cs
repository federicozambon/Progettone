using UnityEngine;
using System.Collections;

public class ElectroTrapBehevior : MonoBehaviour {
    public GameObject particellare1;
    public GameObject particellare2;
    public float timer;
    public bool stoptimer = false;
    public bool start;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space) && stoptimer == false)
        {
            start = true;
            

        }
        if ( start == true)
        {
            particellare1.SetActive(true);
            timer += Time.deltaTime;
        }

        if(timer >= 5)
        {
            stoptimer = true;
            particellare1.SetActive(false);
            particellare2.SetActive(true);

            
        }
	
	}
}
