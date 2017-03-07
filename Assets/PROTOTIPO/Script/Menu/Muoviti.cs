using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Muoviti : MonoBehaviour
{
    public GameObject achivementGame;
    public Transform CurrentPos, StartPos, MainPos, Achievement, Crediti, Opzioni, Controlli, Volume, Records,
    AchivMontacarichiA, AchivDiscarica, AchivMontacarichiB, AchivTetto;
    public GameObject buttonStart;
    public GameObject button;
    public GameObject buttonAchievement;
    public GameObject AchievementButton;
    public List<Button> ButtonsAchievement;
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
    public List<Button> MainMenuButtons;
    public List<string> textTetto;
    Achievement achievement;
    public Text txtMontacarichiA, txtDiscarica, txtMontacarichiB, txtTetto;
    public Text recordTotale;
    public static int scoreMontacarichiA, scoreDiscarica, scoreMontacarichiB, scoreTetto;
    public AudioSource backAudioRef;

    public AudioSource audioBlock;
    public AudioSource audioSelect;

    int sbloccoMontacarichi = 0;
    int sbloccoDiscarica = 0;
    int sbloccoAscensore = 0;
    int sbloccoTetto = 0;



    // Use this for initialization
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        achievement = FindObjectOfType<Achievement>();
        if (achievement == null)
        {
            Instantiate(achivementGame);
            achievement = FindObjectOfType<Achievement>();
        }
    }

    void Start()
    {
        sbloccoMontacarichi = PlayerPrefs.GetInt("sbloccoMontacarichi", sbloccoMontacarichi);
        sbloccoDiscarica = PlayerPrefs.GetInt("sbloccoDiscarica", sbloccoDiscarica);
        sbloccoAscensore = PlayerPrefs.GetInt("sbloccoAscensore", sbloccoAscensore);
        sbloccoTetto = PlayerPrefs.GetInt("sbloccoTetto", sbloccoTetto);
        /*if(achievement.firstLoad == false)
        {
            CurrentPos = MainPos;
        }*/

        SaveRecords();

        LevelBlock();

        for (int i = 0; i < 3; i++)
        {
            ButtonsMontacarichiA[i].transform.GetChild(0).GetComponent<Text>().text = "Per sbloccare : " + ScoreMontacarichiA[i] + " SP";
            ButtonsDiscarica[i].transform.GetChild(0).GetComponent<Text>().text = "Per sbloccare : " + ScoreDiscarica[i] + " SP";
            ButtonsMontacarichiB[i].transform.GetChild(0).GetComponent<Text>().text = "Per sbloccare : " + ScoreMontacarichiB[i] + " SP";
            ButtonsTetto[i].transform.GetChild(0).GetComponent<Text>().text = "Per sbloccare : " + ScoreTetto[i] + " SP";
        }

        UpdateAchievement();
    }

    void UnlockAll()
    {
        achievement.sbloccoMontacarichi = 0;
        achievement.sbloccoDiscarica = 0;
        achievement.sbloccoAscensore = 0;
        achievement.sbloccoTetto = 0;

        achievement.achievementMontacarichi_1 = 1;
        achievement.achievementMontacarichi_2 = 1;
        achievement.achievementMontacarichi_3 = 1;

        achievement.achievementDiscarica_1 = 1;
        achievement.achievementDiscarica_2 = 1;
        achievement.achievementDiscarica_3 = 1;

        achievement.achievementAscensore_1 = 1;
        achievement.achievementAscensore_2 = 1;
        achievement.achievementAscensore_3 = 1;

        achievement.achievementTetto_1 = 1;
        achievement.achievementTetto_2 = 1;
        achievement.achievementTetto_3 = 1;

        achievement.sbloccoMontacarichi = 1;
        achievement.sbloccoDiscarica = 1;
        achievement.sbloccoAscensore = 1;
        achievement.sbloccoTetto = 1;

        achievement.total = 1000000;
        achievement.montacarichiA = 1000000;
        achievement.discarica = 1000000;
        achievement.montacarichiB = 1000000;
        achievement.tetto = 1000000;


        PlayerPrefs.SetInt("sbloccoMontacarichiUI", sbloccoMontacarichi);
        PlayerPrefs.SetInt("sbloccoDiscaricaUI", sbloccoDiscarica);
        PlayerPrefs.SetInt("sbloccoAscensoreUI", sbloccoAscensore);
        PlayerPrefs.SetInt("sbloccoTettoUI", sbloccoTetto);

        PlayerPrefs.SetInt("achievementMontacarichi_1", achievement.achievementMontacarichi_1);
        PlayerPrefs.SetInt("achievementMontacarichi_2", achievement.achievementMontacarichi_2);
        PlayerPrefs.SetInt("achievementMontacarichi_3", achievement.achievementMontacarichi_3);

        PlayerPrefs.SetInt("achievementDiscarica_1", achievement.achievementDiscarica_1);
        PlayerPrefs.SetInt("achievementDiscarica_2", achievement.achievementDiscarica_2);
        PlayerPrefs.SetInt("achievementDiscarica_3", achievement.achievementDiscarica_3);

        PlayerPrefs.SetInt("achievementAscensore_1", achievement.achievementAscensore_1);
        PlayerPrefs.SetInt("achievementAscensore_2", achievement.achievementAscensore_2);
        PlayerPrefs.SetInt("achievementAscensore_3", achievement.achievementAscensore_3);

        PlayerPrefs.SetInt("achievementTetto_1", achievement.achievementTetto_1);
        PlayerPrefs.SetInt("achievementTetto_2", achievement.achievementTetto_2);
        PlayerPrefs.SetInt("achievementTetto_3", achievement.achievementTetto_3);

        PlayerPrefs.SetInt("scoreTotale", achievement.total);
        PlayerPrefs.SetInt("scoreMontacarichiA", achievement.montacarichiA);
        PlayerPrefs.SetInt("scoreDiscarica", achievement.discarica);
        PlayerPrefs.SetInt("scoreMontacarichiB", achievement.montacarichiB);
        PlayerPrefs.SetInt("scoreTetto", achievement.tetto);


        PlayerPrefs.Save();

        LevelBlock();

        UpdateAchievement();
    }


    void UpdateBlockAchievement()
    {
        for (int i = 0; i < 3; i++)
        {
            ButtonsMontacarichiA[i].transform.GetChild(0).GetComponent<Text>().text = "Per sbloccare : " + ScoreMontacarichiA[i] + " SP";
            ButtonsDiscarica[i].transform.GetChild(0).GetComponent<Text>().text = "Per sbloccare : " + ScoreDiscarica[i] + " SP";
            ButtonsMontacarichiB[i].transform.GetChild(0).GetComponent<Text>().text = "Per sbloccare : " + ScoreMontacarichiB[i] + " SP";
            ButtonsTetto[i].transform.GetChild(0).GetComponent<Text>().text = "Per sbloccare : " + ScoreTetto[i] + " SP";
        }
    }

    void LevelBlock()
    {
        if (achievement.montacarichiA >= ScoreDiscarica[3])
        {
            MainMenuButtons[1].transform.GetChild(0).GetComponent<Text>().text = "Discarica";
            ColorBlock cb = MainMenuButtons[1].colors;
            cb.normalColor = Color.white;
            MainMenuButtons[1].colors = cb;

            if (sbloccoDiscarica == 0)
            {
                sbloccoDiscarica = 1;
                MainMenuButtons[1].transform.GetChild(1).gameObject.SetActive(true);
                PlayerPrefs.SetInt("sbloccoDiscarica", sbloccoDiscarica);
                PlayerPrefs.Save();
            }
        }

        else
        {
            MainMenuButtons[1].transform.GetChild(0).GetComponent<Text>().text = "Per sbloccare : " + ScoreDiscarica[3] + " SP";
        }

        if (achievement.discarica >= ScoreMontacarichiB[3])
        {
            MainMenuButtons[2].transform.GetChild(0).GetComponent<Text>().text = "Ascensore";
            ColorBlock cb = MainMenuButtons[2].colors;
            cb.normalColor = Color.white;
            MainMenuButtons[2].colors = cb;

            if (sbloccoAscensore == 0)
            {
                sbloccoAscensore = 1;
                MainMenuButtons[2].transform.GetChild(1).gameObject.SetActive(true);
                PlayerPrefs.SetInt("sbloccoAscensore", sbloccoAscensore);
                PlayerPrefs.Save();
            }

        }

        else
        {
            MainMenuButtons[2].transform.GetChild(0).GetComponent<Text>().text = "Per sbloccare : " + ScoreMontacarichiB[3] + " SP";
        }

        if (achievement.montacarichiB >= ScoreTetto[3])
        {
            MainMenuButtons[3].transform.GetChild(0).GetComponent<Text>().text = "Tetto";
            ColorBlock cb = MainMenuButtons[3].colors;
            cb.normalColor = Color.white;
            MainMenuButtons[3].colors = cb;

            if (sbloccoTetto == 0)
            {
                sbloccoTetto = 1;
                MainMenuButtons[3].transform.GetChild(1).gameObject.SetActive(true);
                PlayerPrefs.SetInt("sbloccoTetto", sbloccoTetto);
                PlayerPrefs.Save();
            }
        }

        else
        {
            MainMenuButtons[3].transform.GetChild(0).GetComponent<Text>().text = "Per sbloccare : " + ScoreTetto[3] + " SP";
        }
    }

    void UpdateAchievement()
    {
        if (achievement.achievementMontacarichi_1 == 1 || achievement.achievementMontacarichi_2 == 1 || achievement.achievementMontacarichi_3 == 1)
        {
            AchievementButton.transform.GetChild(0).gameObject.SetActive(true);
            ButtonsAchievement[0].transform.GetChild(0).gameObject.SetActive(true);

            if (achievement.achievementMontacarichi_1 == 1)
                ButtonsMontacarichiA[0].transform.GetChild(1).gameObject.SetActive(true);
            if (achievement.achievementMontacarichi_2 == 1)
                ButtonsMontacarichiA[1].transform.GetChild(1).gameObject.SetActive(true);
            if (achievement.achievementMontacarichi_3 == 1)
                ButtonsMontacarichiA[2].transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            ButtonsAchievement[0].transform.GetChild(0).gameObject.SetActive(false);
        }
        if (achievement.achievementDiscarica_1 == 1 || achievement.achievementDiscarica_2 == 1 || achievement.achievementDiscarica_3 == 1)
        {
            AchievementButton.transform.GetChild(0).gameObject.SetActive(true);
            ButtonsAchievement[1].transform.GetChild(0).gameObject.SetActive(true);

            if (achievement.achievementDiscarica_1 == 1)
                ButtonsDiscarica[0].transform.GetChild(1).gameObject.SetActive(true);
            if (achievement.achievementDiscarica_2 == 1)
                ButtonsDiscarica[1].transform.GetChild(1).gameObject.SetActive(true);
            if (achievement.achievementDiscarica_3 == 1)
                ButtonsDiscarica[2].transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            ButtonsAchievement[1].transform.GetChild(0).gameObject.SetActive(false);
        }

        if (achievement.achievementAscensore_1 == 1 || achievement.achievementAscensore_2 == 1 || achievement.achievementAscensore_3 == 1)
        {
            AchievementButton.transform.GetChild(0).gameObject.SetActive(true);
            ButtonsAchievement[2].transform.GetChild(0).gameObject.SetActive(true);

            if (achievement.achievementAscensore_1 == 1)
                ButtonsMontacarichiB[0].transform.GetChild(1).gameObject.SetActive(true);
            if (achievement.achievementAscensore_2 == 1)
                ButtonsMontacarichiB[1].transform.GetChild(1).gameObject.SetActive(true);
            if (achievement.achievementAscensore_3 == 1)
                ButtonsMontacarichiB[2].transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            ButtonsAchievement[2].transform.GetChild(0).gameObject.SetActive(false);
        }

        if (achievement.achievementTetto_1 == 1 || achievement.achievementTetto_2 == 1 || achievement.achievementTetto_3 == 1)
        {
            AchievementButton.transform.GetChild(0).gameObject.SetActive(true);
            ButtonsAchievement[3].transform.GetChild(0).gameObject.SetActive(true);

            if (achievement.achievementTetto_1 == 1)
                ButtonsTetto[0].transform.GetChild(1).gameObject.SetActive(true);
            if (achievement.achievementTetto_2 == 1)
                ButtonsTetto[1].transform.GetChild(1).gameObject.SetActive(true);
            if (achievement.achievementTetto_3 == 1)
                ButtonsTetto[2].transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            ButtonsAchievement[3].transform.GetChild(0).gameObject.SetActive(false);
        }

        if (achievement.achievementMontacarichi_1 != 1 && achievement.achievementMontacarichi_2 != 1 && achievement.achievementMontacarichi_3 != 1
            && achievement.achievementDiscarica_1 != 1 && achievement.achievementDiscarica_2 != 1 && achievement.achievementDiscarica_3 != 1
            && achievement.achievementAscensore_1 != 1 && achievement.achievementAscensore_2 != 1 && achievement.achievementAscensore_3 != 1
            && achievement.achievementTetto_1 != 1 && achievement.achievementTetto_2 != 1 && achievement.achievementTetto_3 != 1)
        {
            AchievementButton.transform.GetChild(0).gameObject.SetActive(false);
        }
            
    }

    // Update is called once per frame
    void Update()
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


        if (Input.GetButtonDown("X") && Input.GetButtonDown("Previous Weapon"))
        {
            UnlockAll();
        }


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
                if (i == 0)
                    ButtonsMontacarichiA[0].transform.GetChild(0).GetComponent<Text>().text = "Inizio";
                else if (i == 1)
                    ButtonsMontacarichiA[1].transform.GetChild(0).GetComponent<Text>().text = "Intermezzo";
                else if (i == 2)
                    ButtonsMontacarichiA[2].transform.GetChild(0).GetComponent<Text>().text = "Fine";

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
            if (achievement.achievementMontacarichi_1 == 1)
            {
                achievement.achievementMontacarichi_1 = 2;
                ButtonsMontacarichiA[0].transform.GetChild(1).gameObject.SetActive(false);
                PlayerPrefs.SetInt("achievementMontacarichi_1", achievement.achievementMontacarichi_1);
                PlayerPrefs.Save();
                UpdateAchievement();
            }

        }

    }

    public void SbloccoAudioMontacarichiA_0()

    {
        ColorBlock cb = ButtonsMontacarichiA[0].colors;
        if (cb.normalColor != Color.white)
            audioBlock.Play();
        else
            audioSelect.Play();
    }

    public void SbloccoMontacarichiA_1()

    {
        ColorBlock cb = ButtonsMontacarichiA[1].colors;
        if (cb.normalColor == Color.white)
        {
            txtMontacarichiA.text = textMontacarichiA[1];
            if (achievement.achievementMontacarichi_2 == 1)
            {
                achievement.achievementMontacarichi_2 = 2;
                ButtonsMontacarichiA[1].transform.GetChild(1).gameObject.SetActive(false);
                PlayerPrefs.SetInt("achievementMontacarichi_2", achievement.achievementMontacarichi_2);
                PlayerPrefs.Save();
                UpdateAchievement();
            }

        }

    }

    public void SbloccoAudioMontacarichiA_1()

    {
        ColorBlock cb = ButtonsMontacarichiA[1].colors;
        if (cb.normalColor != Color.white)
            audioBlock.Play();
        else
            audioSelect.Play();
    }

    public void SbloccoMontacarichiA_2()

    {
        ColorBlock cb = ButtonsMontacarichiA[2].colors;
        if (cb.normalColor == Color.white)
        {
            txtMontacarichiA.text = textMontacarichiA[2];
            if (achievement.achievementMontacarichi_3 == 1)
            {
                achievement.achievementMontacarichi_3 = 2;
                ButtonsMontacarichiA[2].transform.GetChild(1).gameObject.SetActive(false);
                PlayerPrefs.SetInt("achievementMontacarichi_3", achievement.achievementMontacarichi_3);
                PlayerPrefs.Save();
                UpdateAchievement();
            }
            
        }

    }

    public void SbloccoAudioMontacarichiA_2()

    {
        ColorBlock cb = ButtonsMontacarichiA[2].colors;
        if (cb.normalColor != Color.white)
            audioBlock.Play();
        else
            audioSelect.Play();
    }

    public void AchievementDiscarica()

    {
        
            for (int i = 0; i < 3; i++)
            {
            if (achievement.discarica >= ScoreDiscarica[i])
            {
                if (i == 0)
                    ButtonsDiscarica[0].transform.GetChild(0).GetComponent<Text>().text = "Inizio";
                else if (i == 1)
                    ButtonsDiscarica[1].transform.GetChild(0).GetComponent<Text>().text = "Intermezzo";
                else if (i == 2)
                    ButtonsDiscarica[2].transform.GetChild(0).GetComponent<Text>().text = "Fine";

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
            if (achievement.achievementDiscarica_1 == 1)
            {
                achievement.achievementDiscarica_1 = 2;
                ButtonsDiscarica[0].transform.GetChild(1).gameObject.SetActive(false);
                PlayerPrefs.SetInt("achievementDiscarica_1", achievement.achievementDiscarica_1);
                PlayerPrefs.Save();
                UpdateAchievement();
            }
        }

    }

    public void SbloccoAudioDiscarica_0()

    {
        ColorBlock cb = ButtonsDiscarica[0].colors;
        if (cb.normalColor != Color.white)
            audioBlock.Play();
        else
            audioSelect.Play();
    }

    public void SbloccoDiscarica_1()

    {
        ColorBlock cb = ButtonsDiscarica[1].colors;
        if (cb.normalColor == Color.white)
        {
            txtDiscarica.text = textDiscarica[1];
            if (achievement.achievementDiscarica_2 == 1)
            {
                achievement.achievementDiscarica_2 = 2;
                ButtonsDiscarica[1].transform.GetChild(1).gameObject.SetActive(false);
                PlayerPrefs.SetInt("achievementDiscarica_2", achievement.achievementDiscarica_2);
                PlayerPrefs.Save();
                UpdateAchievement();
            }
        }

    }

    public void SbloccoAudioDiscarica_1()

    {
        ColorBlock cb = ButtonsDiscarica[1].colors;
        if (cb.normalColor != Color.white)
            audioBlock.Play();
        else
            audioSelect.Play();
    }

    public void SbloccoDiscarica_2()

    {
        ColorBlock cb = ButtonsDiscarica[2].colors;
        if (cb.normalColor == Color.white)
        {
            txtDiscarica.text = textDiscarica[2];
            if (achievement.achievementDiscarica_3 == 1)
            {
                achievement.achievementDiscarica_3 = 2;
                ButtonsDiscarica[2].transform.GetChild(1).gameObject.SetActive(false);
                PlayerPrefs.SetInt("achievementDiscarica_3", achievement.achievementDiscarica_3);
                PlayerPrefs.Save();
                UpdateAchievement();
            }
        }

    }

    public void SbloccoAudioDiscarica_2()

    {
        ColorBlock cb = ButtonsDiscarica[2].colors;
        if (cb.normalColor != Color.white)
            audioBlock.Play();
        else
            audioSelect.Play();
    }

    public void AchievmentMontacarichiB()

    {
        for (int i = 0; i < 3; i++)
        {
            if (achievement.montacarichiB >= ScoreMontacarichiB[i])
            {
                if (i == 0)
                    ButtonsMontacarichiB[0].transform.GetChild(0).GetComponent<Text>().text = "Inizio";
                else if (i == 1)
                    ButtonsMontacarichiB[1].transform.GetChild(0).GetComponent<Text>().text = "Intermezzo";
                else if (i == 2)
                    ButtonsMontacarichiB[2].transform.GetChild(0).GetComponent<Text>().text = "Fine";

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

            if (achievement.achievementAscensore_1 == 1)
            {
                achievement.achievementAscensore_1 = 2;
                ButtonsMontacarichiB[0].transform.GetChild(1).gameObject.SetActive(false);
                PlayerPrefs.SetInt("achievementAscensore_1", achievement.achievementAscensore_1);
                PlayerPrefs.Save();
                UpdateAchievement();
            }
        }

    }

    public void SbloccoAudioMontacarichiB_0()

    {
        ColorBlock cb = ButtonsMontacarichiB[0].colors;
        if (cb.normalColor != Color.white)
            audioBlock.Play();
        else
            audioSelect.Play();
    }

    public void SbloccoMontacarichiB_1()

    {
        ColorBlock cb = ButtonsMontacarichiB[1].colors;
        if (cb.normalColor == Color.white)
        {
            txtMontacarichiB.text = textMontacarichiB[1];

            if (achievement.achievementAscensore_2 == 1)
            {
                achievement.achievementAscensore_2 = 2;
                ButtonsMontacarichiB[1].transform.GetChild(1).gameObject.SetActive(false);
                PlayerPrefs.SetInt("achievementAscensore_2", achievement.achievementAscensore_2);
                PlayerPrefs.Save();
                UpdateAchievement();
            }
        }

    }

    public void SbloccoAudioMontacarichiB_1()

    {
        ColorBlock cb = ButtonsMontacarichiB[1].colors;
        if (cb.normalColor != Color.white)
            audioBlock.Play();
        else
            audioSelect.Play();
    }

    public void SbloccoMontacarichiB_2()

    {
        ColorBlock cb = ButtonsMontacarichiB[2].colors;
        if (cb.normalColor == Color.white)
        {
            txtMontacarichiB.text = textMontacarichiB[2];

            if (achievement.achievementAscensore_3 == 1)
            {
                achievement.achievementAscensore_3 = 2;
                ButtonsMontacarichiB[2].transform.GetChild(1).gameObject.SetActive(false);
                PlayerPrefs.SetInt("achievementAscensore_3", achievement.achievementAscensore_3);
                PlayerPrefs.Save();
                UpdateAchievement();
            }
        }

    }

    public void SbloccoAudioMontacarichiB_2()

    {
        ColorBlock cb = ButtonsMontacarichiB[2].colors;
        if (cb.normalColor != Color.white)
            audioBlock.Play();
        else
            audioSelect.Play();
    }

    public void AchievementTetto()

    {
        for (int i = 0; i < 3; i++)
        {
            if (achievement.tetto >= ScoreTetto[i])
            {
                if (i == 0)
                    ButtonsTetto[0].transform.GetChild(0).GetComponent<Text>().text = "Inizio";
                else if (i == 1)
                    ButtonsTetto[1].transform.GetChild(0).GetComponent<Text>().text = "Intermezzo";
                else if (i == 2)
                    ButtonsTetto[2].transform.GetChild(0).GetComponent<Text>().text = "Fine";

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

            if (achievement.achievementTetto_1 == 1)
            {
                achievement.achievementTetto_1 = 2;
                ButtonsTetto[0].transform.GetChild(1).gameObject.SetActive(false);
                PlayerPrefs.SetInt("achievementTetto_1", achievement.achievementTetto_1);
                PlayerPrefs.Save();
                UpdateAchievement();
            }
        }

    }

    public void SbloccoAudioTetto_0()

    {
        ColorBlock cb = ButtonsTetto[0].colors;
        if (cb.normalColor != Color.white)
            audioBlock.Play();
        else
            audioSelect.Play();
    }

    public void SbloccoTetto_1()

    {
        ColorBlock cb = ButtonsTetto[1].colors;
        if (cb.normalColor == Color.white)
        {
            txtTetto.text = textTetto[1];

            if (achievement.achievementTetto_2 == 1)
            {
                achievement.achievementTetto_2 = 2;
                ButtonsTetto[1].transform.GetChild(1).gameObject.SetActive(false);
                PlayerPrefs.SetInt("achievementTetto_2", achievement.achievementTetto_2);
                PlayerPrefs.Save();
                UpdateAchievement();
            }
        }

    }

    public void SbloccoAudioTetto_1()

    {
        ColorBlock cb = ButtonsTetto[1].colors;
        if (cb.normalColor != Color.white)
            audioBlock.Play();
        else
            audioSelect.Play();
    }

    public void SbloccoTetto_2()

    {
        ColorBlock cb = ButtonsTetto[2].colors;
        if (cb.normalColor == Color.white)
        {
            txtTetto.text = textTetto[2];

            if (achievement.achievementTetto_3 == 1)
            {
                achievement.achievementTetto_3 = 2;
                ButtonsTetto[2].transform.GetChild(1).gameObject.SetActive(false);
                PlayerPrefs.SetInt("achievementTetto_3", achievement.achievementTetto_3);
                PlayerPrefs.Save();
                UpdateAchievement();
            }
        }

    }

    public void SbloccoAudioTetto_2()

    {
        ColorBlock cb = ButtonsTetto[2].colors;
        if (cb.normalColor != Color.white)
            audioBlock.Play();
        else
            audioSelect.Play();
    }

    public void DeleteRecord()
    {
        achievement.total = 0;
        achievement.montacarichiA = 0;
        achievement.discarica = 0;
        achievement.montacarichiB = 0;
        achievement.tetto = 0;
        sbloccoMontacarichi = 0;
        sbloccoDiscarica = 0;
        sbloccoAscensore = 0;
        sbloccoTetto = 0;

        achievement.sbloccoMontacarichi = 0;
        achievement.sbloccoDiscarica = 0;
        achievement.sbloccoAscensore = 0;
        achievement.sbloccoTetto = 0;

        achievement.achievementMontacarichi_1 = 0;
        achievement.achievementMontacarichi_2 = 0;
        achievement.achievementMontacarichi_3 = 0;

        achievement.achievementDiscarica_1 = 0;
        achievement.achievementDiscarica_2 = 0;
        achievement.achievementDiscarica_3 = 0;

        achievement.achievementAscensore_1 = 0;
        achievement.achievementAscensore_2 = 0;
        achievement.achievementAscensore_3 = 0;

        achievement.achievementTetto_1 = 0;
        achievement.achievementTetto_2 = 0;
        achievement.achievementTetto_3 = 0;

        PlayerPrefs.SetInt("scoreMontacarichiA", achievement.montacarichiA);
        PlayerPrefs.SetInt("scoreDiscarica", achievement.discarica);
        PlayerPrefs.SetInt("scoreMontacarichiB", achievement.montacarichiB);
        PlayerPrefs.SetInt("scoreTetto", achievement.tetto);
        PlayerPrefs.SetInt("scoreTotale", achievement.total);

        PlayerPrefs.SetInt("sbloccoMontacarichi", sbloccoMontacarichi);
        PlayerPrefs.SetInt("sbloccoDiscarica", sbloccoDiscarica);
        PlayerPrefs.SetInt("sbloccoAscensore", sbloccoAscensore);
        PlayerPrefs.SetInt("sbloccoTetto", sbloccoTetto);

        PlayerPrefs.SetInt("sbloccoMontacarichiUI", achievement.sbloccoMontacarichi);
        PlayerPrefs.SetInt("sbloccoDiscaricaUI", achievement.sbloccoDiscarica);
        PlayerPrefs.SetInt("sbloccoAscensoreUI", achievement.sbloccoAscensore);
        PlayerPrefs.SetInt("sbloccoTettoUI", achievement.sbloccoTetto);

        PlayerPrefs.SetInt("achievementMontacarichi_1", achievement.achievementMontacarichi_1);
        PlayerPrefs.SetInt("achievementMontacarichi_2", achievement.achievementMontacarichi_2);
        PlayerPrefs.SetInt("achievementMontacarichi_3", achievement.achievementMontacarichi_3);

        PlayerPrefs.SetInt("achievementDiscarica_1", achievement.achievementDiscarica_1);
        PlayerPrefs.SetInt("achievementDiscarica_2", achievement.achievementDiscarica_2);
        PlayerPrefs.SetInt("achievementDiscarica_3", achievement.achievementDiscarica_3);

        PlayerPrefs.SetInt("achievementAscensore_1", achievement.achievementAscensore_1);
        PlayerPrefs.SetInt("achievementAscensore_2", achievement.achievementAscensore_2);
        PlayerPrefs.SetInt("achievementAscensore_3", achievement.achievementAscensore_3);

        PlayerPrefs.SetInt("achievementTetto_1", achievement.achievementTetto_1);
        PlayerPrefs.SetInt("achievementTetto_2", achievement.achievementTetto_2);
        PlayerPrefs.SetInt("achievementTetto_3", achievement.achievementTetto_3);



        PlayerPrefs.Save();

        Options();
        UpdateAchievement();

        for (int i = 1; i < 4; i++)
        {
            MainMenuButtons[i].transform.GetChild(1).gameObject.SetActive(false);

            ColorBlock cb = MainMenuButtons[i].colors;
            cb.normalColor = Color.red;
            MainMenuButtons[i].colors = cb;
        }


        MainMenuButtons[1].transform.GetChild(0).GetComponent<Text>().text = "Per sbloccare : " + ScoreMontacarichiA[3] + " SP";
        MainMenuButtons[2].transform.GetChild(0).GetComponent<Text>().text = "Per sbloccare : " + ScoreDiscarica[3] + " SP";
        MainMenuButtons[3].transform.GetChild(0).GetComponent<Text>().text = "Per sbloccare : " + ScoreMontacarichiB[3] + " SP";

        UpdateBlockAchievement();
    }

    public void SetScoreMontacarichiA()
    {
        recordTotale.text = achievement.montacarichiA.ToString();
        ColorBlock cb = MainMenuButtons[0].colors;
   
         
    }

    public void SetScoreDiscarica()
    {
        recordTotale.text = achievement.discarica.ToString();
        ColorBlock cb = MainMenuButtons[1].colors;
 
    }

    public void SetScoreMontacarichiB()
    {
        recordTotale.text = achievement.montacarichiB.ToString();
        ColorBlock cb = MainMenuButtons[2].colors;
    }

    public void SetScoreTetto()
    {
        recordTotale.text = achievement.tetto.ToString();
        ColorBlock cb = MainMenuButtons[3].colors;
    }

    public void ResetScore()
    {
        recordTotale.text = "";
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Intro");
    }

    public void Montacarichi1()
    {
        ColorBlock cb = MainMenuButtons[0].colors;
        if (cb.normalColor == Color.white)
        {
            SceneManager.LoadScene("Intro_Montacarichi1");
        }


    }

    public void Discarica()
    {
        ColorBlock cb = MainMenuButtons[1].colors;
        if (cb.normalColor == Color.white)
        {
            audioSelect.Play();
            SceneManager.LoadScene("Intro_Discarica");
        }
        else
            audioBlock.Play();



    }

    public void Montacarichi2()
    {
        ColorBlock cb = MainMenuButtons[2].colors;
        if (cb.normalColor == Color.white)
        {
            audioSelect.Play();
            SceneManager.LoadScene("Intro_Montacarichi2");
        }
        else
            audioBlock.Play();


    }

    public void Tetto()
    {
        ColorBlock cb = MainMenuButtons[3].colors;
        if (cb.normalColor == Color.white)
        {
            audioSelect.Play();
            SceneManager.LoadScene("Intro_Tetto");
        }
        else
            audioBlock.Play();


    }


}


