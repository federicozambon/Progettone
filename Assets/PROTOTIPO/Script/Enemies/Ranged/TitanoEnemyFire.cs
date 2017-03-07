using UnityEngine;
using System.Collections;

public class TitanoEnemyFire : MonoBehaviour
{
    public Transform weapon;
    public GameObject bulletPrefab;
    public bool isShooting;
    public BlackBoard blackRef;
    ReferenceManager refManager;

    RaycastHit losRayHit;

    public float speed = 10;

    bool sparo = true;

    TitanoEnemy enemyRef;
    public Transform playerTr;

    void Awake()
    {
        refManager = GameObject.FindGameObjectWithTag("Reference").GetComponent<ReferenceManager>();
        blackRef = GetComponent<BlackBoard>();

        transformTr = new Transform[10];
        myEffect = new EffectSettings[10];
        myDamage = new bugtitanobullet[10];
        enemyRef = GetComponent<TitanoEnemy>();
        pool = GameObject.Find("TitanoParticlePool");
        id = transform.GetSiblingIndex();
        myParticle = pool.transform.GetChild(id);

        for (int i = 0; i < 5; i++)
        {
            transformTr[i] = myParticle.transform.GetChild(i);
            myEffect[i] = myParticle.transform.GetChild(i + 5).GetComponent<EffectSettings>();
            myDamage[i] = myEffect[i].transform.FindChild("MovedTrail").GetComponent<bugtitanobullet>();
        }
    }

    void OnEnable()
    {
        playerTr = FindObjectOfType<Player>().transform.FindChild("Head"); 
        enemyRef.attacking = null;
    }

    public void Update()
    {
        this.transform.LookAt(blackRef.playerTr);
    }

    public IEnumerator Shooting()
    {
        Debug.Log("ciao");
        isShooting = true;
        if (Physics.Linecast(weapon.transform.position, refManager.playerObj.transform.position, out losRayHit))
        {
            enemyRef.animRef.SetBool("PreAttack", true);
            enemyRef.animRef.SetBool("PreAttack", false);
            enemyRef.animRef.SetBool("Attack", true);
       
            if (losRayHit.collider.gameObject.tag == "Player" && Vector3.Distance(refManager.playerObj.transform.position, this.transform.position) < 30)
            {
                ParticleActivator(refManager.playerObj.transform.FindChild("Head").position);                   
            }
        }
        enemyRef.animRef.SetBool("Attack", false);      
        yield return new WaitForSeconds(0.3f);
        isShooting = false;
        enemyRef.attacking = null;
        if (GetComponent<TitanoEnemy>().hPoints < 0)
        {
            StopAllCoroutines();
        }
    }
   
    public GameObject pool;
    public Transform[] transformTr;
    Transform myParticle;
    int id;
    EffectSettings[] myEffect;
    bugtitanobullet[] myDamage;

    int counter = 0; 

    public void ParticleActivator(Vector3 position)
    {
        if (counter<5)
        {
            counter++;
        }
        else
        {
            counter = 0;
        }

        myDamage[counter].damage = (int)enemyRef.damage;
        transformTr[counter].position = position;
        myEffect[counter].transform.position = weapon.transform.position;    
        myEffect[counter].Target = transformTr[counter].gameObject;
        myEffect[counter].gameObject.SetActive(true);
    }
}
