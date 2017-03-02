using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 

public class Muoviti : MonoBehaviour {
    public GameObject achivementGame;
    public Transform CurrentPos, StartPos, MainPos, Achievement, Crediti, Opzioni, Controlli, Volume, Records,
    AchivMontacarichiA, AchivDiscarica, AchivMontacarichiB, AchivTetto;
    public GameObject buttonStart;
    public GameObject button;
    public GameObject buttonAchievement;
    public GameObject buttonOptions;
    public GameObject buttonVolume;
    public GameObject buttonRecords;
    public List<int> ScoreMontacarichiA;
    public List<Button> ButtonsMontacarichiA;
    public List<string> textMontacarichiA;
    public List<int> ScoreDiscarica;
    public List<Button> ButtonsDiscarica;
    public List<string> textDiscarica;
    public List<int> ScoreMontacarichiB;
    public List<Button> ButtonsMontacarichiB;
    public List<string> textMontacarichiB;
    public List<int> ScoreTetto;
    public List<Button> ButtonsTetto;
    public List<string> textTetto;
    Achievement achievement;
    public Text txtMontacarichiA, txtDiscarica, txtMontacarichiB, txtTetto;
    public Text recordTotale, recordMontacarichiA, recordDiscarica, recordMontacarichiB, recordTetto;
    public static int scoreMontacarichiA, scoreDiscarica, scoreMontacarichiB, scoreTetto;
    public AudioSource backAudioRef;



    // Use this for initialization
    void Awake ()

    {
        achievement = FindObjectOfType<Achievement>();
        if (achievement == null)
        {
            Instantiate(achivementGame);
            achievement = FindObjectOfType<Achievement>();

        }

        

    }

    void Start()
    {
        /*if(achievement.firstLoad == false)
        {
            CurrentPos = MainPos;
        }*/

        SaveRecords();
        recordTotale.text = achievement.total.ToString();
        recordMontacarichiA.text = "Record : " + achievement.montacarichiA.ToString();
        recordDiscarica.text = "Record : " + achievement.discarica.ToString();
        recordMontacarichiB.text = "Record : " + achievement.montacarichiB.ToString();
        recordTetto.text = "Record : " + achievement.tetto.ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, CurrentPos.position, 0.05f);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, CurrentPos.rotation, 0.05f);

        if (Input.GetButtonDown("B"))
        {

            if (CurrentPos == StartPos)
                Debug.Log("Resto nel menu");

            else if (CurrentPos == MainPos)
            {
                CurrentPos = StartPos;
                GameObject myEventSystem = GameObject.Find("EventSystem");
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(buttonStart);
                backAudioRef.Play();
            }

            else if (CurrentPos == Achievement || CurrentPos == Crediti || CurrentPos == Opzioni)
            {
                CurrentPos = MainPos;
                GameObject myEventSystem = GameObject.Find("EventSystem");
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(button);
                backAudioRef.Play();
            }

            else if (CurrentPos == Volume || CurrentPos == Controlli || CurrentPos == Records)
            {
                CurrentPos = Opzioni;
                GameObject myEventSystem = GameObject.Find("EventSystem");
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(buttonOptions);
                backAudioRef.Play();
            }
                
            else if (CurrentPos == AchivMontacarichiA || CurrentPos == AchivDiscarica || CurrentPos == AchivMontacarichiB || AchivTetto)
            {
                txtMontacarichiA.text = "";
                txtDiscarica.text = "";
                txtMontacarichiB.text = "";
                txtTetto.text = "";
                CurrentPos = Achievement;
                GameObject myEventSystem = GameObject.Find("EventSystem");
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(buttonAchievement);
                backAudioRef.Play();
            }

            
             

        }

        if (Input.anyKey && CurrentPos == StartPos)
            Menu();


    }

    public void Quit()
    {
       Application.Quit();
    }

    public void SaveRecords()
    {
        if (achievement.montacarichiA > PlayerPrefs.GetInt("scoreMontacarichiA"))
            PlayerPrefs.SetInt("scoreMontacarichiA", achievement.montacarichiA);

        if (achievement.discarica > PlayerPrefs.GetInt("scoreDiscarica"))
            PlayerPrefs.SetInt("scoreDiscarica", achievement.discarica);

        if (achievement.montacarichiB > PlayerPrefs.GetInt("scoreMontacarichiB"))
            PlayerPrefs.SetInt("scoreMontacarichiB", achievement.montacarichiB);

        if (achievement.tetto > PlayerPrefs.GetInt("scoreTetto"))
            PlayerPrefs.SetInt("scoreTetto", achievement.tetto);

        if (achievement.total > PlayerPrefs.GetInt("scoreTotale"))
            PlayerPrefs.SetInt("scoreTotale", achievement.total);

        PlayerPrefs.Save();
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

    public void MenuRecords()

    {
        CurrentPos = Records;
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(buttonRecords);


    }

    public void Credits()

    {
        
        CurrentPos = Crediti;
     
    }

    public void VolumeOptions()
    {
        CurrentPos = Volume;
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(buttonVolume);
    }

    public void ControlliOptions()
    {
        CurrentPos = Controlli;
    }

    public void Options()

    {
        CurrentPos = Opzioni;
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(buttonOptions);

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
    public void SbloccoMontacarichiA_0()

    {
            ColorBlock cb = ButtonsMontacarichiA[0].colors;
            if (cb.normalColor == Color.white)
            {
                txtMontacarichiA.text = textMontacarichiA[0];
            }
   
    }
    public void SbloccoMontacarichiA_1()

    {
        ColorBlock cb = ButtonsMontacarichiA[1].colors;
        if (cb.normalColor == Color.white)
        {
            txtMontacarichiA.text = textMontacarichiA[1];
        }

    }
    public void SbloccoMontacarichiA_2()

    {
        ColorBlock cb = ButtonsMontacarichiA[2].colors;
        if (cb.normalColor == Color.white)
        {
            txtMontacarichiA.text = textMontacarichiA[2];
        }

    }

    public void AchievementDiscarica()

    {
        for (int i = 0; i < 3; i++)
        {
            if (achievement.discarica >= ScoreDiscarica[i])
            {
                ColorBlock cb = ButtonsDiscarica[i].colors;
                cb.normalColor = Color.white;
                ButtonsDiscarica[i].colors = cb;
                ColorBlock cb1 = ButtonsDiscarica[i].colors;
                cb1.highlightedColor = new Color(0, 33, 255, 255);
                ButtonsDiscarica[i].colors = cb1;
            }
        }


        CurrentPos = AchivDiscarica;
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(ButtonsDiscarica[0].gameObject);

    }
    public void SbloccoDiscarica_0()

    {
        ColorBlock cb = ButtonsDiscarica[0].colors;
        if (cb.normalColor == Color.white)
        {
            txtDiscarica.text = textDiscarica[0];
        }

    }
    public void SbloccoDiscarica_1()

    {
        ColorBlock cb = ButtonsDiscarica[1].colors;
        if (cb.normalColor == Color.white)
        {
            txtDiscarica.text = textDiscarica[1];
        }

    }
    public void SbloccoDiscarica_2()

    {
        ColorBlock cb = ButtonsDiscarica[2].colors;
        if (cb.normalColor == Color.white)
        {
            txtDiscarica.text = textDiscarica[2];
        }

    }

    public void AchievmentMontacarichiB()

    {
        for (int i = 0; i < 3; i++)
        {
            if (achievement.montacarichiB >= ScoreMontacarichiB[i])
            {
                ColorBlock cb = ButtonsMontacarichiB[i].colors;
                cb.normalColor = Color.white;
                ButtonsMontacarichiB[i].colors = cb;
                ColorBlock cb1 = ButtonsMontacarichiB[i].colors;
                cb1.highlightedColor = new Color(0, 33, 255, 255);
                ButtonsMontacarichiB[i].colors = cb1;
            }
        }


        CurrentPos = AchivMontacarichiB;
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(ButtonsMontacarichiB[0].gameObject);

    }
    public void SbloccoMontacarichiB_0()

    {
        ColorBlock cb = ButtonsMontacarichiB[0].colors;
        if (cb.normalColor == Color.white)
        {
            txtMontacarichiB.text = textMontacarichiB[0];
        }

    }
    public void SbloccoMontacarichiB_1()

    {
        ColorBlock cb = ButtonsMontacarichiB[1].colors;
        if (cb.normalColor == Color.white)
        {
            txtMontacarichiB.text = textMontacarichiB[1];
        }

    }
    public void SbloccoMontacarichiB_2()

    {
        ColorBlock cb = ButtonsMontacarichiB[2].colors;
        if (cb.normalColor == Color.white)
        {
            txtMontacarichiB.text = textMontacarichiB[2];
        }

    }

    public void AchievementTetto()

    {
        for (int i = 0; i < 3; i++)
        {
            if (achievement.tetto >= ScoreTetto[i])
            {
                ColorBlock cb = ButtonsTetto[i].colors;
                cb.normalColor = Color.white;
                ButtonsTetto[i].colors = cb;
                ColorBlock cb1 = ButtonsTetto[i].colors;
                cb1.highlightedColor = new Color(0, 33, 255, 255);
                ButtonsTetto[i].colors = cb1;
            }
        }


        CurrentPos = AchivTetto;
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(ButtonsTetto[0].gameObject);

    }
    public void SbloccoTetto_0()

    {
        ColorBlock cb = ButtonsTetto[0].colors;
        if (cb.normalColor == Color.white)
        {
            txtTetto.text = textTetto[0];
        }

    }
    public void SbloccoTetto_1()

    {
        ColorBlock cb = ButtonsTetto[1].colors;
        if (cb.normalColor == Color.white)
        {
            txtTetto.text = textTetto[1];
        }

    }
    public void SbloccoTetto_2()

    {
        ColorBlock cb = ButtonsTetto[2].colors;
        if (cb.normalColor == Color.white)
        {
            txtTetto.text = textTetto[2];
        }

    }

    public void DeleteRecord()
    {
        achievement.total = 0;
        achievement.montacarichiA = 0;
        achievement.discarica = 0;
        achievement.montacarichiB = 0;
        achievement.tetto = 0;

        PlayerPrefs.SetInt("scoreMontacarichiA", achievement.montacarichiA);
        PlayerPrefs.SetInt("scoreDiscarica", achievement.discarica);
        PlayerPrefs.SetInt("scoreMontacarichiB", achievement.montacarichiB);
        PlayerPrefs.SetInt("scoreTetto", achievement.tetto);
        PlayerPrefs.SetInt("scoreTotale", achievement.total);

        recordTotale.text = achievement.total.ToString();
        recordMontacarichiA.text = "Record : " + achievement.montacarichiA.ToString();
        recordDiscarica.text = "Record : " + achievement.discarica.ToString();
        recordMontacarichiB.text = "Record : " + achievement.montacarichiB.ToString();
        recordTetto.text = "Record : " + achievement.tetto.ToString();


        PlayerPrefs.Save();

        Options();
    }

    

    
}


