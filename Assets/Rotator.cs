using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    public Transform tr1;

	void OnEnable ()
    {
        Transform tr = GameObject.FindGameObjectWithTag("MainCamera").transform;

        Vector3 position = new Vector3(tr.position.x, this.transform.position.y, tr.position.z);

        tr1.position = position;
        this.transform.LookAt(tr1);
	}
}
