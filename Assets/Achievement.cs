using UnityEngine;
using System.Collections;

public class Achievement : MonoBehaviour {

    public int total = 0;
    public int tutorial = 0;
    public int montacarichiA = 0;
    public int discarica = 0;
    public int montacarichiB = 0;
    public int tetto = 0;
	
	void Start () {

        DontDestroyOnLoad(this.gameObject);
	}
	
	
	void SaveScore () {
	
	}
}
