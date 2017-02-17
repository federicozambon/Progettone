using UnityEngine;
using System.Collections;

public class volumeprova : MonoBehaviour {

    AudioSource a;
    public float time = 0.1f;
    

	void Start () {

        a = GetComponent<AudioSource>();
        StartCoroutine(audioS());

	}
	
	IEnumerator audioS()
    {
        yield return new WaitForSeconds(time);
        a.volume -= time;
        if (a.volume > 0)
            StartCoroutine(audioS());

    }

	void Update () {
	
	}
}
