using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HolePanel : MonoBehaviour {

    
    public Transform targetA;
    public  Transform targetB;
    public Transform targetTr;
    public Transform panel;
    public bool active = false;
    public bool target = true;
    //public HoleTrap hTrap; 

    void Start ()
    {
        targetTr = targetA;
	
	}
	
    IEnumerator returnToPosition()
    {
        yield return new WaitForSeconds(5);

        //hTrap.GetComponent<BoxCollider>().enabled = false;

        active = true;
    }
	

	void Update ()
    {
        if (active == true)
        {
            //hTrap.GetComponent<BoxCollider>().enabled = true;
            //hTrap.activeTrap = true;

            Vector3 distance = targetTr.position - panel.position;
            Vector3 direction = distance.normalized;

            panel.position = panel.position + direction * 25 * Time.deltaTime;

            if (distance.magnitude < 2f && target == true)
            {
                active = false;
                target = false;
                panel.position = targetTr.position;
                targetTr = targetB;
                
                
                StartCoroutine(returnToPosition());
            }

            else if (distance.magnitude < 2f && target == false)
            {
                active = false;
                target = true;
                panel.position = targetTr.position;
                targetTr = targetA;
         
            }

        }

       

    }
}
