using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour
{
    public bool exploded = false;

    void Start()
    {
        StartCoroutine(Destroy());
    }

    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.8f);
        exploded = true;
        this.GetComponent<SphereCollider>().enabled = true;
        yield return new WaitForSeconds(1.1f);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Enemy" && exploded == true)
        {
            
        }

        if (coll.gameObject.tag == "Player" && exploded == true)
        {

        }
    }
}
