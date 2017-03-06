using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class Player : MonoBehaviour
{
    GameObject ammoBox;
    GameObject lifeBox;
    GameObject armorBox;
    GameObject weaponBox;

    bool insideAmmo;
    bool insideLife;
    bool insideArmor;
    bool insideWeapon;

    Transform ammoBoxTr;
    Transform lifeBoxTr;
    Transform armorBoxTr;
    Transform weaponBoxTr;

    public bool godMode = false;
    public bool isAlive = true;
    public bool tutorialMode = false;
    public bool dashTutorial = false;
    public bool tutorial = false;
    public bool stepTutorial = false;
    public bool noWeapons = false;
    public ParticleSystem trailPs;
    ParticleSystemRenderer psRenderer;

    public ReferenceManager refManager;
    Rigidbody playerRigidbody;
    private NavMeshAgent _navAgent;
    Tutorial tutorialElements;
    Animator anim;

    public Material occlusionMaterial;

    Vector3 movement;
    Vector3 shootDirection;

    bool primoSangue = true;
    public bool rotating;

    Ray dashRay;
    RaycastHit dashRayHit;

    float jumpHeight = 10;
    public float speed = 10f;

    bool saltoAttivo = true;
    public bool dashAttivo = true;
    public bool fireDialogue = true;

    private bool _traversingLink;

    public int maxHealth = 100;
    public int currentHealth = 100;
    public int rocketAmmo = 10;
    public float damageModifier = 1;

    public float[] costModifier = new float[4] { 1, 1, 1, 1 };
    public int[] baseCost = new int[4];

    public Ray occlusionRay;
    public RaycastHit[] occlusionHit;
    public Ray antiOcclusionRay;
    public RaycastHit[] antiOcclusionHit;
    public List<OccludedObject> occludedGoList = new List<OccludedObject>();
    GameObject[] allEnemies;
    Achievement achievement;
    UIController uiElements;
    AudioSource aSource;
    public AudioController aController;
    public Material dashMaterial;
    public Gradient gradient;

    public float rx;
    public float ry;

    AssaultRifle assaultRef;
    LaserShotgun shotgunRef;

    string currentScene;
    

    void Awake()
    {
        Time.timeScale = 1;
        textMesh = transform.FindChild("Text").GetComponent<TextMesh>();
        assaultRef = FindObjectOfType<AssaultRifle>();
        shotgunRef = FindObjectOfType<LaserShotgun>();
        refManager = GameObject.FindGameObjectWithTag("Reference").GetComponent<ReferenceManager>();
        anim = GetComponentInChildren<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        tutorialElements = FindObjectOfType<Tutorial>();
        achievement = FindObjectOfType<Achievement>();
        uiElements = FindObjectOfType<UIController>();
        aSource = GetComponent<AudioSource>();
        aController = FindObjectOfType<AudioController>();

        psRenderer = trailPs.GetComponent<ParticleSystemRenderer>();
        dashMaterial = psRenderer.sharedMaterial;

        weaponBox = GameObject.FindGameObjectWithTag("Weapon_PickUp");
        armorBox = GameObject.FindGameObjectWithTag("Armor_PickUp");
        lifeBox = GameObject.FindGameObjectWithTag("Health_PickUp");
        ammoBox = GameObject.FindGameObjectWithTag("Ammo_PickUp");

        weaponBoxTr = weaponBox.transform.FindChild("Pivot");
        armorBoxTr = armorBox.transform.FindChild("Pivot");
        lifeBoxTr = lifeBox.transform.FindChild("Pivot");
        ammoBoxTr = ammoBox.transform.FindChild("Pivot");
        deniedBox = lifeBox.GetComponent<AudioSource>();

        currentScene = SceneManager.GetActiveScene().name;
    }

    public void DestroyAllEnemies()
    {
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in allEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(200);
        }

        Debug.LogWarning("ci sono");
    }

    int damageUp = 0;
    int armorUp = 0;

    public AudioSource deniedBox;


    void UseBox(GameObject nearBox)
    {
        if (nearBox.tag == "Health_PickUp")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (refManager.uicontroller.score >= baseCost[0] * Mathf.Pow(1.2f, costModifier[0])/2 && currentHealth != maxHealth)
                {
                    aController.playSound(AudioContainer.Self.Health_PickUp);
                    refManager.uicontroller.score -= (int)(baseCost[0] * Mathf.Pow(1.2f, costModifier[0]) / 2);
                    refManager.uicontroller.spentScore += (int)(baseCost[0] * Mathf.Pow(1.2f, costModifier[0]) / 2);
                    costModifier[0] += 1;
                    nearBox.transform.GetChild(1).GetComponent<TextMesh>().text = baseCost[0] * costModifier[0] + " SP";
                    refManager.uicontroller.UpdateScore();
                    currentHealth = maxHealth;
                    refManager.uicontroller.IncreaseLife();
     

                    if (currentScene == "Tutorial" && tutorialElements.step == 51)
                    {
                        tutorialElements.HideStep();
                        tutorialElements.NextStep();
                    }
                }
                else
                {
                    deniedBox.Play();
                    nearBox.transform.GetChild(1).GetComponent<TextMesh>().color = Color.red;
                }
            }
        }

        if (nearBox.tag == "Ammo_PickUp")
        {
            if (Input.GetButtonDown("Fire1"))
            {

                if (refManager.uicontroller.score >= baseCost[1] * Mathf.Pow(1.2f, costModifier[1]) / 2 && rocketAmmo != 10)
                {
                    aController.playSound(AudioContainer.Self.Ammo_PickUp);
                    refManager.uicontroller.score -= (int)(baseCost[1] * Mathf.Pow(1.2f, costModifier[1]) / 2);
                    refManager.uicontroller.spentScore += (int)(baseCost[1] * Mathf.Pow(1.2f, costModifier[1]) / 2);
                    costModifier[1] += 0.5f;
                    nearBox.transform.GetChild(1).GetComponent<TextMesh>().text = baseCost[1] * costModifier[1] + " SP";
                    refManager.uicontroller.UpdateScore();
                    rocketAmmo += 5;
                    if (rocketAmmo > 10)
                    {
                        rocketAmmo = 10;
                    }
                    refManager.uicontroller.ammo.text = rocketAmmo.ToString();

                    if (currentScene == "Tutorial" && tutorialElements.step == 52)
                    {
                        tutorialElements.HideStep();
                        tutorialElements.NextStep();
                    }
                }
                else
                {
                    deniedBox.Play();
                    nearBox.transform.GetChild(1).GetComponent<TextMesh>().color = Color.red;
                }
            }
        }

        if (nearBox.tag == "Weapon_PickUp")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (refManager.uicontroller.score >= baseCost[2] * Mathf.Pow(1.2f, costModifier[2]) / 2)
                {

                    aController.playSound(AudioContainer.Self.Weapon_PickUp);
                    damageModifier += 0.25f;
                    assaultRef.damagePerShot += (int)(assaultRef.startingDamage * 0.25f);
                    shotgunRef.damagePerShot += (int)(shotgunRef.startingDamage * 0.25f);
                    refManager.uicontroller.UpdateWeaponUpgrade(25);
                    refManager.uicontroller.score -= (int)(baseCost[2] * Mathf.Pow(1.2f, costModifier[2]) / 2);
                    refManager.uicontroller.spentScore += (int)(baseCost[2] * Mathf.Pow(1.2f, costModifier[2]) / 2);
                    costModifier[2] += 0.5f;
                    nearBox.transform.GetChild(1).GetComponent<TextMesh>().text = (int)(baseCost[2] * Mathf.Pow(1.2f, costModifier[2]) / 2) + " SP";
                    refManager.uicontroller.UpdateScore();
                    damageUp += 25;
                    refManager.uicontroller.UpdateWeaponUpgrade(damageUp);

                    if (currentScene == "Tutorial" && tutorialElements.step == 54)
                    {
                        tutorialElements.HideStep();
                        tutorialElements.NextStep();
                    }
                }
                else
                {
                    deniedBox.Play();
                    nearBox.gameObject.transform.GetChild(1).GetComponent<TextMesh>().color = Color.red;
                }


            }
        }

        if (nearBox.tag == "Armor_PickUp")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (refManager.uicontroller.score >= baseCost[3] * Mathf.Pow(1.2f, costModifier[3]) / 2)
                {
                    aController.playSound(AudioContainer.Self.Armor_PickUp);
                    refManager.uicontroller.score -= (int)(baseCost[3] * Mathf.Pow(1.2f, costModifier[3]) / 2);
                    refManager.uicontroller.spentScore += (int)(baseCost[3] * Mathf.Pow(1.2f, costModifier[3]) / 2);
                    costModifier[3] += 0.5f;
                    nearBox.transform.GetChild(1).GetComponent<TextMesh>().text = baseCost[3] * costModifier[3] + " SP";
                    refManager.uicontroller.UpdateScore();
                    maxHealth += 25;
                    currentHealth += 25;
                    armorUpgrade += 25;
                    armorUp += 25;
                    refManager.uicontroller.UpdateArmorUpgrade(armorUp);
                    refManager.uicontroller.IncreaseLife();

                    if (currentScene == "Tutorial" && tutorialElements.step == 53)
                    {
                        tutorialElements.HideStep();
                        tutorialElements.NextStep();
                    }

                }
                else
                {
                    deniedBox.Play();
                    nearBox.transform.GetChild(1).GetComponent<TextMesh>().color = Color.red;
                }
            }
        }
    }

    public int armorUpgrade = 0;

    void EnterBox(GameObject nearBox)
    {
        if (nearBox.tag == "Health_PickUp")
        {
            refManager.uicontroller.ShowPrompt();
            nearBox.GetComponent<PickUp>().Show();
            nearBox.transform.GetChild(1).GetComponent<TextMesh>().text = baseCost[0] * Mathf.Pow(1.2f, costModifier[0]) / 2 + " SP";
            nearBox.transform.GetChild(1).GetComponent<TextMesh>().color = Color.white;
        }

        if (nearBox.tag == "Ammo_PickUp")
        {
            refManager.uicontroller.ShowPrompt();
            nearBox.GetComponent<PickUp>().Show();
            nearBox.transform.GetChild(1).GetComponent<TextMesh>().text = baseCost[1] * Mathf.Pow(1.2f, costModifier[1]) / 2 + " SP";
            nearBox.transform.GetChild(1).GetComponent<TextMesh>().color = Color.white;
        }

        if (nearBox.tag == "Weapon_PickUp")
        {
            refManager.uicontroller.ShowPrompt();
            nearBox.GetComponent<PickUp>().Show();
            nearBox.transform.GetChild(1).GetComponent<TextMesh>().text = baseCost[2] * Mathf.Pow(1.2f, costModifier[2]) / 2 + " SP";
            nearBox.transform.GetChild(1).GetComponent<TextMesh>().color = Color.white;
        }

        if (nearBox.tag == "Armor_PickUp")
        {
            refManager.uicontroller.ShowPrompt();
            nearBox.GetComponent<PickUp>().Show();
            nearBox.transform.GetChild(1).GetComponent<TextMesh>().text = baseCost[3] * Mathf.Pow(1.2f, costModifier[3]) / 2 + " SP";
        }
    }

    void ExitBox(GameObject nearBox)
    {
        if (nearBox.tag == "Health_PickUp")
        {
            nearBox.transform.GetChild(1).GetComponent<TextMesh>().color = Color.white;
            refManager.uicontroller.HidePrompt();
            nearBox.GetComponent<PickUp>().Hide();
        }

        if (nearBox.tag == "Ammo_PickUp")
        {
            nearBox.transform.GetChild(1).GetComponent<TextMesh>().color = Color.white;
            refManager.uicontroller.HidePrompt();
            nearBox.GetComponent<PickUp>().Hide();
        }
        if (nearBox.tag == "Weapon_PickUp")
        {
            nearBox.transform.GetChild(1).GetComponent<TextMesh>().color = Color.white;
            refManager.uicontroller.HidePrompt();
            nearBox.GetComponent<PickUp>().Hide();
        }

        if (nearBox.tag == "Armor_PickUp")
        {
            nearBox.gameObject.transform.GetChild(1).GetComponent<TextMesh>().color = Color.white;
            refManager.uicontroller.HidePrompt();
            nearBox.GetComponent<PickUp>().Hide();
        }
    }

    void FixedUpdate()
    {
        if (refManager.flyCamRef.endedCutScene && isAlive == true && tutorial == false)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            rx = Input.GetAxisRaw("Horizontal_Stick");
            ry = Input.GetAxisRaw("Vertical_Stick");

            if (isDashing)
            {
                if (newRange >= 1)
                {
                    playerRigidbody.drag = 0;
                    playerRigidbody.AddForce(movement.normalized * 100 * Time.fixedDeltaTime, ForceMode.Impulse);
                    newRange--;
                }
            }
            else
            {
                newRange = dashRange;
                Move(h, v);

                //Step 1 Tutorial Movimento
                if (stepTutorial == true)
                {
                    stepTutorial = false;
                    tutorialElements.NextStep();
                }
            }

            if (((rx <= 0.15f && rx >= -0.15f) && (ry <= 0.15f && ry >= -0.15f)) || isDashing)
            {
                rotating = false;
                transform.rotation = lastRotation;
            }
            else
            {
                rotating = true;
                shootDirection = transform.parent.transform.right * Input.GetAxis("Horizontal_Stick") + transform.parent.transform.right * Input.GetAxis("Vertical_Stick");
                shootDirection.x = rx * Mathf.Cos(Mathf.Deg2Rad * (transform.parent.transform.eulerAngles.y + 0)) + ry * Mathf.Sin(Mathf.Deg2Rad * (transform.parent.transform.eulerAngles.y + 0));
                shootDirection.z = -rx * Mathf.Sin(Mathf.Deg2Rad * (transform.parent.transform.eulerAngles.y + 0)) + ry * Mathf.Cos(Mathf.Deg2Rad * (transform.parent.transform.eulerAngles.y + 0));
                transform.rotation = Quaternion.LookRotation(shootDirection, Vector3.up);
            }
            lastRotation = transform.rotation;
        }
    }

    public Quaternion lastRotation;

    public class OccludedObject
    {
        public Material[] matArray;
        public GameObject occludedObj;
        public MeshRenderer meshRef;
    }

    void DashTutorialMode()
    {
        //Step 2 Tutorial Dash
        if (dashTutorial == true)
        {
            dashTutorial = false;
            Debug.Log("sono qui");
            tutorialElements.NextStep();
        }
    }

    public GameObject boxToPass;
    public bool weaponActive = true;
    public bool ammoActive = true;
    public bool lifeActive = true;
    public bool armorActive = true;


    void Update()
    {
        dashMaterial.SetColor("_TintColor",gradient.Evaluate(1-(float)currentHealth / (float)maxHealth));
        boxToPass = weaponBox;
        if (Vector3.Distance(this.transform.position, weaponBoxTr.position) < 5 && weaponActive)
        {          
            if (!insideWeapon)
            {
                EnterBox(boxToPass);
            }
            insideWeapon = true;
        }
        else
        {
            if (insideWeapon)
            {
                insideWeapon = false;
                ExitBox(boxToPass);
            }
        }
        boxToPass = ammoBox;
        if (Vector3.Distance(this.transform.position, ammoBoxTr.position) < 5 && ammoActive)
        {   
            if (!insideAmmo)
            {
                EnterBox(boxToPass);
            }
            insideAmmo = true;
        }
        else
        {
            if (insideAmmo)
            {
                insideAmmo = false;
                ExitBox(boxToPass);
            }
        }
        boxToPass = lifeBox;
        if (Vector3.Distance(this.transform.position, lifeBoxTr.position) < 5 && lifeActive)
        { 
            if (!insideLife)
            {
                EnterBox(boxToPass);
            }
            insideLife = true;
        }
        else
        {
            if (insideLife)
            {
                insideLife = false;
                ExitBox(boxToPass);
            }
        }
        boxToPass = armorBox;
        if (Vector3.Distance(this.transform.position, armorBoxTr.position) < 5 && armorActive)
        {
            if (!insideArmor)
            {
                EnterBox(boxToPass);
            }
            insideArmor = true;
        }
        else
        {
            if (insideArmor)
            {
                insideArmor = false;
                ExitBox(boxToPass);
            }
        }
        boxToPass = null;
        if (Input.GetButtonDown("Fire1"))
        {
            if (insideWeapon)
            {
                boxToPass = weaponBox;
            }
            else if (insideAmmo)
            {
                boxToPass = ammoBox;
            }
            else if (insideLife)
            {
                boxToPass = lifeBox;
            }
            else if (insideArmor)
            {
                boxToPass = armorBox;
            }
            if (boxToPass != null)
            {
                UseBox(boxToPass);
            }
   
        }

      

        if (Input.GetButtonDown("Selection"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetButtonDown("Previous Weapon"))
        {
            if (godMode == false)
            {
                godMode = true;
                refManager.uicontroller.GodModeOn();
            }

            else if (godMode == true)
            {
                godMode = false;
                refManager.uicontroller.GodModeOff();
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyAllEnemies();
        }

        if (refManager.flyCamRef.endedCutScene)
        {
            occlusionRay = new Ray(this.transform.position, Camera.main.transform.position - this.transform.position);

            occlusionHit = Physics.RaycastAll(occlusionRay);
            foreach (var mesh in occlusionHit)
            {
                if (mesh.collider.gameObject.tag != "MainCamera" && mesh.collider.gameObject.tag != "Player" && mesh.collider.gameObject.tag != "ringhiera" && mesh.collider.tag != "Destructible" && mesh.collider.tag != "Enemy" && mesh.collider.tag != "Terrain" && mesh.collider.GetComponent<MeshRenderer>())
                {
                    if (mesh.collider.gameObject != this)
                    {
                        int counter = 0;
                        foreach (var diobubu in occludedGoList)
                        {
                            if (diobubu.occludedObj == mesh.collider.gameObject)
                            {
                                counter++;
                            }
                        }
                        if (counter == 0)
                        {
                            StartCoroutine(LerpAlpha(mesh.collider.gameObject, 1));

                            StartCoroutine(StillOccluding(mesh.collider.gameObject));
                        }
                        counter = 0;
                    }
                }
            }

            if (Input.GetButton("Dash") && dashAttivo == true)
            {
                DashTutorialMode();
                StartCoroutine(Dash());
            }
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.transform.tag == "notWalkable")
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
        }
    }

    public BoxCollider groundTrigger;
    public bool isGrounded = true;
    public GameObject map;

    void Move(float h, float v)
    {
        Vector3 animationVector = new Vector3();
        if (isGrounded)
        {
            movement.Set(h, 0f, v);

            movement.x = h * Mathf.Cos(Mathf.Deg2Rad * (transform.parent.transform.eulerAngles.y + 0)) + v * Mathf.Sin(Mathf.Deg2Rad * (transform.parent.transform.eulerAngles.y + 0));
            movement.z = -h * Mathf.Sin(Mathf.Deg2Rad * (transform.parent.transform.eulerAngles.y + 0)) + v * Mathf.Cos(Mathf.Deg2Rad * (transform.parent.transform.eulerAngles.y + 0));

            animationVector.x = h * Mathf.Cos(Mathf.Deg2Rad * (-transform.localEulerAngles.y)) + v * Mathf.Sin(Mathf.Deg2Rad * (-transform.localEulerAngles.y));
            animationVector.z = -h * Mathf.Sin(Mathf.Deg2Rad * (-transform.localEulerAngles.y)) + v * Mathf.Cos(Mathf.Deg2Rad * (-transform.localEulerAngles.y));
            animationVector.Normalize();

            Animating(animationVector);

            movement = movement.normalized * speed * Time.fixedDeltaTime;
            playerRigidbody.MovePosition(transform.position + movement);

        }
    }

    void Animating(Vector3 animMovement)
    {
        anim.SetFloat("Forward", animMovement.z);
        anim.SetFloat("Lateral", animMovement.x);
    }

    public float dashRange = 250;
    public float newRange;
    public bool isDashing = false;

    IEnumerator Dash()
    {
        aSource.clip = AudioContainer.Self.Dash;
        aSource.Play();

        isDashing = true;
        dashRay.origin = transform.position;
        dashRay.direction = movement.normalized;

        dashAttivo = false;

        float newRange;

        if (Physics.Raycast(dashRay, out dashRayHit, dashRange))
        {
            newRange = dashRayHit.distance;
        }
        else
        {
            newRange = dashRange;
        }

        ParticleSystem.EmissionModule emitter = trailPs.emission;

        emitter.rate = 150;
        yield return new WaitForSeconds(0.3f);
        emitter.rate = 30;
        isDashing = false;
        playerRigidbody.drag = 5;
        yield return new WaitForSeconds(0.3f);
        playerRigidbody.drag = 1;
        yield return new WaitForSeconds(0.3f);
        dashAttivo = true;
        newRange = dashRange;
    }

    Coroutine combat;
    TextMesh textMesh;

    virtual public IEnumerator CombatText(int damage)
    {
        textMesh.characterSize = 0.46f;
        float timer = 0;
        textMesh.text = "-" + damage.ToString();

        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            textMesh.transform.localPosition = Vector3.Lerp(new Vector3(0, 2.5f, 0), new Vector3(0, 4, 0), timer * 2);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        textMesh.transform.localPosition = new Vector3(0, 3, 0);
        timer = 0;
        textMesh.characterSize = 0.46f;
        textMesh.text = "";
        combat = null;
    }

    public Coroutine Co;

    public IEnumerator DefrostPlayer()
    {
        float timer = 0;
        while (timer < 5)
        {
            Debug.Log("waiting");
            timer += Time.deltaTime;
            yield return null;
        }
        Debug.Log("defrosted");
        speed = 6;
        Co = null;
    }

    public void TakeDamage(float damageTaken)
    {
        if (godMode == false)
        {
            currentHealth -= (int)damageTaken;
            refManager.uicontroller.DecrementLife((float)damageTaken / 100);
        }

        if (combat == null)
        {
            combat = StartCoroutine(CombatText((int)damageTaken));
        }

        else
        {
            StopCoroutine(combat);
            combat = StartCoroutine(CombatText((int)damageTaken));
        }

        if (currentHealth <= 0 && isAlive == true && godMode == false)
        {
            isAlive = false;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        refManager.uicontroller.GameOverOn();
        yield return null;
    }

    public IEnumerator StillOccluding(GameObject go)
    {
        bool found = false;
        yield return new WaitForSeconds(0.5f);

        antiOcclusionRay = new Ray(this.transform.position, Camera.main.transform.position - this.transform.position);
        antiOcclusionHit = Physics.RaycastAll(antiOcclusionRay);

        foreach (var mesh in antiOcclusionHit)
        {
            if (mesh.collider.gameObject.tag != "Player")
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
        if (go != this.gameObject && go.tag != "Enemy")
        {
            if (sign > 0)
            {
                // Debug.LogError("occluso");
                occludedGoList.Add(new OccludedObject());
                occludedGoList[occludedGoList.Count - 1].occludedObj = go;
                occludedGoList[occludedGoList.Count - 1].meshRef = go.GetComponent<MeshRenderer>();
                occludedGoList[occludedGoList.Count - 1].matArray = occludedGoList[occludedGoList.Count - 1].meshRef.materials;

                for (int i = 0; i < occludedGoList[occludedGoList.Count - 1].meshRef.materials.Length; i++)
                {
                    occludedGoList[occludedGoList.Count - 1].meshRef.materials[i].SetFloat("_Mode", 2);
                    occludedGoList[occludedGoList.Count - 1].meshRef.materials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    occludedGoList[occludedGoList.Count - 1].meshRef.materials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    occludedGoList[occludedGoList.Count - 1].meshRef.materials[i].SetInt("_ZWrite", 0);
                    occludedGoList[occludedGoList.Count - 1].meshRef.materials[i].DisableKeyword("_ALPHATEST_ON");
                    occludedGoList[occludedGoList.Count - 1].meshRef.materials[i].EnableKeyword("_ALPHABLEND_ON");
                    occludedGoList[occludedGoList.Count - 1].meshRef.materials[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    occludedGoList[occludedGoList.Count - 1].meshRef.materials[i].renderQueue = 3000;
                }

                while (occludedGoList[occludedGoList.Count - 1].meshRef.material.color.a > 0.4f)
                {
                    occludedGoList[occludedGoList.Count - 1].meshRef.material.color += new Color(0, 0, 0, -3 * Time.deltaTime);
                    yield return null;
                }
            }
            else
            {
                while (occludedGoList[occludedGoList.Count - 1].meshRef.material.color.a < 1f)
                {
                    occludedGoList[occludedGoList.Count - 1].meshRef.material.color += new Color(0, 0, 0, 3 * Time.deltaTime);
                    yield return null;
                }
                for (int i = 0; i < occludedGoList[occludedGoList.Count - 1].meshRef.materials.Length; i++)
                {
                    occludedGoList[occludedGoList.Count - 1].meshRef.materials[i] = occludedGoList[occludedGoList.Count - 1].matArray[i];
                }

                occludedGoList.Remove(occludedGoList[occludedGoList.Count - 1]);
            }
        }
    }
}