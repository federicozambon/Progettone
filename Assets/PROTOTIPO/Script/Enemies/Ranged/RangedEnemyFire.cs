using UnityEngine;
using System.Collections;

public class RangedEnemyFire : MonoBehaviour
{

    public Transform weapon;
    public GameObject bulletPrefab;

    RaycastHit losRayHit;

    public float speed = 10;

    bool sparo = true;

    GameObject playerGo;

    void Start()
    {
        playerGo = FindObjectOfType<Player>().gameObject;       
    }

    public void GetPool()
    {
        pool = GetComponent<RangedEnemy>().poolP;
    }

    public void Update()
    {
        this.transform.LookAt(playerGo.transform);
    }

    public IEnumerator Shooting()
    {
        
        if (Physics.Linecast(weapon.transform.position, playerGo.transform.position, out losRayHit))
        {
            if (losRayHit.collider.gameObject.tag == "Player" && Vector3.Distance(playerGo.transform.position, this.transform.position) < 15)
            {
                ParticleActivator(playerGo.transform.FindChild("Head").position);
            }
            else if (!GetComponent<RangedEnemy>().isActive)
            {
                ParticleActivator(playerGo.transform.position);
            }
        }
        yield return new WaitForSeconds(1.5f);

        if (sparo == true && GetComponent<RangedEnemy>().hPoints > 0)
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

    public void ParticleActivator(Vector3 position)
    {
        if (pool)
        {
            if (!pool.GetComponentInChildren<EffectSettings>(true).gameObject.activeInHierarchy)
            {
                transformTr = GetComponentsInChildren<Transform>()[1];
                transformTr.position = position;
                pool.GetComponentInChildren<EffectSettings>(true).transform.position = weapon.transform.position;
                pool.GetComponentInChildren<EffectSettings>(true).Target = transformTr.gameObject;
                pool.GetComponentInChildren<EffectSettings>(true).gameObject.SetActive(true);
            }
        }
    }
}
