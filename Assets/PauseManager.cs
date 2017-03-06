using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject CanvasPanel1;
    public GameObject CanvasPanel2;
    public GameObject resume;
    public GameObject options;
    public GameObject quit;

    public GameObject generalVolume;

    public Player playerRef;
    public EventSystem eventRef;
    public StandaloneInputModule standRef;

    public AudioSource backAudioRef;
    FlyCamManager flyRef;

    public bool paused;
    bool audioMode = false;

    void Awake()
    {
        playerRef = FindObjectOfType<Player>();
        flyRef = FindObjectOfType<FlyCamManager>();
        //resume = GameObject.Find("Resume");
        //options = GameObject.Find("Options");
        //quit = GameObject.Find("QuitToMenu");
        //generalVolume = GameObject.Find("Volume Principale");
        eventRef = FindObjectOfType<EventSystem>();
        standRef = FindObjectOfType<StandaloneInputModule>();
        CanvasPanel1.SetActive(false);
        CanvasPanel2.SetActive(false);
    }

    bool moved = false;
    float timer = 0;
    public float late = 0;
    void Update()
    {
        if (paused)
        {
            late += Time.deltaTime;
        }
        if ((Input.GetButtonDown("GodMode") || (audioMode == false && paused && Input.GetButtonDown("Cancel")))&& !flyRef.cutScene && playerRef.currentHealth>0)
        {
   
            Debug.Log("qui qui qui");

            if (!paused)
            {
                playerRef.pausePosition = playerRef.transform.position;
                
                //playerRef.transform.position = new Vector3(1000, 1000, 1000);
                PauseActiveMenu();
                
                Time.timeScale = 0.001f;
                paused = true;
                
            }
            else
            {
             
                //playerRef.transform.position = playerRef.pausePosition;
                CanvasPanel1.SetActive(false);
                CanvasPanel2.SetActive(false);
                eventRef.SetSelectedGameObject(null);

                Time.timeScale = 1;
                paused = false;
            }
        }


        if (CanvasPanel2.activeInHierarchy)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                

                CanvasPanel2.SetActive(false);
                CanvasPanel1.SetActive(true);
                eventRef.SetSelectedGameObject(resume);

                if (backAudioRef)
                {
                    backAudioRef.Play();
                }

                audioMode = false;
                
            }
            /*
            if (Input.GetAxisRaw("Horizontal") > 0.3f)
            {
                eventRef.currentSelectedGameObject.GetComponent<Slider>().value += 0.2f;
            }
            if (Input.GetAxisRaw("Horizontal") < -0.3f)
            {
                eventRef.currentSelectedGameObject.GetComponent<Slider>().value -= 0.2f;
            }   
            */
        }

       /* if (CanvasPanel1.activeInHierarchy)
        {
            if (!moved)
            {

                if (Input.GetAxisRaw("Vertical") < -0.3f)
                {

                    moved = true;

                    if (eventRef.currentSelectedGameObject == resume)
                    {
                        eventRef.SetSelectedGameObject(options);
                    }
                    else if (eventRef.currentSelectedGameObject == options)
                    {
                        eventRef.SetSelectedGameObject(quit);
                    }
                    else if (eventRef.currentSelectedGameObject == quit)
                    {
                        eventRef.SetSelectedGameObject(resume);
                    }
                }
            }

            if (Input.GetAxisRaw("Vertical") > 0.3f)
            {
                moved = true;
                if (eventRef.currentSelectedGameObject == resume)
                {
                    eventRef.SetSelectedGameObject(quit);
                }
                else if (eventRef.currentSelectedGameObject == quit)
                {
                    eventRef.SetSelectedGameObject(options);
                }
                else if (eventRef.currentSelectedGameObject == options)
                {
                    eventRef.SetSelectedGameObject(resume);
                }
            }
        }
        if (Input.GetAxisRaw("Vertical") > -0.1 && Input.GetAxisRaw("Vertical") < 0.1)
        {
            moved = false;
        }*/
    }
    public void QuitToMenu()
    {
        FindObjectOfType<Achievement>().SaveScore(FindObjectOfType<UIController>().score);
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void PauseActiveMenu()
    {
        CanvasPanel1.SetActive(true);
        eventRef.SetSelectedGameObject(resume);
        CanvasPanel2.SetActive(false);
    }

    public void AudioOptions()
    {
        audioMode = true;
        CanvasPanel2.SetActive(true);
        eventRef.SetSelectedGameObject(generalVolume);
        CanvasPanel1.SetActive(false);  
    }
    public void Resume()
    {
        Time.timeScale = 1;
        CanvasPanel1.SetActive(false);
    }
}
