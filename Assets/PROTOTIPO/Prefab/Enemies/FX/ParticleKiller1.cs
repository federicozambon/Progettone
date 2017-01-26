using UnityEngine;
using System.Collections;

public class ParticleKiller1 : MonoBehaviour
{

	void Start ()
    {
        StartCoroutine(Killer());
	}
    
    IEnumerator Killer()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
