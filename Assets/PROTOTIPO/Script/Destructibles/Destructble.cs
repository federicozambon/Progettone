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

        if (transform.GetChild(0).gameObject.tag == "Fire")
        {
            transform.GetChild(0).gameObject.GetComponent<FireTrap>().isMiniTrap = true;
        }
            
        else if (transform.GetChild(0).gameObject.tag == "Ice")
        {
            transform.GetChild(0).gameObject.GetComponent<IceTrap>().activeTrap = true;
            transform.GetChild(0).gameObject.GetComponent<IceTrap>().isMiniTrap = true;
        }
            


                yield return new WaitForSeconds(3f);
        //Destroy(this.gameObject);
    }	
}
