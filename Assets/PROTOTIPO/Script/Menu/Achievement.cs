using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Achievement : MonoBehaviour {

    public int total = 0;
    public int tutorial = 0;
    public int montacarichiA = 0;
    public int discarica = 0;
    public int montacarichiB = 0;
    public int tetto = 0;
    public bool firstLoad = true;
    Muoviti muovitiElements;
    public string currentScene;

    public List<int> scoreMontacarichiA;
    public List<int> scoreDiscarica;
    public List<int> scoreMontacarichiB;
    public List<int> scoreTetto;
    int indexScore = 0;
    bool checkLivello = true;
    int score;
    public GameObject SbloccoDialogo;
    public GameObject SbloccoLivello;

    void Awake () {

        DontDestroyOnLoad(this.gameObject);
        muovitiElements = FindObjectOfType<Muoviti>();

        if (firstLoad == true)
        {
            total = PlayerPrefs.GetInt("scoreTotale", total);
            montacarichiA = PlayerPrefs.GetInt("scoreMontacarichiA", montacarichiA);
            discarica = PlayerPrefs.GetInt("scoreDiscarica", discarica);
            montacarichiB = PlayerPrefs.GetInt("scoreMontacarichiB", montacarichiB);
            tetto = PlayerPrefs.GetInt("scoreTetto", tetto);

            firstLoad = false;
            
            PlayerPrefs.SetInt("scoreTotale", total);
            PlayerPrefs.SetInt("scoreMontacarichiA", montacarichiA);
            PlayerPrefs.SetInt("scoreDiscarica", discarica);
            PlayerPrefs.SetInt("scoreMontacarichiB", montacarichiB);
            PlayerPrefs.SetInt("scoreTetto", tetto);

            PlayerPrefs.Save();
        }
       
            
    }

    public void UpdateScore()
    {
        #region SbloccoMontacarichi1
        if (currentScene == "Montacarichi1" && score >= scoreMontacarichiA[0] && checkLivello == true)
        {
            checkLivello = false;
            StartCoroutine(SbloccoLivelli());
        }

        if (currentScene == "Montacarichi1" && score >= scoreMontacarichiA[1] && indexScore == 0)
        {
            indexScore = 1;
            StartCoroutine(SbloccoDialoghi(indexScore));
        }

        if (currentScene == "Montacarichi1" && score >= scoreMontacarichiA[2] && indexScore == 1)
        {
            indexScore = 2;
            StartCoroutine(SbloccoDialoghi(indexScore));
        }

        if (currentScene == "Montacarichi1" && score >= scoreMontacarichiA[3] && indexScore == 2)
        {
            indexScore = 3;
            StartCoroutine(SbloccoDialoghi(indexScore));
        }

        #endregion

        #region SbloccoDiscarica
        if (currentScene == "Discarica" && score >= scoreDiscarica[0] && checkLivello == true)
        {
            checkLivello = false;
            StartCoroutine(SbloccoLivelli());
        }

        if (currentScene == "Discarica" && score >= scoreDiscarica[1] && indexScore == 0)
        {
            indexScore = 1;
            StartCoroutine(SbloccoDialoghi(indexScore));
        }

        if (currentScene == "Discarica" && score >= scoreDiscarica[2] && indexScore == 1)
        {
            indexScore = 2;
            StartCoroutine(SbloccoDialoghi(indexScore));
        }

        if (currentScene == "Discarica" && score >= scoreDiscarica[3] && indexScore == 2)
        {
            indexScore = 3;
            StartCoroutine(SbloccoDialoghi(indexScore));
        }

        #endregion

        #region SbloccoMontacarichi2
        if (currentScene == "Montacarichi2" && score >= scoreMontacarichiB[0] && checkLivello == true)
        {
            checkLivello = false;
            StartCoroutine(SbloccoLivelli());
        }

        if (currentScene == "Montacarichi2" && score >= scoreMontacarichiB[1] && indexScore == 0)
        {
            indexScore = 1;
            StartCoroutine(SbloccoDialoghi(indexScore));
        }

        if (currentScene == "Montacarichi2" && score >= scoreMontacarichiB[2] && indexScore == 1)
        {
            indexScore = 2;
            StartCoroutine(SbloccoDialoghi(indexScore));
        }

        if (currentScene == "Montacarichi2" && score >= scoreMontacarichiB[3] && indexScore == 2)
        {
            indexScore = 3;
            StartCoroutine(SbloccoDialoghi(indexScore));
        }

        #endregion

        #region SbloccoTetto
        if (currentScene == "Tetto" && score >= scoreTetto[0] && checkLivello == true)
        {
            checkLivello = false;
            StartCoroutine(SbloccoLivelli());
        }

        if (currentScene == "Tetto" && score >= scoreTetto[1] && indexScore == 0)
        {
            indexScore = 1;
            StartCoroutine(SbloccoDialoghi(indexScore));
        }

        if (currentScene == "Tetto" && score >= scoreTetto[2] && indexScore == 1)
        {
            indexScore = 2;
            StartCoroutine(SbloccoDialoghi(indexScore));
        }

        if (currentScene == "Tetto" && score >= scoreTetto[3] && indexScore == 2)
        {
            indexScore = 3;
            StartCoroutine(SbloccoDialoghi(indexScore));
        }

        #endregion

    }

    IEnumerator SbloccoDialoghi(int step)
    {
        SbloccoDialogo.SetActive(true);

        SbloccoDialogo.transform.GetChild(0).GetComponent<Text>().text = "Dialogo " + step + " Sbloccato";
        yield return new WaitForSeconds(3);

        SbloccoDialogo.SetActive(false);
    }

    IEnumerator SbloccoLivelli()
    {
        SbloccoLivello.SetActive(true);

        yield return new WaitForSeconds(3);

        SbloccoLivello.SetActive(false);
    }

    public void SaveScore (int score)
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
