using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

    Player player;
    FlyCamManager flyElements;
    int step = 0;

	void Awake ()
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

    void Start()
    {
        NextStep();
        flyElements.Skip();
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
        }

    }
}
