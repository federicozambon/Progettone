using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnerDataBase
    {
        public GameObject typeEnemy;
        public GameObject spawnEnemy;
        public float timerEnemy;
    }

    public SpawnerDataBase[] firstWave;
    public SpawnerDataBase[] secondWave;
    public SpawnerDataBase[] thirdWave;
    public SpawnerDataBase[] fourthWave;
    public SpawnerDataBase[] fifthWave;
    public SpawnerDataBase[] sixthWave;
    public SpawnerDataBase[] seventhWave;
    public SpawnerDataBase[] eighthWave;
    public SpawnerDataBase[] ninthWave;
    public SpawnerDataBase[] tenthWave;

    public List<SpawnerDataBase[]> arrayList;

    public GameObject boss;

    WaveController waveRef;

    public bool isBossDefeated = false;
    public bool isArenaEmpty;
    public bool allSpawned;

    public int enemyCount;

    void Start()
    {
        waveRef = FindObjectOfType<WaveController>();

        arrayList = new List<SpawnerDataBase[]>();

        arrayList.Add(firstWave);
        arrayList.Add(secondWave);
        arrayList.Add(thirdWave);
        arrayList.Add(fourthWave);
        arrayList.Add(fifthWave);
        arrayList.Add(sixthWave);
        arrayList.Add(seventhWave);
        arrayList.Add(eighthWave);
        arrayList.Add(ninthWave);
        arrayList.Add(tenthWave);
    }

    public int spawnedCounter = 0;
    int toSpawnCounter = 0;

    public void Spawn(int waveNumber)
    {

        toSpawnCounter = arrayList[waveNumber].Length;
        spawnedCounter = 0;
        foreach (var enemy in arrayList[waveNumber])
        {
            StartCoroutine(SpawnEnemy(enemy));
        }
    }

    public IEnumerator SpawnEnemy(SpawnerDataBase spawnerDB)
    {
        yield return new WaitForSeconds(spawnerDB.timerEnemy);
        Instantiate(spawnerDB.typeEnemy, spawnerDB.spawnEnemy.transform.position, Quaternion.identity);
        spawnedCounter++;
        if (toSpawnCounter == spawnedCounter)
        {
            allSpawned = true;
        }
    }
}
