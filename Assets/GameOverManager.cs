using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {
    public GameObject GameOverCanvas;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void GameOverStart ()
    {
        Time.timeScale = 0;
        GameOverCanvas.SetActive(true);	
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
