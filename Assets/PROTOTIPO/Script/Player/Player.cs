using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class Player: MonoBehaviour
{
    public bool godMode = false;
    public bool isAlive = true;
    public bool tutorialMode = false;
    public bool tutorial = false;
    public bool stepTutorial = false;
    public bool noWeapons = false;

    FlyCamManager flyCamRef;
    Rigidbody playerRigidbody;
    private NavMeshAgent _navAgent;
    DialogueSystem dialoghi;
    UIController uicontroller;
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

    public float[] costModifier = new float[4];
    public int[] baseCost = new int[4];

    public Ray occlusionRay;
    public RaycastHit[] occlusionHit;
    public Ray antiOcclusionRay;
    public RaycastHit[] antiOcclusionHit;
    public List<OccludedObject> occludedGoList = new List<OccludedObject>();

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        flyCamRef = FindObjectOfType<FlyCamManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        tutorialElements = FindObjectOfType<Tutorial>();
    }

    void Start()
    {
        uicontroller = FindObjectOfType<UIController>();
        dialoghi = FindObjectOfType<DialogueSystem>();
        
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Health_PickUp")
        {
            coll.gameObject.transform.GetChild(1).LookAt(Camera.main.transform);
            coll.gameObject.transform.GetChild(1).Rotate(new Vector3(0, 180, 0));
            if (Input.GetButtonDown("Fire1"))
            {
                if (uicontroller.score >= baseCost[0] * costModifier[0])
                {
                    uicontroller.score -= (int)(baseCost[0] * costModifier[0]);
                    uicontroller.UpdateScore();
                    currentHealth = maxHealth;
                    uicontroller.IncreaseLife();
                }
                else
                {
                    coll.gameObject.transform.GetChild(1).GetComponent<TextMesh>().color = Color.red;
                }
            }
        }

        if (coll.tag == "Ammo_PickUp")
        {
            coll.gameObject.transform.GetChild(1).LookAt(Camera.main.transform);
            coll.gameObject.transform.GetChild(1).Rotate(new Vector3(0, 180, 0));
            if (Input.GetButtonDown("Fire1"))
            {
          
                if (uicontroller.score >= baseCost[1] * costModifier[1])
                {
                    uicontroller.score -= (int)(baseCost[1] * costModifier[1]);
                    uicontroller.UpdateScore();
                    rocketAmmo += 5;
                    uicontroller.ammo.text = rocketAmmo.ToString();
                }
                else
                {
                    coll.gameObject.transform.GetChild(1).GetComponent<TextMesh>().color = Color.red;
                }
            }
        }

        if (coll.tag == "Weapon_PickUp")
        {
            coll.gameObject.transform.GetChild(1).LookAt(Camera.main.transform);
            coll.gameObject.transform.GetChild(1).Rotate(new Vector3(0, 180, 0));
            if (Input.GetButtonDown("Fire1"))
            {
                if (uicontroller.score >= baseCost[2] * costModifier[2])
                {
                    damageModifier += 0.25f;
                    uicontroller.UpdateWeaponUpgrade(25);
                    uicontroller.score -= (int)(baseCost[2] * costModifier[2]);
                    uicontroller.UpdateScore();
                    uicontroller.UpdateWeaponUpgrade(25);             
                }
                else
                {
                    coll.gameObject.transform.GetChild(1).GetComponent<TextMesh>().color = Color.red;
                }
            }  
        }

        if (coll.tag == "Armor_PickUp")
        {
            coll.gameObject.transform.GetChild(1).LookAt(Camera.main.transform);
            coll.gameObject.transform.GetChild(1).Rotate(new Vector3(0, 180, 0));
            if (Input.GetButtonDown("Fire1"))
            {
                if (uicontroller.score >= baseCost[3] * costModifier[3])
                {
              
                    uicontroller.score -= (int)(baseCost[3] * costModifier[3]);
                    uicontroller.UpdateScore();
                    maxHealth += 25;
                    armorUpgrade += 25;
                    uicontroller.UpdateWeaponUpgrade(25);
                }
                else
                {
                    coll.gameObject.transform.GetChild(1).GetComponent<TextMesh>().color = Color.red;
                }
            }
        }
    }

    public int armorUpgrade = 0;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Health_PickUp")
        {
            uicontroller.ShowPrompt();
            coll.GetComponent<PickUp>().Show();
            coll.gameObject.transform.GetChild(1).GetComponent<TextMesh>().text = "$ " + baseCost[0] * costModifier[0] + "\n" + "Restore Health";
        }

        if (coll.tag == "Ammo_PickUp")
        {
            uicontroller.ShowPrompt();
            coll.GetComponent<PickUp>().Show();
            coll.gameObject.transform.GetChild(1).GetComponent<TextMesh>().text = "$ " + baseCost[1] * costModifier[1] + "\n" + "Reload Rockets";
        }

        if (coll.tag == "Weapon_PickUp")
        {
            uicontroller.ShowPrompt();
            coll.GetComponent<PickUp>().Show();
            coll.gameObject.transform.GetChild(1).GetComponent<TextMesh>().text = "$ " + baseCost[2] * costModifier[2] + "\n" + "Empower Weapons";
        }

        if (coll.tag == "Armor_PickUp")
        {
            uicontroller.ShowPrompt();
            coll.GetComponent<PickUp>().Show();
            coll.gameObject.transform.GetChild(1).GetComponent<TextMesh>().text = "$ " + baseCost[3] * costModifier[3] + "\n" + "Empower shields";
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Health_PickUp")
        {
            uicontroller.HidePrompt();
            coll.GetComponent<PickUp>().Hide(); 
        }

        if (coll.tag == "Ammo_PickUp")
        {
            uicontroller.HidePrompt();
            coll.GetComponent<PickUp>().Hide();
        }
        if (coll.tag == "Weapon_PickUp")
        {
            uicontroller.HidePrompt();
            coll.GetComponent<PickUp>().Hide();
        }

        if (coll.tag == "Armor_PickUp")
        {
            uicontroller.HidePrompt();
            coll.GetComponent<PickUp>().Hide();
        }
    }

    void FixedUpdate()
    {
        if (flyCamRef.endedCutScene && isAlive == true && tutorial == false)
        {

            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            float rx = Input.GetAxisRaw("Horizontal_Stick");
            float ry = Input.GetAxisRaw("Vertical_Stick");


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
                Animating(h, v);

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
                    shootDirection = Vector3.right * Input.GetAxis("Horizontal_Stick") + Vector3.forward * Input.GetAxis("Vertical_Stick");
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

    void Update()
    {
        

        if (Input.GetButtonDown("Selection"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetButtonDown("GodMode"))
        {
            if (godMode == false)
            {
                godMode = true;
                uicontroller.GodModeOn();
            }

            else if (godMode == true)
            {
                godMode = false;
                uicontroller.GodModeOff();
            }

        }

        if (Input.GetButtonDown("Selection") && Input.GetButtonDown("GodMode"))
        {
            SceneManager.LoadScene("Menu Alfa");
        }


        if (flyCamRef.endedCutScene)
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
                StartCoroutine(Dash());

                if (tutorialMode == true)
                    tutorialElements.NextStep();

                    

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

    void Move(float h, float v)
    {
        if (isGrounded)
        {
            movement.Set(h, 0f, v);
            movement = movement.normalized * speed * Time.fixedDeltaTime;
            playerRigidbody.MovePosition(transform.position + movement);
        }
    }

    

    public bool walking;

    void Animating(float h, float v)
    {
        walking = h != 0f || v != 0f;
        if (walking)
        {
            //anim.SetBool("moving", true);
            //anim.SetFloat("run", h);
            //anim.SetFloat("side", v);
        }
        else
        {
            //anim.SetBool("moving", false);
        }
    }

    public float dashRange = 250;
    public float newRange;
    public bool isDashing = false;

    IEnumerator Dash()
    {
        isDashing = true;
        dashRay.origin = transform.position;
        dashRay.direction = movement.normalized;

        TrailRenderer trailRef = GetComponentInChildren<TrailRenderer>();
        dashAttivo = false;

        trailRef.enabled = true;

        float newRange;

        if (Physics.Raycast(dashRay, out dashRayHit, dashRange))
        {
            newRange = dashRayHit.distance;
        }
        else
        {
            newRange = dashRange;
        }

        trailRef.time = 0.1f;
        yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(0.2f);
        isDashing = false;
        playerRigidbody.drag = 5;
        trailRef.Clear();
        trailRef.enabled = false;
        yield return new WaitForSeconds(0.3f);
        playerRigidbody.drag = 1;
        dashAttivo = true;
        newRange = dashRange;
    }

    public void TakeDamage(float damageTaken)
    {
        /* if (primoSangue == true)
         {
             //primoSangue = false;
             //dialoghi.SetDialogue(1);
         }*/

        if (godMode == false)
        {
            currentHealth -= (int)damageTaken;
            uicontroller.DecrementLife((float)damageTaken / 100);
        }
        

        if (currentHealth <= 0 && isAlive == true && godMode == false)
        {
            isAlive = false;
            StartCoroutine(Die());
        }
        
    }

    IEnumerator Die()
    {
        uicontroller.GameOverOn();

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
                occludedGoList[occludedGoList.Count-1].matArray = occludedGoList[occludedGoList.Count - 1].meshRef.materials;

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