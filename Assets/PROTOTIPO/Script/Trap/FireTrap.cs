using UnityEngine;
using System.Collections;

public class FireTrap: Trap
{
    
    DialogueSystem dialoghi;
    public Player playerRef;
    public ReferenceManager refManager;


    void Start ()
    {
        
        particle = this.GetComponent<ParticleSystem>();
        
        refManager = GameObject.FindGameObjectWithTag("Reference").GetComponent<ReferenceManager>();
        playerRef = refManager.playerObj.GetComponent<Player>();
    }
	
	public override IEnumerator ActivateTrap()
    {
        
            foreach (var coll in colliders)
        {
            if (coll.gameObject.tag == "Enemy")
            {
                Enemy nemico = coll.GetComponent<Enemy>();
                if (nemico.remainHPoints > 0)
                    nemico.remainHPoints -= healthPointDamage;
                else
                {                   
                    nemico.StartCoroutine(nemico.Die());
                }
            }

            if (coll.gameObject.tag == "Player")
            {
                playerRef.TakeDamage(playerPointDamage);
            }

        }

        
        yield return new WaitForSeconds(timeToRepeat);

            StartCoroutine(ActivateTrap()); 
    }

    public override IEnumerator ParticleTrap()
    {
        particle.Play();
       

        yield return new WaitForSeconds(timeToDisable);

        myActivatorsController.GetComponent<ActivatorsController>().enabledAllActivators = true;

        particle.Stop();
        resetTrap = true;      
    }

    public override IEnumerator MiniTrap()
    {
        foreach (var coll in colliders)
        {
            if (coll.gameObject.tag == "Enemy")
            {
                Enemy nemico = coll.GetComponent<Enemy>();
                if (nemico.remainHPoints > 0)
                    nemico.remainHPoints -= healthPointDamage;
                else
                {
                    nemico.StartCoroutine(nemico.Die());
                }
            }

            if (coll.gameObject.tag == "Player")
            {
                playerRef.TakeDamage(playerPointDamage);
            }

        }


        yield return new WaitForSeconds(timeToDisable);

        Destroy(this.transform.parent.gameObject);
    }

    void Update()
    {
        if (activeTrap)
        {
            activeTrap = false;
            StartCoroutine(ParticleTrap());
            StartCoroutine(ActivateTrap());
        }

        if (isMiniTrap)
        {
            particle.Play();
            playSound = false;
            aController.playSound(myDie);
            this.transform.parent.GetComponent<MeshRenderer>().enabled = false;
            isMiniTrap = false;
            StartCoroutine(MiniTrap());

        }

        if (resetTrap)
        {
            resetTrap = false;
            StopAllCoroutines();
            
        }
    }
}
