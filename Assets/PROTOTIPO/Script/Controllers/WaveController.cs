using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class WaveController : MonoBehaviour
{
    Spawner spawnRef;
    public int currentWaveNumber = 0;
    public List<WallMover> firstDoorsList;
    public List<WallMover> secondDoorsList;
    public bool isWaveFinished;
    public int killedCounter = 0;
    FlyCamManager flyRef;
    UIController uiElements;

    void Awake()
    {
        flyRef = FindObjectOfType<FlyCamManager>();
        spawnRef = FindObjectOfType<Spawner>();
        currentWaveNumber = 0;
        firstDoorsList = new List<WallMover>();
        secondDoorsList = new List<WallMover>();
        uiElements = FindObjectOfType<UIController>();
    }

    public IEnumerator StartWave()
    {
        yield return new WaitForSeconds(3f);
        isWaveFinished = false;
        spawnRef.Spawn(currentWaveNumber%10);
    }

    public void IsWaveFinished()
    {
        killedCounter++;
        if (spawnRef.allSpawned)
        {
            if (killedCounter == spawnRef.spawnedCounter)
            {
                isWaveFinished = true;
                currentWaveNumber++;
                killedCounter = 0;
                StartCoroutine(uiElements.NextWave());
                
                if (currentWaveNumber == 5)
                {
                    OpenFirstRoom();
                }
       
                else
                {
                    StartCoroutine(StartWave());                
                }
            }
            else
            {
                isWaveFinished = false;
            }
        }    
    }

    public void OpenFirstRoom()
    {
        if (SceneManager.GetActiveScene().name == "Discarica")
        {
            foreach (var door in firstDoorsList)
            {
                StartCoroutine(door.HideWall());
            }
            StartCoroutine(StartWave());
            StartCoroutine(DisapperCupola());
        }
        StartCoroutine(StartWave());

    }

    IEnumerator DisapperCupola()
    {
        int counter = 128;
        while (counter > 0)
        {
            flyRef.cupole[1].GetComponent<MeshRenderer>().material.SetInt("_BumpAmt", counter);
            counter -= 4;
            yield return null;
        }
    }

    IEnumerator DisapperCupola2()
    {
        int counter = 128;
        while (counter > 0)
        {
            flyRef.cupole[3].GetComponent<MeshRenderer>().material.SetInt("_BumpAmt", counter);
            counter -= 4;
            yield return null;
        }
    }


    public void OpenSecondRoom()
    {
        if (SceneManager.GetActiveScene().name == "Lvl1 + tutorial")
        {
            foreach (var door in secondDoorsList)
            {
                StartCoroutine(door.HideWall());
            }
            StartCoroutine(DisapperCupola2());
        }
            StartCoroutine(StartWave());
  
    }
}
