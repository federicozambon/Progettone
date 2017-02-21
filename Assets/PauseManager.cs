using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {
    public GameObject CanvasPause;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            CanvasPause.SetActive(true);
        }    
    }
    public void QuitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
    public void Resume()
    {
        Time.timeScale = 1;
        CanvasPause.SetActive(false);
    }
}
