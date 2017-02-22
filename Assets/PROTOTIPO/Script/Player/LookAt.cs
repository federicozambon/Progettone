using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{
    public Transform camera1;
	void Update ()
    {

        transform.LookAt(camera1);
        transform.Rotate(0, 180, 0);
	}
}
