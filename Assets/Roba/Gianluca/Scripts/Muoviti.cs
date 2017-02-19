using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 

public class Muoviti : MonoBehaviour {
    public Transform CurrentPos, MainPos, MontacarichiPos, DiscaricaPos, TettoPos, FonderiaPos, PalazzoPos, Achievement,
        AchivMontacarichiA;
    public GameObject button;
    public GameObject buttonAchievement;
    public List<int> ScoreMontacarichiA;
    public List<Button> ButtonsMontacarichiA;
    Achievement achievement;



    // Use this for initialization
    void Start ()

    {
        achievement = FindObjectOfType<Achievement>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, CurrentPos.position, 0.05f);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, CurrentPos.rotation, 0.05f);
	
	}



    public void Menu()

    {
        CurrentPos = MainPos;
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(button);

    }

    public void Achievment()

    {
        CurrentPos = Achievement;
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(buttonAchievement);

    }

    public void AchievmentMontacarichiA()

    {
        for (int i = 0; i < 3; i++)
        {
            if (achievement.montacarichiA >= ScoreMontacarichiA[i])
            {
                ColorBlock cb = ButtonsMontacarichiA[i].colors;
                cb.normalColor = Color.white;
                ButtonsMontacarichiA[i].colors = cb;
                ColorBlock cb1 = ButtonsMontacarichiA[i].colors;
                cb1.highlightedColor = new Color(0, 33, 255, 255);
                ButtonsMontacarichiA[i].colors = cb1;
            }
        }
        
            
        CurrentPos = AchivMontacarichiA;
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(ButtonsMontacarichiA[0].gameObject);

    }

    
    public void Montacarichi()

    {
        CurrentPos = MontacarichiPos;

    }

    public void Discarica()

    {
        CurrentPos = DiscaricaPos;

    }

    public void Tetto()

    {
        CurrentPos = TettoPos;
   

    }

    public void Fonderia()

    {
        CurrentPos = FonderiaPos;

    }


   public void Palazzo()

    {
        CurrentPos = PalazzoPos;


    }

    
}


