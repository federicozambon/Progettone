using UnityEngine;
using System.Collections;

public class TitanoEnemyFire : MonoBehaviour
{
    public Transform weapon;
    public GameObject bulletPrefab;
    public bool isShooting;
    public BlackBoard blackRef;

    RaycastHit losRayHit;

    public float speed = 10;

    bool sparo = true;

    TitanoEnemy enemyRef;

    void Awake()
    {
        enemyRef = GetComponent<TitanoEnemy>();
    }

    void Start()
    {
        blackRef = GetComponent<BlackBoard>();
        pool = GameObject.Find("TitanoParticlePool");
        transformTr = new Transform[10];
    }

    public void Update()
    {
        this.transform.LookAt(blackRef.playerTr);
    }

    public IEnumerator Shooting()
    {
        isShooting = true;
        for (int i = 0; i < 10; i++)
        {
            EffectSettings effectRef = pool.GetComponentsInChildren<EffectSettings>(true)[i];
            if (!effectRef.gameObject.activeInHierarchy)
            {
                effectRef.transform.position = weapon.transform.position;
                effectRef.transform.FindChild("Trail").position = weapon.transform.position;
            }
            if (Physics.Linecast(weapon.transform.position, blackRef.playerTr.position, out losRayHit))
            {
                if (losRayHit.collider.gameObject.tag == "Player" && Vector3.Distance(blackRef.playerTr.position, this.transform.position) < 40)
                {
                    enemyRef.animRef.SetBool("Attack",true);
                    ParticleActivator(blackRef.playerTr.FindChild("Head").position);
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
        enemyRef.animRef.SetBool("Attack", false);
        yield return new WaitForSeconds(1);
        isShooting = false;

        if (blackRef.enemyRef.hPoints < 0)
        {
            StopAllCoroutines();
        }
    }

    public GameObject pool;
    public Transform[] transformTr;

    public void ParticleActivator(Vector3 position)
    {
        for (int j = 0; j < 10; j++)
        {
            for (int i = 0; i < 10; i++)
            {
                {
                    EffectSettings effectRef = pool.GetComponentsInChildren<EffectSettings>(true)[j];
                    if (!effectRef.gameObject.activeInHierarchy)
                    {
                        transformTr[i] = effectRef.transform.parent.GetComponentsInChildren<Transform>()[1 + i];
                        transformTr[i].position = position;
                        effectRef.Target = transformTr[i].gameObject;
                        effectRef.gameObject.SetActive(true);
                        break;
                    }
                }
            }
        }
    }
}
