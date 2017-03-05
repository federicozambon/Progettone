using UnityEngine;
using System.Collections;

public class RangedEnemyBullet: MonoBehaviour {

	public int damage = 10;
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
