﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverManager : MonoBehaviour
{
    public UIController uiRef;
    public ReferenceManager refManager;
    public GameObject GameOverCanvas;

    public Button riprova;
    public Button next;
    public Button menu;

    public Text waveScore;
    public Text tripleKill;
    public Text quadraKill;
    public Text multiKill;
    public Text killedEnemy;
    public Text totalScore;
    public Text multiMax;
    public Text spentPoints;

    public int killedEnemyScore;
    public int waveScoreScore;
    public int tripleKillScore;
    public int quadraKillScore;
    public int multiKillScore;
    public int totalScoreScore;
    public int comboMaxScore;
    public int spentPointsScore;

    float timeToShow = 0.003f;
    public bool shown;

    public int tempTotalScore = 0;

    void Awake()
    {
        refManager = GameObject.FindGameObjectWithTag("Reference").GetComponent<ReferenceManager>();
        uiRef = FindObjectOfType<UIController>();
        riprova.interactable = false;        
        next.interactable = false;
        menu.interactable = false;
        GameOverCanvas.SetActive(false);
    }

	public void GameOverStart ()
    {
        uiRef.lifeAlarm.Stop();
        Time.timeScale = 0.001f;
        GameOverCanvas.SetActive(true);
        StartCoroutine(ShowScore());
	}

    public IEnumerator KilledScore()
    {
        killedEnemyScore = uiRef.enemyScore;
        float timer = 0;
        if (killedEnemyScore != 0)
        {
            tempTotalScore += killedEnemyScore;
            while (timer < timeToShow)
            {           
                killedEnemy.text = ((int)Mathf.Lerp(0, killedEnemyScore, timer/timeToShow)).ToString();
                timer += Time.deltaTime;         
                yield return null;
            }
            killedEnemy.text = killedEnemyScore.ToString();
        }
    }

    public IEnumerator WaveScore()
    {
        for (int i = 0; i < refManager.waveRef.currentWaveNumber+1; i++)
        {
            waveScoreScore += 1000 * (i + 1);
        }

        float timer = 0;

        if (waveScoreScore != 0)
        {
            tempTotalScore += waveScoreScore;
            while (timer < timeToShow)
            {         
                waveScore.text = ((int)Mathf.Lerp(0, waveScoreScore, timer / timeToShow)).ToString();
                timer += Time.deltaTime;
                yield return null;
            }
            waveScore.text = waveScoreScore.ToString();
        }
    }

    public IEnumerator TripleScore()
    {
        tripleKillScore = uiRef.tripleKillCounter;
        float timer = 0;
        if (tripleKillScore!= 0)
        {
            tempTotalScore += tripleKillScore;
            while (timer < timeToShow)
            {       
                tripleKill.text = ((int)Mathf.Lerp(0, tripleKillScore, timer / timeToShow)).ToString();
                timer += Time.deltaTime;
                yield return null;
            }
            tripleKill.text = tripleKillScore.ToString();
        }
    }

    public IEnumerator QuadraScore()
    {
        quadraKillScore = uiRef.quadraKillCounter;
        float timer = 0;
        if (quadraKillScore != 0)
        {
            tempTotalScore += quadraKillScore;
            while (timer < timeToShow)
            {     
                quadraKill.text = ((int)Mathf.Lerp(0, quadraKillScore, timer / timeToShow)).ToString();
                timer += Time.deltaTime;
                yield return null;
            }
            quadraKill.text = quadraKillScore.ToString();
        }
    }

    public IEnumerator MultiScore()
    {
        multiKillScore = uiRef.multiKillCounter;
        float timer = 0;
        if (multiKillScore != 0)
        {
            tempTotalScore += multiKillScore;
            while (timer < timeToShow)
            {    
                multiKill.text = ((int)Mathf.Lerp(0, multiKillScore, timer / timeToShow)).ToString();
                timer += Time.deltaTime;
                yield return null;
            }
            multiKill.text = multiKillScore.ToString();
        }
    }

    public IEnumerator SpentScore()
    {
        spentPointsScore = uiRef.spentScore;
        float timer = 0;
        if (spentPointsScore != 0)
        {
            tempTotalScore -= spentPointsScore;
            while (timer < timeToShow)
            {       
                spentPoints.text = "- " + ((int)Mathf.Lerp(0, spentPointsScore, timer / timeToShow)).ToString();
                timer += Time.deltaTime;
                yield return null;
            }
            spentPoints.text = spentPointsScore.ToString();
        }
    }

    public IEnumerator ComboScore()
    {
        comboMaxScore = (int)Mathf.Pow(3, uiRef.maxComboAchieved);
        float timer = 0;
        if (comboMaxScore != 0)
        {
            tempTotalScore += comboMaxScore;
            while (timer < timeToShow)
            {          
                multiMax.text = ((int)Mathf.Lerp(0, comboMaxScore, timer / timeToShow)).ToString();
                timer += Time.deltaTime;
                yield return null;
            }
            multiMax.text = comboMaxScore.ToString();
        }
    }

    public IEnumerator TotalScore()
    {
        float timer = 0;
        if (tempTotalScore != 0)
        {
            while (timer < timeToShow*2)
            {
                totalScore.text = ((int)Mathf.Lerp(0, tempTotalScore, timer / timeToShow*2)).ToString();
                timer += Time.deltaTime;
                yield return null;
            }
            totalScore.text = tempTotalScore.ToString();
        }
        shown = true;
    }

    public IEnumerator ShowScore()
    {
        while (!shown)
        {
            yield return StartCoroutine(KilledScore());
            yield return StartCoroutine(TripleScore());
            yield return StartCoroutine(QuadraScore());
            yield return StartCoroutine(MultiScore());
            yield return StartCoroutine(WaveScore());
            yield return StartCoroutine(ComboScore());
            yield return StartCoroutine(SpentScore());
            yield return StartCoroutine(TotalScore());
        }

        if (FindObjectOfType<Achievement>())
        {
            if (SceneManager.GetActiveScene().name != "Tutorial")
            {
                FindObjectOfType<Achievement>().SaveScore(tempTotalScore);
            }
        }

        menu.interactable = true;
        if ((SceneManager.GetActiveScene().name == "Montacarichi1" && FindObjectOfType<Achievement>().sbloccoMontacarichi == 1)||
            (SceneManager.GetActiveScene().name == "Discarica" && FindObjectOfType<Achievement>().sbloccoDiscarica == 1)||
            (SceneManager.GetActiveScene().name == "Montacarichi2" && FindObjectOfType<Achievement>().sbloccoAscensore == 1))
        {
            next.interactable = true;
            next.GetComponentInChildren<Text>().text = "Nuovo Livello";
        }
        else
        {
            next.GetComponentInChildren<Text>().text = "Livello Bloccato";
            next.interactable = false;
        }

        if (SceneManager.GetActiveScene().name == "Tetto" && FindObjectOfType<UIController>().score >= 500000)
        {
            next.GetComponentInChildren<Text>().text = "Epilogo";
            next.interactable = true;
        }
        else if(SceneManager.GetActiveScene().name == "Tetto" && FindObjectOfType<UIController>().score < 500000)
        {
            next.GetComponentInChildren<Text>().text = "Livello Bloccato";
            next.interactable = false;
        }
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            if (GameObject.FindObjectOfType<Tutorial>().step == 55)
            {
                next.interactable = true;
            }   
        }
        if (next.IsInteractable())
        {
            EventSystem.current.SetSelectedGameObject(next.gameObject);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(riprova.gameObject);
        }

        riprova.interactable = true;
   
    }

    public void RetryLvl()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLvl()
    {
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().name == "Tetto" && FindObjectOfType<UIController>().score > 500000)
        {
            SceneManager.LoadScene("Epilogo");
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void MainManu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    private void Update()
    {
        if (GameOverCanvas.activeInHierarchy && Input.GetButtonDown("Fire1"))
        {
            timeToShow = 0.001f;
        }
    }
}
