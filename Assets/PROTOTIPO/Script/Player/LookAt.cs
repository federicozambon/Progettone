using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{
    Transform camera1;

    private void Start()
    {
        camera1 = GameObject.FindGameObjectWithTag("Reference").GetComponent<ReferenceManager>().camTr;
    }

    void Update ()
    {
        transform.LookAt(camera1);
        transform.Rotate(0, 180, 0);
	}
}
