using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {

    Player player;
    FlyCamManager flyElements;
    int step = 0;
    public GameObject nemico1;
    public GameObject nemico2;
    public GameObject nemico3;

    string currentScene;

    void Awake ()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "TutorialProva")
        {
            FindObjectOfType<FlyCamManager>().tutorial = true;
            flyElements = FindObjectOfType<FlyCamManager>();
            player = FindObjectOfType<Player>();
            player.tutorial = true;
            player.stepTutorial = true;
            player.dashAttivo = false;
            player.noWeapons = true;
            player.tutorialMode = true;
        }

        
        
	}

    void Start()
    {
        if (currentScene == "TutorialProva")
        {
            NextStep();
            flyElements.Skip();
        }
    }
	
    public void NextStep()
    {
        step++;
        switch (step)
        {
            case 1:
                transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 2:
                transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 3:
                transform.GetChild(2).gameObject.SetActive(true);
                
                break;
            case 4:
                transform.GetChild(3).gameObject.SetActive(true);
                break;
            case 5:
                transform.GetChild(4).gameObject.SetActive(true);
                break;
            case 6:
                transform.GetChild(5).gameObject.SetActive(true);
                break;

        }
    }

    public void HideStep()
    {
        switch (step)
        {
            case 1:
                transform.GetChild(0).gameObject.SetActive(false);
                break;
            case 2:
                transform.GetChild(1).gameObject.SetActive(false);
                break;
            case 3:
                transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 4:
                transform.GetChild(3).gameObject.SetActive(false);
                break;
            case 5:
                transform.GetChild(4).gameObject.SetActive(false);
                break;
            case 6:
                transform.GetChild(5).gameObject.SetActive(false);
                break;

        }
    }

    void Update ()
    {
	    if (Input.GetButtonDown("Fire1") && step == 1 )
        {
            HideStep();
            player.tutorial = false;
        }

        if (Input.GetButtonDown("Fire1") && step == 2)
        {
            HideStep();
            player.dashAttivo = true;
        }
        if (Input.GetButtonDown("Fire1") && step == 3)
        {
            HideStep();
            player.noWeapons = false;
            nemico1.gameObject.SetActive(true);
        }

   
        if (Input.GetButtonDown("Next Weapon") && step == 4)
        {
            HideStep();
            
            nemico2.gameObject.SetActive(true);
        }

        if (Input.GetButtonDown("Previous Weapon") && step == 5)
        {
            HideStep();
            
            nemico3.gameObject.SetActive(true);
        }

        if (step == 3 && nemico1.gameObject == null)
        {
            NextStep();
        }

        if (step == 4 && nemico2.gameObject == null)
        {
            NextStep();
        }

        if (step == 5 && nemico3.gameObject == null)
        {
            NextStep();
        }

        if (Input.GetButtonDown("Fire1") && step == 6)
        {
            HideStep();

            int indexSC = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(indexSC + 1);

        }
    }
}
