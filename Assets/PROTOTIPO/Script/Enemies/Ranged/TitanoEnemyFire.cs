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

        for (int i = 0; i < 10; i++)
        {
            transformTr[i] = myParticle.transform.GetChild(i);
            myEffect[i] = myParticle.transform.GetChild(i + 10).GetComponent<EffectSettings>();
            myDamage[i] = myEffect[i].transform.FindChild("Trail").GetComponent<bugtitanobullet>();
        }
    }

    void Start()
    {
        playerTr = FindObjectOfType<Player>().transform.FindChild("Head");
    }

    public void Update()
    {
        this.transform.LookAt(blackRef.playerTr);
    }

    public IEnumerator Shooting()
    {
        isShooting = true;
        if (Physics.Linecast(weapon.transform.position, refManager.playerObj.transform.position, out losRayHit))
        {
            enemyRef.animRef.SetBool("PreAttack", true);
            yield return new WaitForSeconds(0.35f);
            enemyRef.animRef.SetBool("PreAttack", false);
            enemyRef.animRef.SetBool("Attack", true);
            for (int i = 0; i < 10; i++)
            {            
                if (losRayHit.collider.gameObject.tag == "Player" && Vector3.Distance(refManager.playerObj.transform.position, this.transform.position) < 15)
                {
                    ParticleActivator(refManager.playerObj.transform.FindChild("Head").position,i);
                }
                yield return new WaitForSeconds(0.35f);
            }
            isShooting = false;
            enemyRef.animRef.SetBool("Attack", false);

            if (enemyRef.hPoints < 0)
            {
                StopAllCoroutines();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public GameObject pool;
    public Transform[] transformTr;
    Transform myParticle;
    int id;
    EffectSettings[] myEffect;
    bugtitanobullet[] myDamage;

    public void ParticleActivator(Vector3 position, int n)
    {
        transformTr[n].position = position;
        myDamage[n].damage = (int)enemyRef.damage;
        myEffect[n].transform.position = weapon.transform.position;
        myEffect[n].Target = transformTr[n].gameObject;
        myEffect[n].gameObject.SetActive(true);
    }
}
