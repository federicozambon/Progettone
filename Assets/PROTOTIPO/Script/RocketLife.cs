using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RocketLife : MonoBehaviour {

    public List<Enemy> enemies;
    public int damagePerShot = 20;
    bool isExploded = false;

    void OnTriggerEnter(Collider coll)
    {

        transform.GetChild(0).gameObject.SetActive(true);
        if (isExploded == false)
            StartCoroutine(Explosion());
    }
    
    public float speed = 1;

    void Update()
    {
        if (!isExploded)
        {
            this.transform.position += transform.forward * speed;
        } 
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            if (!enemies.Contains(coll.gameObject.GetComponent<Enemy>()))
            {
                enemies.Add(coll.gameObject.GetComponent<Enemy>());
            }
        }
    }
	
    IEnumerator Explosion()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<SphereCollider>().enabled = true;

        isExploded = true;
        yield return new WaitForSeconds(1);
        foreach (var enemy in enemies)
        {
            enemy.TakeDamage(damagePerShot);
        }
        
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
