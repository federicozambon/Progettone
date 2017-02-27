using UnityEngine;
using System.Collections;

public class fix : MonoBehaviour
{
    AssaultRifle weaponRef;
    bool firstTime = true;

    private void OnEnable()
    {
        if (firstTime)
        {
            weaponRef = FindObjectOfType<AssaultRifle>();
            this.gameObject.SetActive(false);
            firstTime = false;
        }
    }

    void Update()
    {
        foreach (var item in Physics.OverlapSphere(this.transform.position, 0.3f))
        {
            if (item.tag == "Enemy")
            {
                item.GetComponent<Enemy>().TakeDamage(weaponRef.damagePerShot);
            }
            if (item.tag == "Destructible")
            {
                item.GetComponent<Destructble>().TakeDamage(weaponRef.damagePerShot);
            }
        }
    }
}
