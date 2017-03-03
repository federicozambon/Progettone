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
    Coroutine myCo;

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
                nemico.TakeDamage(healthPointDamage);
            }

            if (coll.gameObject.tag == "Player")
            {
                playerRef.TakeDamage(playerPointDamage);
            }
        }
        yield return new WaitForSeconds(timeToRepeat);

        myCo = StartCoroutine(ActivateTrap());
    }

    public override IEnumerator ParticleTrap()
    {
        activeTrap = false;
        particellare1.SetActive(true);

        playSound = false;
        aController.playSound(myDie);

        yield return new WaitForSeconds(timeToDisable);
        StopCoroutine(myCo);
        particellare1.SetActive(false);
        particellare2.SetActive(true);
        resetTrap = true;

        yield return new WaitForSeconds(3);
        particellare2.SetActive(false);
        yield return new WaitForSeconds(3);
        myActivatorsController.GetComponent<ActivatorsController>().enabledAllActivators = true;
        StopAllCoroutines();
    }

    void Update()
    {
        if (activeTrap)
        {
            activeTrap = false;
            StartCoroutine(ParticleTrap());
            myCo = StartCoroutine(ActivateTrap());
        }

        if (resetTrap)
        {
            resetTrap = false;
        }
    }
}
