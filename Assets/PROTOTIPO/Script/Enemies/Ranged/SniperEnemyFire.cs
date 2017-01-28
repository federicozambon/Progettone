using UnityEngine;
using System.Collections;

public class SniperEnemyFire : MonoBehaviour
{
    public Transform weapon;
    public GameObject bulletPrefab;
    public LineRenderer aimLine;

    RaycastHit losRayHit;

    public float speed = 10;

    bool isShooting;
    bool sparo = true;

    public Transform playerTr;

    void Start()
    {
        playerTr = FindObjectOfType<Player>().transform.FindChild("Head");
    }

    public void GetPool()
    {
        pool = GetComponent<SniperEnemy>().poolP;
    }

    public IEnumerator Shooting()
    {
        float timer = 0;
        isShooting = true;
        aimLine.SetWidth(0, 0);


        while (Physics.Linecast(weapon.transform.position, playerTr.position, out losRayHit))
        {
        Debug.Log(losRayHit.collider.gameObject.tag);
            if (losRayHit.collider.gameObject.tag == "Player")
            {
                aimLine.SetPosition(0, weapon.position);
                aimLine.SetPosition(1, playerTr.transform.position);
                aimLine.enabled = true;
                timer += Time.deltaTime;
                aimLine.SetWidth(timer / 20, timer / 20);
                yield return null;
                if (timer > 3)
                {
                    aimLine.enabled = false;
                    ParticleActivator(playerTr.transform.position);
                    isShooting = false;
                    break;
                }
            }
            else
            {
                aimLine.enabled = false;
                timer = 0;
                isShooting = false;
                break;
            }     
        }   
          
    yield return new WaitForSeconds(4);

    if (sparo == true && GetComponent<SniperEnemy>().hPoints > 0)
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
