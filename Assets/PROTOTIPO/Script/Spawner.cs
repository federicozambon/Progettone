using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Spawner : MonoBehaviour
{
    public List<GameObject> furiaPool;
    public List<GameObject> explosiveFuriaPool;
    public List<GameObject> fantePool;
    public List<GameObject> sniperPool;
    public List<GameObject> predatorPool;
    public List<GameObject> titanoPool;

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

        furiaPool = new List<GameObject>();
        explosiveFuriaPool = new List<GameObject>();
        fantePool = new List<GameObject>();
        sniperPool = new List<GameObject>();
        titanoPool = new List<GameObject>();
        predatorPool = new List<GameObject>();

        foreach (var item in GameObject.Find("FuriaPool").GetComponentsInChildren<MeleeEnemy>(true))
        {
            furiaPool.Add(item.gameObject);
        }
        foreach (var item in GameObject.Find("FantePool").GetComponentsInChildren<RangedEnemy>(true))
        {
            fantePool.Add(item.gameObject);
        }
        foreach (var item in GameObject.Find("ExplosiveFuriaPool").GetComponentsInChildren<MeleeExplosiveEnemy>(true))
        {
            explosiveFuriaPool.Add(item.gameObject);
        }
        foreach (var item in GameObject.Find("SniperPool").GetComponentsInChildren<SniperEnemy>(true))
        {
            sniperPool.Add(item.gameObject);
        }
        foreach (var item in GameObject.Find("TitanoPool").GetComponentsInChildren<TitanoEnemy>(true))
        {
            titanoPool.Add(item.gameObject);
        }
        foreach (var item in GameObject.Find("PredatorPool").GetComponentsInChildren<PredatorEnemy>(true))
        {
            predatorPool.Add(item.gameObject);
        }

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

    public GameObject PickEnemy(GameObject enemyToPick)
    {
        string enemyType = enemyToPick.GetComponent<Enemy>().enemyType;

        switch (enemyType)
        {
            case "furia":
                foreach (var enemy in furiaPool)
                {
                    if (!enemy.activeInHierarchy)
                    {
                        return enemy;
                    }
                }
                break;

            case "furiaesplosiva":
                foreach (var enemy in explosiveFuriaPool)
                {
                    if (!enemy.activeInHierarchy)
                    {
                        return enemy;
                    }
                }
                break;

            case "fante":
                foreach (var enemy in fantePool)
                {
                    if (!enemy.activeInHierarchy)
                    {
                        return enemy;
                    }
                }
                break;

            case "predatore":
                foreach (var enemy in predatorPool)
                {
                    if (!enemy.activeInHierarchy)
                    {
                        return enemy;
                    }
                }
                break;

            case "cecchino":
                foreach (var enemy in sniperPool)
                {
                    if (!enemy.activeInHierarchy)
                    {
                        return enemy;
                    }
                }
                break;

            case "titano":
                foreach (var enemy in titanoPool)
                {
                    if (!enemy.activeInHierarchy)
                    {
                        return enemy;
                    }
                }
                break;
        }
        return null;
    }

    public void PlaceAndResetEnemy(GameObject enemyToPlace, Vector3 position)
    {
        Enemy enemyToReset = enemyToPlace.GetComponent<Enemy>();
        enemyToReset.remainHPoints = enemyToReset.hPoints;
        enemyToPlace.transform.position = position;
        enemyToPlace.SetActive(true);
    }

    public void StoreEnemy(GameObject enemyToStore)
    {
        enemyToStore.SetActive(false);
        enemyToStore.transform.position = new Vector3(1000, 1000, 1000);
    }

    public void Spawn(int waveNumber)
    {
        int frameToSkip = 0;
        toSpawnCounter = arrayList[waveNumber].Length;
        spawnedCounter = 0;
        foreach (var enemy in arrayList[waveNumber])
        {
            frameToSkip += 2;
            StartCoroutine(SpawnEnemy(enemy, frameToSkip));
         
        }
        frameToSkip = 0;
    }

    public IEnumerator SpawnEnemy(SpawnerDataBase spawnerDB, int frameToSkip)
    {
        while (frameToSkip>0)
        {
            frameToSkip--;
            yield return new WaitForSeconds(Time.deltaTime*3);
        } 
        yield return new WaitForSeconds(spawnerDB.timerEnemy);
        GameObject enemyToManage = PickEnemy(spawnerDB.typeEnemy);
        PlaceAndResetEnemy(enemyToManage, spawnerDB.spawnEnemy.transform.position);
        //Instantiate(spawnerDB.typeEnemy, spawnerDB.spawnEnemy.transform.position, Quaternion.identity);
        spawnedCounter++;
        if (toSpawnCounter == spawnedCounter)
        {
            allSpawned = true;
        }
    }
}
