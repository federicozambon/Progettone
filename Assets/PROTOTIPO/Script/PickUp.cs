using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public void Hide()
    {
        this.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
    }

    public void Show ()
    {
        this.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;

    }
}
