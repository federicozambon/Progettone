using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{
    Transform camera1;

    private void Awake()
    {
        camera1 = GameObject.Find("MainCamera").transform;
    }

    void Update ()
    {
        transform.LookAt(camera1);
        transform.Rotate(0, 180, 0);
	}
}
