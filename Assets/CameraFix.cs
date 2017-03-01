using UnityEngine;
using System.Collections;

public class CameraFix : MonoBehaviour
{ 
	void Awake ()
    {
        this.GetComponent<Camera>().depth = 0.1f;
	}

    void Start()
    {
        this.GetComponent<Camera>().depth = 0;
    }
}
