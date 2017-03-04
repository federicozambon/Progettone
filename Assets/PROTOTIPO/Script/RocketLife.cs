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
    }
    
    public float speed = 1;

    void Update()
    {
        if (!isExploded)
        {
            this.transform.position += transform.forward * speed;
            Collider[] colliders = Physics.OverlapCapsule(this.transform.position, this.transform.position + new Vector3(0, 0, 1), 1);
            foreach (var item in colliders)
            {
                if (item.tag != "Player" && isExploded == false && item.tag != "Trap")
                {
                    StartCoroutine(Explosion());
                }
            }
        }
    }

    void DoDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 4);
        foreach (var item in colliders)
        {
            if (item.tag == "Enemy")
            {
                enemies.Add(item.GetComponent<Enemy>());
            }
        }
    }
	
    IEnumerator Explosion()
    {
        DoDamage();
        transform.GetChild(0).gameObject.SetActive(true);

        isExploded = true;
        yield return new WaitForSeconds(0.5f);
        foreach (var enemy in enemies)
        {
            enemy.TakeDamage(damagePerShot);
        }
        
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
