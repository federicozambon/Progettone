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
        StartCoroutine(Shooting());        
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
                ParticleActivator(playerGo.transform.position);
                //GameObject newBullet = Instantiate(bulletPrefab, this.transform.position + transform.forward, Quaternion.identity) as GameObject;
                //newBullet.GetComponentInChildren<EffectSettings>().Target = playerGo;
            }
            else if (!GetComponent<RangedEnemy>().isActive)
            {
                ParticleActivator(playerGo.transform.position);
                //GameObject newBullet = Instantiate(bulletPrefab, this.transform.position+transform.forward, Quaternion.identity) as GameObject;
                //newBullet.GetComponentInChildren<EffectSettings>().Target = playerGo;
            }
        }
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));

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
                pool.GetComponentInChildren<EffectSettings>(true).transform.position = this.transform.position;
                pool.GetComponentInChildren<EffectSettings>(true).Target = transformTr.gameObject;
                pool.GetComponentInChildren<EffectSettings>(true).gameObject.SetActive(true);
            }
        }
    }
}
