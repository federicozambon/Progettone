using UnityEngine;
using System.Collections;

public class PlayerShotgunBullet : MonoBehaviour
{ 
    LaserShotgun weaponRef;
    bool firstTime = true;
    public bool hit = false;

    private void OnEnable()
    {
        if (firstTime)
        {
            weaponRef = FindObjectOfType<LaserShotgun>();
            this.gameObject.SetActive(false);
            firstTime = false;
        }
        hit = false;
    }

    void Update ()
    {
        if (!hit)
        {
            foreach (var item in Physics.OverlapSphere(this.transform.position, 0.2f))
            {
                if (item.tag == "Enemy")
                {
                    hit = true;
                    item.GetComponent<Enemy>().TakeDamage(weaponRef.damagePerShot);
                }
                if (item.tag == "Destructible")
                {
                    hit = true;
                    item.GetComponent<Destructble>().TakeDamage(weaponRef.damagePerShot);
                }
            }
        }
	}
}
