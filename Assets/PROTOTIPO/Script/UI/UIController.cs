using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Scrollbar life;
    public Image gameOver;
    public Image godMode;
    public Text ammo;
    public Text thisWave;
    public Text waveEnd;
    public Text numberWave;
    WaveController wElements;
    public Text actualScore;
    public Text comboMultiplier;
    public Text specialFeedback;
    public int score = 0;
    public int scoreStreak = 0;
    public int comboMulti = 1;
    public int maxComboAchieved;
    public int tripleKillCounter, quadraKillCounter, multiKillCounter;
    public int specialCounter = 0;
    public float specialTimer = 3;
    public float specialActualTimer = 0;
    public int[] comboTarget = {1000,2500,5000,7500,10000,15000,20000,30000,40000,50000,75000,100000};
    public int[] specialScore = { 500, 1000, 2000};
    public Coroutine specialCo;
    public Image promptButton;
    public Text weaponUpgrade;
    public Text armorUpgrade;
    public Player playerRef;

    Canvas canvas;

    public void UpdateArmorUpgrade(int percent)
    {
        armorUpgrade.text = "A+" + "\n" + percent + "%";
    }
    public void UpdateWeaponUpgrade(int percent)
    {
        weaponUpgrade.text = "W+" + "\n" + percent + "%";
    }

    public void ShowPrompt()
    {
        promptButton.enabled = true;
    }

    public void HidePrompt()
    {
        promptButton.enabled = false;
    }

    void Awake()
    {
        canvas = GetComponent<Canvas>();
        CanvasOff();
    }

    void Start()
    {
        playerRef = FindObjectOfType<Player>();
        
        wElements = FindObjectOfType<WaveController>();
                 
    }

    public void DecrementLife(float damageTaken)
    {
        life.size -= damageTaken;
        scoreStreak = 0;
        ResetMulti();
    }

    public void IncreaseLife()
    {
        life.size = playerRef.currentHealth / playerRef.maxHealth;
    }

    public void UpdateScore()
    {
        actualScore.text = score + " SP";
    }

    public void IncreaseScore(int scoreToAdd)
    {
        score += scoreToAdd * comboMulti;
        scoreStreak += scoreToAdd * comboMulti;

        if (specialCo == null)
        {
            specialCo = StartCoroutine(specialChecker());
            killedTimerList.Add(specialActualTimer);
            specialCounter++;
        }
        else
        {
            killedTimerList.Add(specialActualTimer);
            specialCounter++;
            specialTimer = 1+specialActualTimer;
            foreach (var killed in killedTimerList)
            {
                if (specialTimer - killed > 3)
                {
                    Debug.LogError(killed);
                    killedTimerList.Remove(killed);
                    specialCounter--;
                }
            }
        }

        if (comboMulti<13)
        {
            if (scoreStreak >= comboTarget[comboMulti])
            {
                IncreaseMulti();
            }
            UpdateScore();
        }
    }

    public void UpdateMultiplier()
    {
        comboMultiplier.text = comboMulti + " X";
    }

    public void ResetMulti()
    {
        comboMulti = 1;
        UpdateMultiplier();
    }

    public void IncreaseMulti()
    {
        if (maxComboAchieved < comboMulti)
        {
            maxComboAchieved = comboMulti;
        }

        comboMulti ++;
        UpdateMultiplier();
    }

    public IEnumerator ShowSpecialFeedback(string specialToShow)
    {
        specialFeedback.text = specialToShow;
        yield return new WaitForSeconds(1);
        specialFeedback.text = "";
    }

    public List<float> killedTimerList = new List<float>();

    public IEnumerator specialChecker()
    {
        while (specialActualTimer < specialTimer)
        {          
            specialActualTimer += Time.deltaTime;
            yield return null;
        }
        if (specialCounter <3)
        {
           
        }
        else if (specialCounter == 3)
        {
            tripleKillCounter++;
            StartCoroutine(ShowSpecialFeedback("Triple Kill"));
            score += specialScore[0];
        }
        else if (specialCounter == 4)
        {
            quadraKillCounter++;
            StartCoroutine(ShowSpecialFeedback("Quadra Kill"));
            score += specialScore[1];
        }
        else if (specialCounter >= 5)
        {
            multiKillCounter++;
            StartCoroutine(ShowSpecialFeedback("Multi Kill"));
            score += specialScore[2];
        }
        killedTimerList.Clear();
        specialCounter = 0;
        specialActualTimer = 0;
        specialTimer = 3;
        specialCo = null;
    }

    public void CanvasOn()
    {
        GetComponent<Canvas>().enabled = true;
    }

    public void CanvasOff()
    {
        GetComponent<Canvas>().enabled = false;
    }

    public void GameOverOn()
    {
        gameOver.enabled = true;
    }

    public void GodModeOn()
    {
        godMode.enabled = true;
        life.gameObject.SetActive(false);
    }

    public void GodModeOff()
    {
        godMode.enabled = false;
        life.gameObject.SetActive(true);
    }

    public IEnumerator NextWave()
    {
        waveEnd.color = Color.green;
        waveEnd.text = "finish wave";
        waveEnd.color = Color.green;

        yield return new WaitForSeconds(2f);

        waveEnd.text = "";
        numberWave.text = wElements.currentWaveNumber.ToString();
        numberWave.color = Color.red;
    }

    public bool decreasing = true;
    float timer = 0;

    void Update()
    {
        if (timer <= 1)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
        if (decreasing)
        {
            promptButton.color = Color.Lerp(new Color(1,1,1,0.9f),new Color(1,1,1,0.1f), timer);
            if (promptButton.color.a < 0.1f)
            {
                decreasing = false;
            }
        }
        else if (!decreasing)
        {
            promptButton.color = Color.Lerp(new Color(1, 1, 1, 0.1f), new Color(1, 1, 1, 0.9f), timer);
            if (promptButton.color.a > 0.9f)
            {
                decreasing = true;
            }
        }  
    }
}

