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

    public EventSystem eventRef;
    public StandaloneInputModule standRef;

    public AudioSource backAudioRef;
    FlyCamManager flyRef;

    bool paused;

    void Awake()
    {
        flyRef = FindObjectOfType<FlyCamManager>();
        resume = GameObject.Find("Resume");
        options = GameObject.Find("Options");
        quit = GameObject.Find("QuitToMenu");
        generalVolume = GameObject.Find("Volume Principale");
        eventRef = FindObjectOfType<EventSystem>();
        standRef = FindObjectOfType<StandaloneInputModule>();
        CanvasPanel1.SetActive(false);
        CanvasPanel2.SetActive(false);
    }

    bool moved = false;
    float timer = 0;

    void Update()
    {
        if ((Input.GetButtonDown("GodMode") || (paused && Input.GetButtonDown("Cancel")))&& !flyRef.cutScene)
        {
            if (!paused)
            {
                CanvasPanel1.SetActive(true);

                eventRef.SetSelectedGameObject(options);
                eventRef.SetSelectedGameObject(resume);

                Time.timeScale = 0;
                paused = true;
            }
            else
            {
                CanvasPanel1.SetActive(false);
                CanvasPanel2.SetActive(false);
                Time.timeScale = 1;
                paused = false;
            }
        }


        if (CanvasPanel2.activeInHierarchy)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                if (backAudioRef)
                {
                    backAudioRef.Play();
                }

                CanvasPanel1.SetActive(true);
                CanvasPanel2.SetActive(false);
                eventRef.SetSelectedGameObject(resume);
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

        if (CanvasPanel1.activeInHierarchy)
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
        }
    }
    public void QuitToMenu()
    {
        FindObjectOfType<Achievement>().SaveScore(FindObjectOfType<UIController>().score);
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
    public void AudioOptions()
    {
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
