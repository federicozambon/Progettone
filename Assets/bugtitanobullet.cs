using UnityEngine;
using System.Collections;

public class bugtitanobullet : MonoBehaviour
{

    public int damage = 15;
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