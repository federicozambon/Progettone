using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {
    public GameObject CanvasPanel1;
    public GameObject CanvasPanel2;
    public GameObject resume;
    public GameObject options;
    public GameObject quit;

    public GameObject generalVolume;
    public GameObject musicVolume;
    public GameObject sfxVolume;

    public EventSystem eventRef;
    public StandaloneInputModule standRef;

    public AudioSource backAudioRef;

    bool paused;

	void Awake ()
    {
        resume = GameObject.Find("Resume");
        options = GameObject.Find("Options");
        quit = GameObject.Find("QuitToMenu");
        generalVolume = GameObject.Find("HandleP");
        musicVolume = GameObject.Find("HandleM");
        sfxVolume = GameObject.Find("HandleS");
        eventRef = FindObjectOfType<EventSystem>();
        standRef = FindObjectOfType<StandaloneInputModule>();
        CanvasPanel1.SetActive(false);
        CanvasPanel2.SetActive(false);
    }

    bool moved = false;
    float timer = 0;

    void Update()
    {
        if (Input.GetButtonDown("GodMode"))
        {
            if (!paused)
            {
                CanvasPanel1.SetActive(true);
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
        if (Input.GetAxisRaw("Vertical") > -0.1 && Input.GetAxisRaw("Vertical") < 0.1)
        {
            moved = false;
        }

        if (CanvasPanel2.activeInHierarchy)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                backAudioRef.Play();
                CanvasPanel1.SetActive(true);
                CanvasPanel2.SetActive(false);
                eventRef.SetSelectedGameObject(resume);
            }
            if (!moved)
            {
                if (Input.GetAxisRaw("Vertical") > 0.3f)
                {
                    moved = true;
                    if (eventRef.currentSelectedGameObject == generalVolume)
                    {
                        eventRef.SetSelectedGameObject(sfxVolume);
                    }
                    if (eventRef.currentSelectedGameObject == sfxVolume)
                    {
                        eventRef.SetSelectedGameObject(musicVolume);
                    }
                    if (eventRef.currentSelectedGameObject == musicVolume)
                    {
                        eventRef.SetSelectedGameObject(generalVolume);
                    }
                }
                if (Input.GetAxisRaw("Vertical") < -0.3f)
                {
                    moved = true;
                    if (eventRef.currentSelectedGameObject == generalVolume)
                    {
                        eventRef.SetSelectedGameObject(musicVolume);
                    }
                    if (eventRef.currentSelectedGameObject == musicVolume)
                    {
                        eventRef.SetSelectedGameObject(sfxVolume);
                    }
                    if (eventRef.currentSelectedGameObject == sfxVolume)
                    {
                        eventRef.SetSelectedGameObject(generalVolume);
                    }
                }
            }
            if (Input.GetAxisRaw("Horizontal") > 0.3f)
            {
                eventRef.currentSelectedGameObject.transform.parent.transform.parent.GetComponent<Slider>().value += 0.2f;
            }
            if (Input.GetAxisRaw("Horizontal") < -0.3f)
            {
                eventRef.currentSelectedGameObject.transform.parent.transform.parent.GetComponent<Slider>().value -= 0.2f;
            }
        }

   

        if (!moved)
        {
          
            if (Input.GetAxisRaw("Vertical") < -0.3f)
            {
              
                moved = true;
                if (CanvasPanel1.activeInHierarchy)
                {
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
                else
                {
                    if (eventRef.currentSelectedGameObject == generalVolume)
                    {
                        eventRef.SetSelectedGameObject(musicVolume);
                    }
                    else if (eventRef.currentSelectedGameObject == musicVolume)
                    {
                        eventRef.SetSelectedGameObject(sfxVolume);
                    }
                    else if (eventRef.currentSelectedGameObject == sfxVolume)
                    {
                        eventRef.SetSelectedGameObject(generalVolume);
                    }
                }
            }

            if (Input.GetAxisRaw("Vertical") > 0.3f)
            {
                moved = true;
                if (CanvasPanel1.activeInHierarchy)
                {
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
                else
                {
                    if (eventRef.currentSelectedGameObject == generalVolume)
                    {
                        eventRef.SetSelectedGameObject(sfxVolume);
                    }
                    else if (eventRef.currentSelectedGameObject == sfxVolume)
                    {
                        eventRef.SetSelectedGameObject(musicVolume);
                    }
                    else if (eventRef.currentSelectedGameObject == musicVolume)
                    {
                        eventRef.SetSelectedGameObject(generalVolume);
                    }
                }
            }
        }
    }
    public void QuitToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
    public void AudioOptions()
    {     
        CanvasPanel1.SetActive(false);
        CanvasPanel2.SetActive(true);
        eventRef.SetSelectedGameObject(generalVolume);
    }
    public void Resume()
    {
        Time.timeScale = 1;
        CanvasPanel1.SetActive(false);
    }
}
