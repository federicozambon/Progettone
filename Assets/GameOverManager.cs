using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

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

    float timeToShow = 3f;
    public bool shown;

    public int tempTotalScore = 0;

    void Awake()
    {
        uiRef = FindObjectOfType<UIController>();
        riprova.interactable = false;        
        next.interactable = false;
        menu.interactable = false;
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
            tempTotalScore += (int)Mathf.SmoothStep(0, killedEnemyScore, timer / timeToShow);
            killedEnemy.text = Mathf.SmoothStep(0, killedEnemyScore, timer/timeToShow).ToString();
            timer += Time.deltaTime;         
            yield return null;
        }
    }

    public IEnumerator WaveScore()
    {
        int waveScoreTemp = 0;
        for (int i = 0; i < refManager.waveRef.currentWaveNumber; i++)
        {
            waveScoreTemp += 1000 * (i + 1);
        }
        waveScoreScore = waveScoreTemp;
        float timer = 0;
        while (timer < timeToShow)
        {
            tempTotalScore += (int)Mathf.SmoothStep(0, waveScoreScore, timer / timeToShow);
            waveScore.text = Mathf.SmoothStep(0, waveScoreScore, timer / timeToShow).ToString();
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator TripleScore()
    {
        tripleKillScore = uiRef.tripleKillCounter;
        float timer = 0;
        while (timer < timeToShow)
        {
            tempTotalScore += (int)Mathf.SmoothStep(0, tripleKillScore, timer / timeToShow);
            tripleKill.text = Mathf.SmoothStep(0, tripleKillScore, timer / timeToShow).ToString();
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator QuadraScore()
    {
        quadraKillScore = uiRef.quadraKillCounter;
        float timer = 0;
        while (timer < timeToShow)
        {
            tempTotalScore += (int)Mathf.SmoothStep(0, quadraKillScore, timer / timeToShow);
            quadraKill.text = Mathf.SmoothStep(0, quadraKillScore, timer / timeToShow).ToString();
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator MultiScore()
    {
        multiKillScore = uiRef.multiKillCounter;
        float timer = 0;
        while (timer < timeToShow)
        {
            tempTotalScore += (int)Mathf.SmoothStep(0, multiKillScore, timer / timeToShow);
            multiKill.text = Mathf.SmoothStep(0, multiKillScore, timer / timeToShow).ToString();
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator SpentScore()
    {
        spentPointsScore = uiRef.spentScore;
        float timer = 0;
        while (timer < timeToShow)
        {
            tempTotalScore -= (int)Mathf.SmoothStep(0, spentPointsScore, timer / timeToShow);
            spentPoints.text = Mathf.SmoothStep(0, spentPointsScore, timer / timeToShow).ToString();
            timer += Time.deltaTime;
            yield return null;
        }
        shown = true;
    }

    public IEnumerator ComboScore()
    {
        comboMaxScore = (int)Mathf.ClosestPowerOfTwo((int)Mathf.Pow(3, uiRef.maxComboAchieved));
        float timer = 0;
        while (timer < timeToShow)
        {
            tempTotalScore += (int)Mathf.SmoothStep(0, comboMaxScore, timer / timeToShow);
            multiMax.text = Mathf.SmoothStep(0, comboMaxScore, timer / timeToShow).ToString();
            timer += Time.deltaTime;
            yield return null;
        }
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
        }
        menu.interactable = true;
        next.interactable = true;
        riprova.interactable = true;
        FindObjectOfType<Achievement>().SaveScore(tempTotalScore);
    }

    public void RetryLvl()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLvl()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void MainManu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void Update()
    {
        totalScore.text = tempTotalScore.ToString();
        if (Input.GetButtonDown("Fire1"))
        {
            timeToShow = 0.1f;
        }
    }
}
