using UnityEngine;
using System.Collections;

public class Deactivator : MonoBehaviour
{
    public float timer;

	void OnEnable()
    {
        StartCoroutine(WaitAndDeactivate());
	}

	IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(timer);
        this.gameObject.SetActive(false);
	}
}
