using UnityEngine;
using System.Collections;
 

public class grenade : MonoBehaviour {

    public bool esplosa = false;
    public bool magnete = false;

    SphereCollider explosionRadius;
    public ParticleSystem pSRef;

    void Start ()
    {
        explosionRadius = GetComponentsInChildren<SphereCollider>()[1];   
        StartCoroutine(GranadeBehaviour());
	}


    IEnumerator GranadeBehaviour()
    {
        this.GetComponent<SphereCollider>().enabled = true;

        yield return new WaitForSeconds(0.7f);
        magnete = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(2);

        pSRef.Play();
        pSRef.transform.localScale = new Vector3(2,2,2);
        magnete = false;

        esplosa = true;

        this.GetComponent<SphereCollider>().isTrigger = true;
        this.GetComponent<SphereCollider>().radius = 50;

        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }
}
