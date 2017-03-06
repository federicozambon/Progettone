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

    PauseManager pauseRef;

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

    public int enemyCount;

    private void Awake()
    {
        pauseRef = FindObjectOfType<PauseManager>();
    }

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

    public int toSpawnCounter = 0;

    int furiaCounter = 0;
    int explosivefuriaCounter = 0;
    int fanteCounter = 0;
    int predatorCounter = 0;
    int sniperCounter = 0;
    int titanoCounter = 0;

    GameObject enemyToSpawn = null;

    public GameObject PickEnemy(GameObject enemyToPick, out GameObject enemyToSpawn)
    {
        string enemyType = enemyToPick.GetComponent<Enemy>().enemyType;
        enemyToSpawn = null;
        switch (enemyType)
        {
            case "furia":            
                if (furiaCounter < 48)
                {
                    furiaCounter++;
                }
                else
                {
                    furiaCounter = 0;
                }
                GameObject furiaToSpawn = furiaPool[furiaCounter];
                enemyToSpawn = furiaToSpawn;
                return furiaToSpawn;

            case "furiaesplosiva":
                if (explosivefuriaCounter < 48)
                {
                    explosivefuriaCounter++;
                }
                else
                {
                    explosivefuriaCounter = 0;
                }
                GameObject furiaEsplosivatoSpawn = explosiveFuriaPool[explosivefuriaCounter];
                enemyToSpawn = furiaEsplosivatoSpawn;
                return furiaEsplosivatoSpawn;

            case "fante":
                if (fanteCounter < 48)
                {
                    fanteCounter++;
                }
                else
                {
                    fanteCounter = 0;
                }
                GameObject fanteToSpawn = fantePool[fanteCounter];
                enemyToSpawn = fanteToSpawn;
                return fanteToSpawn;

            case "predatore":
                if (predatorCounter < 18)
                {
                    predatorCounter++;
                }
                else
                {
                    predatorCounter = 0;
                }
                GameObject predatorToSpawn = predatorPool[predatorCounter];
                enemyToSpawn = predatorToSpawn;
                return predatorToSpawn;

            case "cecchino":
                if (sniperCounter < 18)
                {
                    sniperCounter++;
                }
                else
                {
                    sniperCounter = 0;
                }
                GameObject sniperToSpawn = sniperPool[sniperCounter];
                enemyToSpawn = sniperToSpawn;
                return sniperToSpawn;

            case "titano":
                if (titanoCounter < 8)
                {
                    titanoCounter++;
                }
                else
                {
                    titanoCounter = 0;
                }
                GameObject titanToSpawn = titanoPool[titanoCounter];
                enemyToSpawn = titanToSpawn;
                return titanToSpawn;
        }
        Debug.Log("SPAWN BUG");
        Debug.Log(enemyToSpawn);
        return null;
    }

    public IEnumerator PlaceAndResetEnemy(GameObject enemyToPlace, Vector3 position)
    {
        Enemy enemyToReset = enemyToPlace.GetComponent<Enemy>();
        enemyToReset.SpawnParticleActivator(position);
        yield return new WaitForSeconds(1);   
        enemyToReset.remainHPoints = enemyToReset.hPoints;
        enemyToPlace.transform.position = position;
        enemyToReset.dead = false;
        enemyToReset.dieController = true;
        enemyToPlace.SetActive(true);
    }

    public void StoreEnemy(GameObject enemyToStore)
    {
        enemyToStore.SetActive(false);
    }

    public void Spawn(int waveNumber)
    {
        int frameToSkip = 0;
        toSpawnCounter = arrayList[waveNumber].Length;
        foreach (var enemy in arrayList[waveNumber])
        {
            frameToSkip += 1;
            StartCoroutine(SpawnEnemy(enemy, frameToSkip));        
        }
        frameToSkip = 0;
    }

    public IEnumerator SpawnEnemy(SpawnerDataBase spawnerDB, int frameToSkip)
    {
        for (int j = 0; j < 4; j++)
        {
            for (int i = frameToSkip + 1; i > 0; i--)
            {
                //yield return null;
            }
        }
        yield return new WaitForSeconds(spawnerDB.timerEnemy);
        //yield return new WaitForSeconds(spawnerDB.timerEnemy);
        GameObject enemyToManage = PickEnemy(spawnerDB.typeEnemy, out enemyToSpawn);
        Debug.Log(enemyToSpawn);
        StartCoroutine(PlaceAndResetEnemy(enemyToManage, spawnerDB.spawnEnemy.transform.position));
    }
}
