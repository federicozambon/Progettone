using UnityEngine;
using System.Collections;

public class Aspect : MonoBehaviour {

    
	void Start ()
    {
        FourTree();
        
    }

    public void FourTree()
    {
        this.GetComponent<Camera>().aspect = 4 / 3;
        this.GetComponent<Camera>().rect = new Rect(0.165f, 0, 0.67f, 1);
    }

    public void SixteenNine()
    {
        this.GetComponent<Camera>().aspect = 16 / 9;
        this.GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
    }

    
}
