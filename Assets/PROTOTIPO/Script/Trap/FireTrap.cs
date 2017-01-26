using UnityEngine;
using System.Collections;

public class FireTrap: Trap
{
    public Player playerRef;
    DialogueSystem dialoghi;

	void Start ()
    {
        particle = this.GetComponent<ParticleSystem>();
        coll = this.GetComponent<SphereCollider>();
        playerRef = FindObjectOfType<Player>();
        dialoghi = FindObjectOfType<DialogueSystem>();
        
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

            /*if (playerRef.fireDialogue == true)
                
                {
                    playerRef.fireDialogue = false;
                    int[] dialogo = new int[3] { 3, 4, 9 };
                    dialoghi.dialogoMultiplo(dialogo);


                }*/
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
        if (activeTrap == true)
        {
            activeTrap = false;
            StartCoroutine(ParticleTrap());
            StartCoroutine(ActivateTrap());
        }

        if (resetTrap == true)
        {
            resetTrap = false;
            enemies.Clear();
        }

    }


}
