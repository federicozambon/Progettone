using UnityEngine;
using System.Collections;

public class ElectroTrapBehevior : Trap {
    public GameObject particellare1;
    public GameObject particellare2;
    public float timer;
    public bool stoptimer = false;
    public bool start;
    public Player playerRef;
    public ReferenceManager refManager;

    void Start()
    {
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
        activeTrap = false;
        particellare1.SetActive(true);

        playSound = false;
        aController.playSound(myDie);

        yield return new WaitForSeconds(timeToDisable);

        if (isMiniTrap)
            Destroy(this.gameObject);

        particellare1.SetActive(false);
        particellare2.SetActive(true);
        resetTrap = true;

        yield return new WaitForSeconds(3);
        particellare2.SetActive(false);
        myActivatorsController.GetComponent<ActivatorsController>().enabledAllActivators = true;
        StopAllCoroutines();

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
            
            

        }
    }


}
