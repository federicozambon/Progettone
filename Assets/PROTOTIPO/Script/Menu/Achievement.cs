using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{

    public int total = 0;
    public int tutorial = 0;
    public int montacarichiA = 0;
    public int discarica = 0;
    public int montacarichiB = 0;
    public int tetto = 0;
    public bool firstLoad = true;
    Muoviti muovitiElements;
    public string currentScene;
    public bool tutorialComplete = false;

    public int sbloccoMontacarichi = 0;
    public int achievementMontacarichi_1 = 0;
    public int achievementMontacarichi_2 = 0;
    public int achievementMontacarichi_3 = 0;

    public int sbloccoDiscarica = 0;
    public int achievementDiscarica_1 = 0;
    public int achievementDiscarica_2 = 0;
    public int achievementDiscarica_3 = 0;

    public int sbloccoAscensore = 0;
    public int achievementAscensore_1 = 0;
    public int achievementAscensore_2 = 0;
    public int achievementAscensore_3 = 0;

    public int sbloccoTetto = 0;
    public int achievementTetto_1 = 0;
    public int achievementTetto_2 = 0;
    public int achievementTetto_3 = 0;


    void Awake()
    {

        DontDestroyOnLoad(this.gameObject);
        muovitiElements = FindObjectOfType<Muoviti>();

        if (firstLoad == true)
        {
            total = PlayerPrefs.GetInt("scoreTotale", total);
            montacarichiA = PlayerPrefs.GetInt("scoreMontacarichiA", montacarichiA);
            discarica = PlayerPrefs.GetInt("scoreDiscarica", discarica);
            montacarichiB = PlayerPrefs.GetInt("scoreMontacarichiB", montacarichiB);
            tetto = PlayerPrefs.GetInt("scoreTetto", tetto);

            sbloccoMontacarichi = PlayerPrefs.GetInt("sbloccoMontacarichiUI", sbloccoMontacarichi);
            sbloccoDiscarica = PlayerPrefs.GetInt("sbloccoDiscaricaUI", sbloccoDiscarica);
            sbloccoAscensore = PlayerPrefs.GetInt("sbloccoAscensoreUI", sbloccoAscensore);
            sbloccoTetto = PlayerPrefs.GetInt("sbloccoTettoUI", sbloccoTetto);

            achievementMontacarichi_1 = PlayerPrefs.GetInt("achievementMontacarichi_1", achievementMontacarichi_1);
            achievementMontacarichi_2 = PlayerPrefs.GetInt("achievementMontacarichi_2", achievementMontacarichi_2);
            achievementMontacarichi_3 = PlayerPrefs.GetInt("achievementMontacarichi_3", achievementMontacarichi_3);

            achievementDiscarica_1 = PlayerPrefs.GetInt("achievementDiscarica_1", achievementDiscarica_1);
            achievementDiscarica_2 = PlayerPrefs.GetInt("achievementDiscarica_2", achievementDiscarica_2);
            achievementDiscarica_3 = PlayerPrefs.GetInt("achievementDiscarica_3", achievementDiscarica_3);

            achievementAscensore_1 = PlayerPrefs.GetInt("achievementAscensore_1", achievementAscensore_1);
            achievementAscensore_2 = PlayerPrefs.GetInt("achievementAscensore_2", achievementAscensore_2);
            achievementAscensore_3 = PlayerPrefs.GetInt("achievementAscensore_3", achievementAscensore_3);

            achievementTetto_1 = PlayerPrefs.GetInt("achievementTetto_1", achievementTetto_1);
            achievementTetto_2 = PlayerPrefs.GetInt("achievementTetto_2", achievementTetto_2);
            achievementTetto_3 = PlayerPrefs.GetInt("achievementTetto_3", achievementTetto_3);

            firstLoad = false;

            PlayerPrefs.SetInt("scoreTotale", total);
            PlayerPrefs.SetInt("scoreMontacarichiA", montacarichiA);
            PlayerPrefs.SetInt("scoreDiscarica", discarica);
            PlayerPrefs.SetInt("scoreMontacarichiB", montacarichiB);
            PlayerPrefs.SetInt("scoreTetto", tetto);

            PlayerPrefs.SetInt("sbloccoMontacarichiUI", sbloccoMontacarichi);
            PlayerPrefs.SetInt("sbloccoDiscaricaUI", sbloccoDiscarica);
            PlayerPrefs.SetInt("sbloccoAscensoreUI", sbloccoAscensore);
            PlayerPrefs.SetInt("sbloccoTettoUI", sbloccoTetto);

            PlayerPrefs.SetInt("achievementMontacarichi_1", achievementMontacarichi_1);
            PlayerPrefs.SetInt("achievementMontacarichi_2", achievementMontacarichi_2);
            PlayerPrefs.SetInt("achievementMontacarichi_3", achievementMontacarichi_3);

            PlayerPrefs.SetInt("achievementDiscarica_1", achievementDiscarica_1);
            PlayerPrefs.SetInt("achievementDiscarica_2", achievementDiscarica_2);
            PlayerPrefs.SetInt("achievementDiscarica_3", achievementDiscarica_3);

            PlayerPrefs.SetInt("achievementAscensore_1", achievementAscensore_1);
            PlayerPrefs.SetInt("achievementAscensore_2", achievementAscensore_2);
            PlayerPrefs.SetInt("achievementAscensore_3", achievementAscensore_3);

            PlayerPrefs.SetInt("achievementTetto_1", achievementTetto_1);
            PlayerPrefs.SetInt("achievementTetto_2", achievementTetto_2);
            PlayerPrefs.SetInt("achievementTetto_3", achievementTetto_3);

            PlayerPrefs.Save();
        }


    }


    public void SaveScore(int score)
    {


        currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "Montacarichi1":
                if (score > PlayerPrefs.GetInt("scoreMontacarichiA", montacarichiA))
                {
                    montacarichiA = score;
                    TotalCount();
                }

                break;

            case "Discarica":
                if (score > PlayerPrefs.GetInt("scoreDiscarica", discarica))
                {
                    discarica = score;
                    TotalCount();
                }
                break;

            case "Montacarichi2":
                if (score > PlayerPrefs.GetInt("scoreMontacarichiB", montacarichiB))
                {
                    montacarichiB = score;
                    TotalCount();
                }
                break;

            case "Tetto":
                if (score > PlayerPrefs.GetInt("scoreTetto", tetto))
                {
                    tetto = score;
                    TotalCount();
                }
                break;

        }
    }

    void TotalCount()
    {
        total = montacarichiA + discarica + montacarichiB + tetto;
    }
}

