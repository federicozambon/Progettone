using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour
{
    public WaveController waveRef;
    public UIController uicontroller;
    public GameObject playerObj;
    public GameObject medikit;
    public string enemyType;

    public float remainHPoints;
    public float hPoints;
    public float damage;
    public float scoreValue;

    public int spawnObject = 1;
    public bool spawnable = true;
    bool spawnTrue = false;

    public bool grenade = true;
    public bool repulsion = false;
    public bool trapped = false;
    public bool isActive = false;

    public bool dieController = true;
    public bool dead;
    public int comboValue;
    public bool isCharging;
    public bool knockbacked;

    public Transform headRef;
    public NavMeshAgent navRef;
    public FlyCamManager flyCamRef;

    /*
    public MeshRenderer toOutline;
    public Material occlusionMaterial;
    public Material defaultMaterial;
    */
    /*
    public Ray occlusionRay;
    public RaycastHit[] occlusionHit;
    public Ray antiOcclusionRay;
    public RaycastHit[] antiOcclusionHit;
    public List<GameObject> occludedGoList = new List<GameObject>();
    public Rigidbody enemyRb;
    */

    public bool isActiveAttractionTrap;
    public bool isActiveElectricTrap;
    public bool isActiveIceTrap;
    public Transform attractionTrap;
    public FX fxRef;

    public virtual void Attack()
    {

    }

    void Awake()
    {
        pool = GameObject.Find("ParticleEnemyExplosion");
        //defaultMaterial = this.GetComponentInChildren<MeshRenderer>().material;
        flyCamRef = FindObjectOfType<FlyCamManager>();
        waveRef = FindObjectOfType<WaveController>();
        uicontroller = FindObjectOfType<UIController>();
        playerObj = FindObjectOfType<Player>().gameObject;
        navRef = GetComponent<NavMeshAgent>();
        //enemyRb = GetComponent<Rigidbody>();
        uicontroller.GetComponent<MiniMap>().NewEnemy(this.gameObject);
    }

    public IEnumerator TrapController(float durataDanno)
    {
        if (trapped == true)
        {
        }
        yield return new WaitForSeconds(durataDanno);
        trapped = false;
    }

    void Start()
    {
        if (waveRef.currentWaveNumber > 10)
        {
            damage *= 1.5f * Mathf.Floor(waveRef.currentWaveNumber/10);
            scoreValue *= 1.5f * Mathf.FloorToInt(waveRef.currentWaveNumber/10);
            hPoints *= 1.5f * Mathf.FloorToInt(waveRef.currentWaveNumber/10);
        }
        remainHPoints = hPoints;
    }

    virtual public void TakeDamage(int damagePerShot)
    {
        if (!dead)
        {
            if (remainHPoints - damagePerShot >= 0)
            {
                remainHPoints -= damagePerShot;
                Debug.Log(damagePerShot);
                knockbacked = true;
            }
            else
            {
                StartCoroutine("Die");
            }
        }
    }

    public GameObject pool;

    virtual public void ParticleActivator(Vector3 position)
    {
        for (int i = 0; i < 18; i++)
        {
            if (!pool.GetComponentsInChildren<EffectSettings>(true)[i].gameObject.activeInHierarchy)
            {
                pool.GetComponentsInChildren<EffectSettings>(true)[i].transform.position = this.transform.position;
                pool.GetComponentsInChildren<EffectSettings>(true)[i].gameObject.SetActive(true);
                break;
            }
        }
    }

    virtual public IEnumerator Die()
    {
        yield return null;
        if (dieController == true)
        {
            ParticleActivator(this.transform.position);
            uicontroller.GetComponent<MiniMap>().DeleteEnemy(this.gameObject);
            waveRef.IsWaveFinished();
            dieController = false;
            uicontroller.IncreaseScore((int)scoreValue);
        }      
        Destroy(this.gameObject);
    }

    public void SpawnMedikit()
    {        
        for (int i = 0; i < spawnObject; i++)
        {
            int nRandom;
            nRandom = Random.Range(1, 10);
            Debug.Log(nRandom);
            if (spawnObject == nRandom)
            {             
                if (spawnTrue == false)
                {
                    spawnTrue = true;
                    GameObject nuovoMedikit = Instantiate(medikit.gameObject);
                    nuovoMedikit.transform.position = this.transform.position + new Vector3(0,1.2f,0);
                }
            }
        }   
    }
    /*
    virtual public void Occlusion()
    {
        if (!FindObjectOfType<FlyCamManager>().cutScene)
        {
            occlusionRay = new Ray(this.transform.position, Camera.main.transform.position - this.transform.position);

            occlusionHit = Physics.RaycastAll(occlusionRay);
            foreach (var mesh in occlusionHit)
            {
                //Debug.Log(mesh.collider.gameObject);
                if (mesh.collider.gameObject.tag != "MainCamera")
                {
                    if (mesh.collider.gameObject.tag != "Enemy")
                    {
                        if (occludedGoList.Contains(mesh.collider.gameObject))
                        {

                        }
                        else
                        {
                            occludedGoList.Add(mesh.collider.gameObject);
                            StartCoroutine(LerpAlpha(mesh.collider.gameObject, 1));

                            StartCoroutine(StillOccluding(mesh.collider.gameObject));
                        }
                    }
                }
            }
        }
    }

    public IEnumerator StillOccluding(GameObject go)
    {
        bool found = false;
        yield return new WaitForSeconds(0.5f);

        antiOcclusionRay = new Ray(this.transform.position, Camera.main.transform.position - this.transform.position);
        antiOcclusionHit = Physics.RaycastAll(antiOcclusionRay);

        foreach (var mesh in antiOcclusionHit)
        {
            if (mesh.collider.gameObject != this)
            {
                if (mesh.collider.gameObject == go)
                {
                    found = true;
                    StartCoroutine(StillOccluding(go));
                }
            }
        }
        if (!found)
        {
            StartCoroutine(LerpAlpha(go, -1));
        }
    }

    public IEnumerator LerpAlpha(GameObject go, int sign)
    {
        if (sign > 0)
        {
            this.GetComponentInChildren<MeshRenderer>().material = occlusionMaterial;
            toOutline.sortingOrder = 0;
            yield return null;
        }
        else
        {
            this.GetComponentInChildren<MeshRenderer>().material = defaultMaterial;
            toOutline.sortingOrder = 1;
            yield return null;
        }
        occludedGoList.Remove(go);
    }
    */
}
