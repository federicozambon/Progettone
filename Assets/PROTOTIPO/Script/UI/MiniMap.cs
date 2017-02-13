using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//class used to link enemies to minimap icons
public class EnemyToMap
{
    public bool instantiatedOnMiniMap;
    public GameObject imageRef, enemyRef;
    public Vector3 enemyPos;

    public EnemyToMap(GameObject _enemyRef)
    {    
        enemyRef = _enemyRef;
    }
}


public class MiniMap : MonoBehaviour
{
    public GameObject iconPool;
    public List<GameObject> iconPoolList;

    public ReferenceManager refManager;

    public GameObject enemyIconPrefab;
    public Transform minimapCenter;
    public List<EnemyToMap> enemyToMapList = new List<EnemyToMap>();
    Vector3 playerPos;

	void Start ()
    {
        refManager = GameObject.FindGameObjectWithTag("Reference").GetComponent<ReferenceManager>();
        iconPool = GameObject.Find("IconPool");
        iconPoolList = new List<GameObject>();

        foreach (var item in iconPool.GetComponentsInChildren<Image>(true))
        {
            iconPoolList.Add(item.gameObject);
        }
	}

    GameObject PickIconFromPool()
    {
        foreach (var item in iconPoolList)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                return item;
            }
        }
        return null;
    }

    //method to create enemy minimap icon when new enemy is spawned
    public void NewEnemy(GameObject go)
    {
        EnemyToMap newGo = new EnemyToMap(go);

        enemyToMapList.Add(newGo);
        newGo.imageRef = PickIconFromPool();

        //newGo.imageRef = (GameObject)Instantiate(enemyIconPrefab, minimapCenter.position, Quaternion.identity);
        //Debug.Log(newGo);
        //Debug.Log(newGo.enemyRef);
        newGo.imageRef.transform.SetParent(this.transform);
        newGo.instantiatedOnMiniMap = true;
        //choose color of the icon to spawn
        switch (newGo.enemyRef.GetComponent<Enemy>().enemyType)
        {
            case "fante":
                newGo.imageRef.GetComponent<Image>().color = Color.red;
                break;
            case "furia":
                newGo.imageRef.GetComponent<Image>().color = Color.green;
                break;
            case "furiaesplosiva":
                newGo.imageRef.GetComponent<Image>().color = Color.yellow;
                break;
            case "predatore":
                newGo.imageRef.GetComponent<Image>().color = Color.blue;
                break;
            case "sniper":
                newGo.imageRef.GetComponent<Image>().color = Color.magenta;
                break;
            case "titano":
                newGo.imageRef.GetComponent<Image>().color = Color.black;
                break;
        }
    }

    public GameObject gameplayPrefab;

    //method to delete enemy minimap icon when enemy is killed
    public void DeleteEnemy(GameObject go)
    {
        EnemyToMap toDestroy = enemyToMapList.Find(x => x.enemyRef == go);
        toDestroy.imageRef.transform.SetParent(iconPool.transform);
        toDestroy.imageRef.SetActive(false);
        enemyToMapList.Remove(toDestroy);       
    }

    //minimap management
    void Update ()
    {
        //player position with y normalized to zero
        playerPos = new Vector3(refManager.playerObj.transform.position.x, 0, refManager.playerObj.transform.position.z);

        foreach (var enemy in enemyToMapList)
        {
            if (enemy.enemyRef)
            {
                //enemy position with y normalized to zero
                enemy.enemyPos = new Vector3(enemy.enemyRef.transform.position.x, 0, enemy.enemyRef.transform.position.z);
                
                //distance vector enemy to player
                Vector3 playerToEnemy = enemy.enemyPos - playerPos;

                //if player is into minimap range
                if (playerToEnemy.magnitude < 35)
                {
                    enemy.imageRef.transform.position = minimapCenter.position + playerToEnemy.magnitude * 3.35f *
                        new Vector3(Mathf.Cos(Mathf.Atan2(playerToEnemy.normalized.z, playerToEnemy.normalized.x)+gameplayPrefab.transform.eulerAngles.y),
                        Mathf.Sin(Mathf.Atan2(playerToEnemy.normalized.z, playerToEnemy.normalized.x)+ gameplayPrefab.transform.eulerAngles.y),
                        5);
                }
                //if player is outside minimap range
                else
                {
                    enemy.imageRef.transform.position = minimapCenter.position + 35 * 3.35f*
                        new Vector3(Mathf.Cos(Mathf.Atan2(playerToEnemy.normalized.z, playerToEnemy.normalized.x)+ gameplayPrefab.transform.eulerAngles.y), 
                        Mathf.Sin(Mathf.Atan2(playerToEnemy.normalized.z, playerToEnemy.normalized.x)+ gameplayPrefab.transform.eulerAngles.y),
                        5);
                }
            }          
        }
	}
}
