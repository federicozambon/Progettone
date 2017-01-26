using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class Muoviti : MonoBehaviour {
    public Transform CurrentPos, MainPos, MontacarichiPos, DiscaricaPos, TettoPos, FonderiaPos, PalazzoPos;

	// Use this for initialization
	void Start ()

    {
	

	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, CurrentPos.position, 0.05f);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, CurrentPos.rotation, 0.05f);
	
	}

    public void Menu()

    {
        CurrentPos = MainPos;

    }

    public void Montacarichi()

    {
        CurrentPos = MontacarichiPos;

    }

    public void Discarica()

    {
        CurrentPos = DiscaricaPos;

    }

    public void Tetto()

    {
        CurrentPos = TettoPos;
   

    }

    public void Fonderia()

    {
        CurrentPos = FonderiaPos;

    }


   public void Palazzo()

    {
        CurrentPos = PalazzoPos;


    }

    
}


