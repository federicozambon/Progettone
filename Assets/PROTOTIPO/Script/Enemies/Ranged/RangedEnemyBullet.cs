using UnityEngine;
using System.Collections;

public class RangedEnemyBullet: MonoBehaviour {

	public int damage = 10;
    public float timer;

	void OnTriggerEnter(Collider coll)
    {
        Debug.LogError(coll.gameObject);
        if (coll.gameObject.tag == "Player")
        {
            FindObjectOfType<Player>().TakeDamage(damage);
        }
    }
}
