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

        transformTr = new Transform[5];
        myEffect = new EffectSettings[5];
        myDamage = new bugtitanobullet[5];
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

            for (int i = 0; i < 5; i++)
            {
                if (losRayHit.collider.gameObject.tag == "Player" && Vector3.Distance(refManager.playerObj.transform.position, this.transform.position) < 30)
                {
                    ParticleActivator(refManager.playerObj.transform.FindChild("Head").position + this.transform.forward * 5, i);
                }
                yield return new WaitForSeconds(0.3f);
            }
        
        }
        enemyRef.animRef.SetBool("Attack", false);

        yield return new WaitForSeconds(1);
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

    public void ParticleActivator(Vector3 position, int f)
    {
        myDamage[f].damage = (int)enemyRef.damage;
        transformTr[f].position = position;
        myDamage[f].transform.localPosition = new Vector3(0, 0, 0);
        myEffect[f].transform.position = weapon.transform.position;    
        myEffect[f].Target = transformTr[f].gameObject;
        myEffect[f].gameObject.SetActive(true);
    }
}
