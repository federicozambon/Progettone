using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour
{
    public ReferenceManager refManager;
    public NavMeshAgent navRef;
    public Gradient gradient;

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
    public bool tutorialMode = false;

    public bool isActiveAttractionTrap;
    public bool isActiveElectricTrap;
    public bool isActiveIceTrap;
    public Transform attractionTrap;
    public FX fxRef;
    public BlackBoard blackRef;

    AudioController aController;
    public AudioClip myDie;
    bool playSound = true;
    public Animator animRef;

    public virtual void Attack()
    {

    }

    public float startDamage;
    public float startScore;
    public float startHPoints;

    public virtual void Awake()
    {
        animRef = GetComponentInChildren<Animator>();
        refManager = GameObject.FindGameObjectWithTag("Reference").GetComponent<ReferenceManager>();
        blackRef = GetComponent<BlackBoard>();
        navRef = GetComponent<NavMeshAgent>();
        this.gameObject.SetActive(false);

        startDamage = damage;
        startHPoints = hPoints;
        startScore = scoreValue;
        
        aController = FindObjectOfType<AudioController>();   
    }

    bool firstTime = true;

    void OnEnable()
    {
        refManager.miniMapRef.NewEnemy(this.gameObject);
        if (!this.navRef.isOnNavMesh)
        {
            Debug.LogError(this.name);
        }
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
        float coef = Mathf.Pow(1.1f, refManager.waveRef.currentWaveNumber + 1);
        damage = (startDamage * coef)/2;
        scoreValue = (startScore * coef)/2;
        hPoints = (startHPoints * coef)/2;
        remainHPoints = hPoints;
    }


    virtual public void ResetCombatText()
    {
        blackRef.textMesh.transform.localPosition = new Vector3(0, 3, 0);
        blackRef.textMesh.text = "";
        blackRef.textMesh.characterSize = 0.46f;
    }

    virtual public IEnumerator CombatText(int damage, bool isCrit)
    {
        blackRef.textMesh.characterSize = 0.46f;
        float timer = 0;
        blackRef.textMesh.text = damage.ToString();
        blackRef.textMesh.color = gradient.Evaluate(1-remainHPoints/hPoints);
        if (isCrit)
        {
            blackRef.textMesh.characterSize = 0.8f;
        }
        while (timer <0.5f)
        {
            timer += Time.deltaTime;
            blackRef.textMesh.transform.localPosition = Vector3.Lerp(new Vector3(0,2.5f,0),new Vector3(0,4,0), timer*2);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        blackRef.textMesh.transform.localPosition = new Vector3(0, 3, 0);
        timer = 0; 
        blackRef.textMesh.characterSize = 0.46f;
        blackRef.textMesh.text = "";
        combat = null;
    }
    Coroutine combat;

    virtual public void TakeDamage(int damagePerShot)
    {
        bool isCrit = false;
        int percent = damagePerShot * 15 / 100;
        damagePerShot += Random.Range(-percent, percent);

        if (Random.value > 0.9f)
        {
            isCrit = true;  
            damagePerShot *= 2;
        }

        if (!dead)
        {
            remainHPoints -= damagePerShot;
            if (combat == null)
            {
                 combat = StartCoroutine(CombatText(damagePerShot, isCrit));       
            }
            else
            {
                StopCoroutine(combat);
                combat = StartCoroutine(CombatText(damagePerShot,isCrit));
            }
  
       
            if (remainHPoints <= 0)
            {
                StartCoroutine("Die");
                if (playSound == true && myDie != null)
                {
                    playSound = false;
                    aController.playSound(myDie);
                    //Debug.Log("sono morto");
                }
            }
        }
    }

    virtual public void ParticleActivator(Vector3 position)
    {
        for (int i = 0; i < 149; i++)
        {
            EffectSettings effectRef = refManager.pool.GetComponentsInChildren<EffectSettings>(true)[i];
            if (!effectRef.gameObject.activeInHierarchy)
            {
                effectRef.transform.position = this.transform.position;
                effectRef.gameObject.SetActive(true);
                break;
            }
        }
    }

    virtual public void SpawnParticleActivator(Vector3 position)
    {
        for (int i = 0; i < 30; i++)
        {
            EffectSettings effectRef = refManager.spawnPool.GetComponentsInChildren<EffectSettings>(true)[i];
            if (!effectRef.gameObject.activeInHierarchy)
            {
                effectRef.transform.position = position;
                effectRef.gameObject.SetActive(true);
                break;
            }
        }
    }

    virtual public IEnumerator Die()
    {
        ResetCombatText();
        if (dieController == true && tutorialMode == false)
        {
            ParticleActivator(this.transform.position);
            refManager.miniMapRef.DeleteEnemy(this.gameObject);
            refManager.waveRef.IsWaveFinished();
            dieController = false;
            refManager.uicontroller.IncreaseScore((int)scoreValue);
            refManager.spawnRef.StoreEnemy(this.gameObject);
            playSound = true;
        }
        else
            Destroy(this.gameObject);
        yield return null; 
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
