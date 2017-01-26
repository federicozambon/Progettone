using UnityEngine;
using System.Collections;
 

public class explosion_grenade: MonoBehaviour
{
    grenade grenadeRef;


    void Awake()
    {
        grenadeRef = GetComponentInParent<grenade>();
    }

    void OnTriggerStay(Collider coll)
    {
        float range = this.GetComponent<SphereCollider>().radius;

        if (coll.gameObject.tag == "Enemy" && grenadeRef.magnete)
        {
            coll.gameObject.GetComponent<Enemy>().knockbacked = true;
         
            coll.gameObject.GetComponent<Enemy>().isActiveAttractionTrap = true;
            coll.gameObject.GetComponent<Enemy>().attractionTrap = this.transform;
        }
        if (coll.gameObject.tag == "Enemy" && grenadeRef.esplosa == true)
        {
            coll.gameObject.GetComponent<Rigidbody>().AddExplosionForce(2, this.transform.position, 20, 2, ForceMode.Impulse);
            coll.gameObject.GetComponent<Enemy>().repulsion = true;

            coll.gameObject.GetComponent<Enemy>().TakeDamage(20);  
        }
    }
}
