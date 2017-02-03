using UnityEngine;
using System.Collections;

public class RangedEnemyFire : MonoBehaviour
{
    public Transform weapon;
    public GameObject bulletPrefab;
    public bool isShooting;
    ReferenceManager refManager;

    RaycastHit losRayHit;

    public float speed = 10;

    bool sparo = true;

    GameObject playerGo;

    void Start()
    {
        refManager = GameObject.FindGameObjectWithTag("Reference").GetComponent<ReferenceManager>();
    }

    public void Awake()
    {
        pool = GameObject.Find("FanteParticlePool");
    }

    public void Update()
    {
        this.transform.LookAt(refManager.playerObj.transform);
    }

    public IEnumerator Shooting()
    {
        isShooting = true;
        if (Physics.Linecast(weapon.transform.position, playerGo.transform.position, out losRayHit))
        {
            if (losRayHit.collider.gameObject.tag == "Player" && Vector3.Distance(playerGo.transform.position, this.transform.position) < 15)
            {
                ParticleActivator(playerGo.transform.FindChild("Head").position);
            }
        }
        yield return new WaitForSeconds(1.5f);
        isShooting = false;

        if (GetComponent<RangedEnemy>().hPoints < 0)
        {
            StopAllCoroutines();
        }
    }

    public GameObject pool;
    public Transform transformTr;

    public void ParticleActivator(Vector3 position)
    {
        for (int i = 0; i < 50; i++)
        {
            EffectSettings effectRef = pool.GetComponentsInChildren<EffectSettings>(true)[i];
            if (!effectRef.gameObject.activeInHierarchy)
            {
                transformTr = effectRef.transform.parent.GetComponentsInChildren<Transform>(true)[1];
                transformTr.position = position;
                effectRef.transform.position = weapon.transform.position;
                effectRef.Target = transformTr.gameObject;
                effectRef.gameObject.SetActive(true);
            }
        }
    }
}
