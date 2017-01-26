using UnityEngine;
using System.Collections;

public class RightWheelRotation: MonoBehaviour
{
	void Update ()
    {
        this.transform.Rotate(0, Time.deltaTime * -90, 0);
    }
}
