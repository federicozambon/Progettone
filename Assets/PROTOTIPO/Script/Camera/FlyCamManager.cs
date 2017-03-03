using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FlyCamManager : MonoBehaviour
{
    UIController elementsUI;
    public GameObject playerGo;
    public Camera[] camArray = new Camera[5];
    public Camera mainCamera;
    public Transform[] wayPoints = new Transform[5];
    public Transform[] lookAtTr = new Transform[5];
    public float[] velocityArray = new float[5];
    public bool[] interpolationTypes = new bool[5];
    public int activeWaypoint;
    public GameObject[] cupole = new GameObject[4];

    WaveController wcRef;
    RocketLauncher rLauncher;
    public bool cutScene;
    public bool endedCutScene = false;
    public bool tutorial = false;

    void Awake()
    {
<<<<<<< HEAD
        cutSceneCanvas = GameObject.Find("CanvasCutscene").GetComponent<Canvas>();
=======
>>>>>>> 67490208d8f1636bb01080cacc99937b451e63b3
        Application.targetFrameRate = 144;
        playerGo = FindObjectOfType<Player>().gameObject;
        elementsUI = FindObjectOfType<UIController>();
        wcRef = FindObjectOfType<WaveController>();
        rLauncher = FindObjectOfType<RocketLauncher>();
    }

    public void SwitchCamera(Camera oldCam, Camera newCam)
    {
        oldCam.gameObject.SetActive(false);
        newCam.gameObject.SetActive(true);
    }

    public void LerpAngle(Camera activeCamera, Transform lookAtTarget)
    {
        Camera.current.transform.LookAt(lookAtTarget);
    }

    bool moving, rotating;
    Camera currentCamera;

    public IEnumerator LerpPosition(Camera currentCam, float speed, bool lookAt, int wayPoint)
    {
        float timer = 0;
        activeWaypoint = wayPoint;
        currentCamera = currentCam;
        moving = true;
        cutScene = true;
        Vector3 direction = (currentCam.transform.position - wayPoints[wayPoint].transform.position).normalized;

        while (Vector3.Distance(currentCam.transform.position, wayPoints[wayPoint].transform.position) > 5)
        {
            timer += Time.deltaTime;
          //  Debug.Log(Vector3.Distance(currentCam.transform.position, wayPoints[wayPoint].transform.position));
          
            currentCam.transform.position = Vector3.Lerp(currentCam.transform.position, wayPoints[wayPoint].position, timer / speed);
            if (lookAt)
            {
                currentCam.transform.LookAt(lookAtTr[wayPoint].position);
            }
            yield return null;
        }
        moving = false;
        cutScene = false;
    }

    Vector3 startPos;
    float timer;
    float distanceClass;
    float velocity;

    public IEnumerator LerpRotateAround(Camera currentCam, float speed, float distance, bool lookAt, int lookAtWaypoint, bool clockWise)
    {
        distanceClass = distance;
        currentCamera = currentCam;
        rotating = true;
        cutScene = true;
        startPos = currentCam.transform.position;
        bool lapFinished = false;
        timer = 0;
        while (!lapFinished)
        {
            timer += Time.deltaTime;
         
            if (clockWise)
            {
                currentCam.transform.position = new Vector3((startPos.x + Mathf.Sin(timer) * distance), startPos.y, startPos.z + Mathf.Cos(timer) * distance);
            }
            else
            {
                currentCam.transform.position = new Vector3((startPos.x - Mathf.Sin(timer) * distance), startPos.y, startPos.z - Mathf.Cos(timer) * distance);
            }
            if (lookAt)
            {
                currentCam.transform.LookAt(lookAtTr[lookAtWaypoint].position);
            }

            if (timer > 10)
            {
                lapFinished = true;
            }
            yield return null;
        }
        rotating = false;
        cutScene = false;
    }

    public IEnumerator CutSceneManager()
    {
        SwitchCamera(mainCamera, camArray[0]);

        if (SceneManager.GetActiveScene().name == "Montacarichi1")
        {
            StartCoroutine(LerpPosition(camArray[0], 600f, true, 1));
            while (cutScene)
            {
                yield return null;
            }
            yield return new WaitForSeconds(1f);

            StartCoroutine(LerpRotateAround(camArray[0], 0.5f, 15, true, 1, true));
            while (cutScene)
            {
                yield return null;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Discarica")
        {
            StartCoroutine(LerpPosition(camArray[0], 100f, true, 0));
            while (cutScene)
            {
                yield return null;
            }
            StartCoroutine(LerpPosition(camArray[0], 100f, true, 1));
            while (cutScene)
            {
                yield return null;
            }
            StartCoroutine(LerpRotateAround(camArray[0], 0.1f, 40, true, 0, true));
            while (cutScene)
            {
                yield return null;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Montacarichi2")
        {
            StartCoroutine(LerpPosition(camArray[0], 100f, true, 0));
            while (cutScene)
            {
                yield return null;
            }
            StartCoroutine(LerpPosition(camArray[0], 100f, true, 1));
            while (cutScene)
            {
                yield return null;
            }
            StartCoroutine(LerpRotateAround(camArray[0], 0.1f, 40, true, 0, true));
            while (cutScene)
            {
                yield return null;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Tetto")
        {
            StartCoroutine(LerpPosition(camArray[0], 100f, true, 0));
            while (cutScene)
            {
                yield return null;
            }
            StartCoroutine(LerpPosition(camArray[0], 100f, true, 1));
            while (cutScene)
            {
                yield return null;
            }
            StartCoroutine(LerpRotateAround(camArray[0], 0.1f, 40, true, 0, true));
            while (cutScene)
            {
                yield return null;
            }
        }
        /*else if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            StartCoroutine(LerpPosition(camArray[0], 600f, true, 1));
            while (cutScene)
            {
                yield return null;
            }
            yield return new WaitForSeconds(1f);

            StartCoroutine(LerpRotateAround(camArray[0], 0.5f, 15, true, 1, true));
            while (cutScene)
            {
                yield return null;
            }
        }*/
        SwitchCamera(camArray[1], mainCamera);
        SwitchCamera(camArray[0], mainCamera);
        playerGo.gameObject.SetActive(true);
        StopAllCoroutines();
        
        moving = false;
        rotating = false;

        endedCutScene = true;
        cutScene = false;
        rLauncher.startGame = true;
        elementsUI.CanvasOn();

        //Ho spostato la Coroutine prima del wait altrimenti non spawnavano i nemici
        if(tutorial == false)
        {
            StartCoroutine(wcRef.StartWave());
        }
        else
        {
            FindObjectOfType<Tutorial>().StartTutorial();
        }

  
        yield return new WaitForSeconds(2);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Cancel"))
        {
            SwitchCamera(camArray[1], mainCamera);
            SwitchCamera(camArray[0], mainCamera);
            playerGo.gameObject.SetActive(true);
            StopAllCoroutines();
     
            moving = false;
            rotating = false;

            endedCutScene = true;
            cutScene = false;
            rLauncher.startGame = true;
            elementsUI.CanvasOn();
            if (tutorial == false)
            {
                StartCoroutine(wcRef.StartWave());
            }
            else
            {
                FindObjectOfType<Tutorial>().StartTutorial();
            }
        }
        if (moving)
        {
            currentCamera.transform.position = Vector3.Lerp(currentCamera.transform.position, wayPoints[activeWaypoint].position, Mathf.SmoothDamp(0, 1, ref velocity, Time.deltaTime * 100));
        }
        if (rotating)
        {
            currentCamera.transform.position = new Vector3((startPos.x + Mathf.Sin(timer/2) * distanceClass), startPos.y, startPos.z + Mathf.Cos(timer/2) * distanceClass);
        }   
    }

    void Start()
    {
        StartCoroutine(CutSceneManager());
    }

    public void Skip()
    {
        SwitchCamera(camArray[1], mainCamera);
        SwitchCamera(camArray[0], mainCamera);
        playerGo.gameObject.SetActive(true);
        StopAllCoroutines();

        moving = false;
        rotating = false;

        endedCutScene = true;
        cutScene = false;
        rLauncher.startGame = true;
        elementsUI.CanvasOn();
        if (tutorial == false)
            StartCoroutine(wcRef.StartWave());
        else
        {
            FindObjectOfType<Tutorial>().StartTutorial();
        }
    }
}


