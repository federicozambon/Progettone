using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Achievement : MonoBehaviour {

    public int total = 0;
    public int tutorial = 0;
    public int montacarichiA = 0;
    public int discarica = 0;
    public int montacarichiB = 0;
    public int tetto = 0;
    bool firstLoad = true;

    public string currentScene;

    void Awake () {

        DontDestroyOnLoad(this.gameObject);


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
	
	
	public void SaveScore (int score)
    {
        currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "Montacarichi1":
                if (score > PlayerPrefs.GetInt("scoreMontacarichiA", montacarichiA))
                {
                    total += score;
                    montacarichiA = score;
                }
                
                break;

            case "Discarica":
                if (score > PlayerPrefs.GetInt("scoreDiscarica", discarica))
                {
                    total += score;
                    discarica = score;
                }
                break;

            case "Montacarichi2":
                if (score > PlayerPrefs.GetInt("scoreMontacarichiB", montacarichiB))
                {
                    total += score;
                    montacarichiB = score;
                }
                break;

            case "Tetto":
                if (score > PlayerPrefs.GetInt("scoreTetto", tetto))
                {
                    total += score;
                    tetto = score;
                }
                break;

        }
    }
}
