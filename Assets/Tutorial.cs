using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    Player player;
    FlyCamManager flyElements;
    public int step = 0;
    public GameObject nemico1;
    public GameObject nemico2;
    public GameObject nemico3;

    public string currentScene;
    Achievement achievement;

    bool tutorialMode = true;

    void Awake ()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Tutorial")
        {
            FindObjectOfType<FlyCamManager>().tutorial = true;
            flyElements = FindObjectOfType<FlyCamManager>();
            player = FindObjectOfType<Player>();
            player.tutorial = true;
            player.dashTutorial = true;
            player.stepTutorial = true;
            player.dashAttivo = false;
            player.noWeapons = true;
            player.tutorialMode = true;
        }
        achievement = FindObjectOfType<Achievement>();
	}

    public void StartTutorial()
    {
        if (currentScene == "Tutorial" && tutorialMode == true)
        {
            NextStep();
            tutorialMode = false;
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
            case 31:
                transform.GetChild(3).gameObject.SetActive(true);
                break;
            case 41:
                transform.GetChild(4).gameObject.SetActive(true);
                break;
            case 51:
                transform.GetChild(5).gameObject.SetActive(true);
                break;
            case 52:
                transform.GetChild(6).gameObject.SetActive(true);
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
            case 31:
                transform.GetChild(3).gameObject.SetActive(false);
                break;
            case 41:
                transform.GetChild(4).gameObject.SetActive(false);
                break;
            case 51:
                transform.GetChild(5).gameObject.SetActive(false);
                break;
            case 52:
                transform.GetChild(6).gameObject.SetActive(false);
                break;

        }
    }

    void Update ()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float rocket = Input.GetAxisRaw("RightTrigger");

        if (currentScene == "Tutorial")
        {
            
            if (step == 1 && (h > 0 || v > 0))
            {
                HideStep();
                player.tutorial = false;
                player.dashAttivo = true;
            }

            if (Input.GetButtonDown("Dash") && step == 2)
            {
                HideStep();
            }
            if (step == 3 && (player.rx > 0 || player.ry > 0)) 
            {
                HideStep();
                player.noWeapons = false;
                nemico1.gameObject.SetActive(true);
                Debug.Log("spawno il nemico");
                step = 30;
            }
            if (Input.GetButtonDown("Jump") && step == 31)
            {
                HideStep();
                nemico2.gameObject.SetActive(true);
                step = 40;
            }
            if (rocket > 0 && step == 41)
            {
                HideStep();
                nemico3.gameObject.SetActive(true);
                step = 50;
            }
            if (step == 30 && nemico1.gameObject == null)
            {
                NextStep();
            }
            if (step == 40 && nemico2.gameObject == null)
            {
                NextStep();
            }
            if (step == 50 && nemico3.gameObject == null)
            {
                
                NextStep();
            }

            if (step == 51 && Input.GetButtonDown("Fire1"))
            {
                HideStep();
                NextStep();
            }

            else if (Input.GetButtonDown("Fire1") && step == 52)
            {
                HideStep();
                int indexSC = SceneManager.GetActiveScene().buildIndex;
                achievement.tutorialComplete = true;
                SceneManager.LoadScene(indexSC + 1);
            }
        }
    }
}
