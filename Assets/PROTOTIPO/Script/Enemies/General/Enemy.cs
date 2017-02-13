using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour
{
    public ReferenceManager refManager;
    public NavMeshAgent navRef;

    public string enemyType;
    public Transform headRef;
    public GameObject medikit;

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
    public BlackBoard blackRef;

    AudioController aController;
    public AudioClip myDie;
    bool playSound = true;
    
    public virtual void Attack()
    {

    }

    public virtual void Awake()
    {
        refManager = GameObject.FindGameObjectWithTag("Reference").GetComponent<ReferenceManager>();
        blackRef = GetComponent<BlackBoard>();
        navRef = GetComponent<NavMeshAgent>();
        this.gameObject.SetActive(false);
        pool = GameObject.Find("ParticleEnemyExplosion");
        aController = FindObjectOfType<AudioController>();
        
    }

    bool firstTime = true;

    void OnEnable()
    {

        refManager.miniMapRef.NewEnemy(this.gameObject);
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
        if (refManager.waveRef.currentWaveNumber > 10)
        {
            damage *= 1.5f * Mathf.Floor(refManager.waveRef.currentWaveNumber/10);
            scoreValue *= 1.5f * Mathf.FloorToInt(refManager.waveRef.currentWaveNumber/10);
            hPoints *= 1.5f * Mathf.FloorToInt(refManager.waveRef.currentWaveNumber/10);
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
            }
            else
            {
                StartCoroutine("Die");
                if (playSound == true && myDie != null)
                {
                    playSound = false;
                    aController.playSound(myDie);
                    Debug.Log("sono morto");
                }
                    

            }
        }
    }

    public GameObject pool;

    virtual public void ParticleActivator(Vector3 position)
    {
        for (int i = 0; i < 149; i++)
        {
            EffectSettings effectRef = pool.GetComponentsInChildren<EffectSettings>(true)[i];
            if (!effectRef.gameObject.activeInHierarchy)
            {
                effectRef.transform.position = this.transform.position;
                effectRef.gameObject.SetActive(true);
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
            refManager.miniMapRef.DeleteEnemy(this.gameObject);
            refManager.waveRef.IsWaveFinished();
            dieController = false;
            refManager.uicontroller.IncreaseScore((int)scoreValue);
            refManager.spawnRef.StoreEnemy(this.gameObject);
            playSound = true;

        }

        yield return new WaitForSeconds(5);

        
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
