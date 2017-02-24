using UnityEngine;
using System.Collections;

public class SniperEnemyFire : MonoBehaviour
{
    public ReferenceManager refManager;
    public Transform weapon;
    public GameObject bulletPrefab;
    public LineRenderer aimLine;
    public int damage = 25;
    SniperEnemy enemyRef;

    RaycastHit losRayHit;

    public float speed = 10;

    bool isShooting;
    bool sparo = true;

    public Transform playerTr;

    private void Awake()
    {
        enemyRef = GetComponent<SniperEnemy>();
        refManager = GameObject.FindGameObjectWithTag("Reference").GetComponent<ReferenceManager>();
        pool = GameObject.Find("SniperParticlePool");
        id = transform.GetSiblingIndex();
        myParticle = pool.transform.GetChild(id);
        myEffect = myParticle.GetComponentsInChildren<EffectSettings>(true)[0];
        transformTr = myParticle.GetComponentsInChildren<Transform>(true)[1];
    }

    void Start()
    {
        playerTr = FindObjectOfType<Player>().transform.FindChild("Head");
    }

    public IEnumerator Shooting()
    {
        float timer = 0;
        isShooting = true;
        aimLine.SetWidth(0, 0);


        while (Physics.Linecast(weapon.transform.position, playerTr.position, out losRayHit))
        {
            if (losRayHit.collider.gameObject.tag == "Player")
            {
                enemyRef.animRef.SetBool("Attack", true);
                aimLine.SetPosition(0, weapon.position);
                aimLine.SetPosition(1, playerTr.transform.position);
                aimLine.enabled = true;
                timer += Time.deltaTime;
                aimLine.SetWidth(timer / 20, timer / 20);
                yield return null;
                if (timer > 3)
                {
                    aimLine.enabled = false;
                    refManager.playerRef.TakeDamage(damage);
                    ParticleActivator(playerTr.transform.position);                 
                    isShooting = false;
                    enemyRef.animRef.SetBool("Attack", false);
                    break;
                }
            }
            else
            {
                enemyRef.animRef.SetBool("Attack", false);
                aimLine.enabled = false;
                timer = 0;
                isShooting = false;
                break;
            }     
        }   
          
    yield return new WaitForSeconds(4);

    if (GetComponent<SniperEnemy>().hPoints > 0)
    {
        StartCoroutine(Shooting());
    }
    else
    {
        StopAllCoroutines();
    }
}

    public GameObject pool;
    public Transform transformTr;
    Transform myParticle;
    int id;
    EffectSettings myEffect;

    public void ParticleActivator(Vector3 position)
    {
        transformTr.position = position;
        myEffect.transform.position = weapon.transform.position;
        myEffect.Target = transformTr.gameObject;
        myEffect.gameObject.SetActive(true);
    }
}
