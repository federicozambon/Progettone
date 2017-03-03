using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
    int score;

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
        

        if (currentScene == "Discarica" && score >= scoreMontacarichiA[0] && indexScore == 0)
        {
            StartCoroutine(SbloccoDialoghi());
        }
    }

    IEnumerator SbloccoDialoghi()
    {
        yield return new WaitForSeconds(3);
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
