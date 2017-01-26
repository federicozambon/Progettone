using UnityEngine;
using System.Collections;

public class RangedEnemyBullet: MonoBehaviour {

	public int damage = 10;
    public float timer;

	void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
