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

    public string currentScene;

    void Start () {

        DontDestroyOnLoad(this.gameObject);

        
        montacarichiA = PlayerPrefs.GetInt("scoreMontacarichiA", montacarichiA);
        discarica = PlayerPrefs.GetInt("scoreDiscarica", discarica);
        montacarichiB = PlayerPrefs.GetInt("scoreMontacarichiB", montacarichiB);
        tetto = PlayerPrefs.GetInt("scoreTetto", tetto);

    }
	
	
	public void SaveScore (int score)
    {
        currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "Montacarichi1":
                if (score > montacarichiA)
                {
                    total += score;
                    montacarichiA = score;
                }
                
                break;

            case "Discarica":
                if (score > discarica)
                {
                    total += score;
                    discarica = score;
                }
                break;

            case "Montacarichi2":
                if (score > montacarichiB)
                {
                    total += score;
                    montacarichiB = score;
                }
                break;

            case "Tetto":
                if (score > montacarichiB)
                {
                    total += score;
                    tetto = score;
                }
                break;

        }
    }
}
