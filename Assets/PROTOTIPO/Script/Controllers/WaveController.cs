using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class WaveController : MonoBehaviour
{
    Spawner spawnRef;
    FlyCamManager flyRef;
    UIController uiElements;

    public int currentWaveNumber;
    public int killedCounter = 0;
    public bool isWaveFinished;
    PauseManager pauseRef;

    void Awake()
    {
        pauseRef = FindObjectOfType<PauseManager>();
        flyRef = FindObjectOfType<FlyCamManager>();
        spawnRef = FindObjectOfType<Spawner>();
        uiElements = FindObjectOfType<UIController>();
        currentWaveNumber = 0;
    }

    public IEnumerator StartWave()
    {
        yield return new WaitForSeconds(6f);
        isWaveFinished = false;
        StartCoroutine(uiElements.NextWave());
        yield return new WaitForSeconds(3);
        StartCoroutine(spawnRef.Spawn(currentWaveNumber%10));
    }

    public void IsWaveFinished()
    {
        killedCounter++;
        if (killedCounter == spawnRef.toSpawnCounter)   
        {
            isWaveFinished = true;
            currentWaveNumber++;
            killedCounter = 0;
            StartCoroutine(uiElements.WaveFinished());
            StartCoroutine(StartWave());
        } 
    }
}
