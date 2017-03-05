using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public UIController uiRef;
    public ReferenceManager refManager;
    public GameObject GameOverCanvas;

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

    float timeToShow = 5f;
    public bool shown;

    void Awake()
    {
        uiRef = FindObjectOfType<UIController>();
	}

	public void GameOverStart ()
    {
        Time.timeScale = 0;
        GameOverCanvas.SetActive(true);
	}

    public IEnumerator KilledScore()
    {
        killedEnemyScore = uiRef.enemyScore;
        float timer = 0;
        while (timer < timeToShow)
        {
            killedEnemy.text = Mathf.Lerp(0, killedEnemyScore, timer/timeToShow).ToString();
            timer += Time.deltaTime;         
            yield return null;
        }
    }

    public IEnumerator WaveScore()
    {
        int waveScoreTemp = 0;
        for (int i = 0; i < refManager.waveRef.currentWaveNumber; i++)
        {
            waveScoreTemp += 500 * (i + 1);
        }
        waveScoreScore = (refManager.waveRef.currentWaveNumber + 1) Mathf.;
        float timer = 0;
        while (timer < timeToShow)
        {
            Mathf.Lerp(0, killedEnemyScore, timer / timeToShow);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator TripleScore()
    {
        float timer = 0;
        while (timer < timeToShow)
        {
            Mathf.Lerp(0, killedEnemyScore, timer / timeToShow);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator QuadraScore()
    {
        float timer = 0;
        while (timer < timeToShow)
        {
            Mathf.Lerp(0, killedEnemyScore, timer / timeToShow);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator MultiScore()
    {
        float timer = 0;
        while (timer < timeToShow)
        {
            Mathf.Lerp(0, killedEnemyScore, timer / timeToShow);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator SpentScore()
    {
        float timer = 0;
        while (timer < timeToShow)
        {
            Mathf.Lerp(0, killedEnemyScore, timer / timeToShow);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator ComboScore()
    {
        float timer = 0;
        while (timer < timeToShow)
        {
            Mathf.Lerp(0, killedEnemyScore, timer / timeToShow);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator ShowScore()
    {
        while (!shown)
        {
            yield return StartCoroutine(KilledScore());



        }
    }

    public void RetryLvl()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainManu()
    {
        SceneManager.LoadScene("Menu");
    }
}
