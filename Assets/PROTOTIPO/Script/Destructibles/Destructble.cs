using UnityEngine;
using System.Collections;

public class Destructble : MonoBehaviour
{
    bool firstBarrel = true;
    
    public GameObject myTrap;
    public int hPoints;  
    public int remainHPoints;
    public bool knockbacked;
	
	void Start ()
    {
        remainHPoints = hPoints;
	}

    public void TakeDamage(int damagePerShot)
    {
        if (remainHPoints - damagePerShot >= 0)
        {
            remainHPoints -= damagePerShot;
            Debug.Log(damagePerShot);
            knockbacked = true;
            StartCoroutine("KnockbackTimer");             
        }
        else
        {
            if(firstBarrel)
            {
                firstBarrel = false;
                StartCoroutine(Die());

            }
            
        }
    }

    public IEnumerator KnockbackTimer()
    {
        yield return new WaitForSeconds(0.5f);
        knockbacked = false;
        StopCoroutine("KnockbackTimer");
    }

    public IEnumerator Die()
    {
        GameObject newTrap = Instantiate(myTrap);
        newTrap.transform.position = this.transform.position;
        newTrap.GetComponent<Trap>().isMiniTrap = true;
        newTrap.GetComponent<Trap>().activeTrap = true;
        newTrap.GetComponent<Trap>().playerTrapped = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }	
}
