using UnityEngine;
using System.Collections;

public class MovimentoPistone : MonoBehaviour {
    int RandomNumberForStart;
    public bool stoprandomnumber = false;
    public int randomstart;
    public bool StartMoving;
    public int speedmovment = 1;
    public bool pistonesugiu = false;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (stoprandomnumber == false)
        {
            RandomNumberForStart = Random.Range(-1, 1 * randomstart);
        }
        if (RandomNumberForStart <= 0)
        {
            StartMoving = true;

        }
        if (StartMoving == true && pistonesugiu == false)
        {
            transform.Translate(0, -speedmovment * Time.deltaTime, 0);
        }
        else
        {
            transform.Translate(0, speedmovment * Time.deltaTime, 0);
        }
    
        if(transform.position.y < 0)
        {
            pistonesugiu = true;
        }
        else if (transform.position.y > 6)
        {
            pistonesugiu = false;
        }
        
	
	}
}
