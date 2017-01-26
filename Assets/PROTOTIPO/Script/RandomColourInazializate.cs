using UnityEngine;
using System.Collections;

public class RandomColourInazializate : MonoBehaviour
{
	void Start ()
    {
        int randomValue = Random.Range(0,4);

        switch (randomValue)
        {
            case 0:
                this.GetComponent<MeshRenderer>().material.color = new Color(0.2f,0,0, 1);
                break;
            case 1:
                this.GetComponent<MeshRenderer>().material.color = new Color(0, 0.2f, 0, 1);
                break;
            case 2:
                this.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0.2f, 1);
                break;
            case 3:
                this.GetComponent<MeshRenderer>().material.color = new Color(0.1f, 0.1f, 0, 1);
                break;
            case 4:
                this.GetComponent<MeshRenderer>().material.color = new Color(0.1f, 0, 0.1f, 1);
                break;
        }
	}
}
