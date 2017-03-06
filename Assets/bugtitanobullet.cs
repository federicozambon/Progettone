using UnityEngine;
using System.Collections;

public class bugtitanobullet : MonoBehaviour
{

    public int damage = 15;
    public float timer;

    ReferenceManager refManager;

    private void Awake()
    {
        refManager = GameObject.FindGameObjectWithTag("Reference").GetComponent<ReferenceManager>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            refManager.playerRef.TakeDamage(damage);
        }
    }
}