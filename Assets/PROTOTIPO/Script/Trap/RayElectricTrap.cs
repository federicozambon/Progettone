using UnityEngine;
using System.Collections;

public class RayElectricTrap: Trap
{

    public ElectricTrap eTrap;
    public MeshRenderer mr;
    

	void Start ()
    {
       
        coll = this.GetComponent<CapsuleCollider>();
        player = FindObjectOfType<Player>();
        //eTrap = FindObjectOfType<ElectricTrap>();
        mr = GetComponent<MeshRenderer>();
        

        coll.enabled = false;
        mr.enabled = false;
    }


    public override void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            eTrap.enemies.Add(collider.gameObject);
            
        }

        if (collider.gameObject.tag == "Player")
        {
            eTrap.playerTrapped = true;
        }
    }


    public void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            eTrap.enemies.Remove(collider.gameObject);
        }

        if (collider.gameObject.tag == "Player")
        {
            eTrap.playerTrapped = false;
        }
    }

    


}
