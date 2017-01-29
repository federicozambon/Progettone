using UnityEngine;
using System.Collections;

public class TitanoEnemyFire : MonoBehaviour
{
    public Transform weapon;
    public GameObject bulletPrefab;
    public bool isShooting;

    RaycastHit losRayHit;

    public float speed = 10;

    bool sparo = true;

    GameObject playerGo;

    void Start()
    {
        transformTr = new Transform[10];
        playerGo = FindObjectOfType<Player>().gameObject;
        for (int i = 0; i < 10; i++)
        {
            transformTr[i] = pool.GetComponentsInChildren<Transform>(true)[i+1];
        }
    }

    public void GetPool()
    {
        pool = GetComponent<TitanoEnemy>().poolP;
    }

    public void Update()
    {
        this.transform.LookAt(playerGo.transform);
    }

    public IEnumerator Shooting()
    {
        isShooting = true;
        for (int i = 0; i < 10; i++)
        {
            if (!pool.GetComponentsInChildren<EffectSettings>(true)[i].gameObject.activeInHierarchy)
            {
                pool.GetComponentsInChildren<EffectSettings>(true)[i].transform.position = weapon.transform.position;
                pool.GetComponentsInChildren<EffectSettings>(true)[i].transform.FindChild("Trail").position = weapon.transform.position;
            }
            if (Physics.Linecast(weapon.transform.position, playerGo.transform.position, out losRayHit))
            {
                if (losRayHit.collider.gameObject.tag == "Player" && Vector3.Distance(playerGo.transform.position, this.transform.position) < 40)
                {
                    ParticleActivator(playerGo.transform.FindChild("Head").position);
                }
                else if (!GetComponent<TitanoEnemy>().isActive)
                {
                    ParticleActivator(playerGo.transform.position);
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(1);
        isShooting = false;

        if (GetComponent<TitanoEnemy>().hPoints < 0)
        {
            StopAllCoroutines();
        }
    }

    public GameObject pool;
    public Transform[] transformTr;

    public void ParticleActivator(Vector3 position)
    {

        for (int i = 0; i < 10; i++)
        {
            if (pool)
            {
                if (!pool.GetComponentsInChildren<EffectSettings>(true)[i].gameObject.activeInHierarchy)
                {               
                    transformTr[i].position = position;            
                    pool.GetComponentsInChildren<EffectSettings>(true)[i].Target = transformTr[i].gameObject;
                    pool.GetComponentsInChildren<EffectSettings>(true)[i].gameObject.SetActive(true);
                    break;
                }
            }
        }  
    }
}
