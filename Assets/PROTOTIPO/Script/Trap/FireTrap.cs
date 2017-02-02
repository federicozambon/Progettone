using UnityEngine;
using System.Collections;

public class FireTrap: Trap
{
    public Player playerRef;
    DialogueSystem dialoghi;
    public ReferenceManager refManager;

	void Start ()
    {
        refManager = FindObjectOfType<ReferenceManager>();
        particle = this.GetComponent<ParticleSystem>();
        coll = this.GetComponent<SphereCollider>();
        playerRef = refManager.playerObj.GetComponent<Player>();    
        coll.enabled = false;
    }
	
	public override IEnumerator ActivateTrap()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                Enemy nemico = enemy.GetComponent<Enemy>();
                if (nemico.remainHPoints > 0)
                    nemico.remainHPoints -= healthPointDamage;
                else
                {                   
                    nemico.StartCoroutine(nemico.Die());
                }
            }
        }

        if (playerTrapped == true)
        {
            playerRef.TakeDamage(playerPointDamage);
<<<<<<< HEAD
=======

>>>>>>> f103432abe526a9a43fe57b7ca7975535dcaa710
        }
        yield return new WaitForSeconds(timeToRepeat);
        StartCoroutine(ActivateTrap()); 
    }

    public override IEnumerator ParticleTrap()
    {
        particle.Play();
        coll.enabled = true;

        yield return new WaitForSeconds(timeToDisable);

        if (isMiniTrap)
            Destroy(this.gameObject);

        else
            myActivatorsController.GetComponent<ActivatorsController>().enabledAllActivators = true;

        particle.Stop();
        coll.enabled = false;
        resetTrap = true;      
    }

    public void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            enemies.Remove(coll.gameObject);
        }

        if (coll.gameObject.tag == "Player")
        {
            playerTrapped = false;
        }
    }

    void Update()
    {
        if (activeTrap)
        {
            activeTrap = false;
            StartCoroutine(ParticleTrap());
            StartCoroutine(ActivateTrap());
        }

        if (resetTrap)
        {
            resetTrap = false;
            enemies.Clear();
        }
    }
}
