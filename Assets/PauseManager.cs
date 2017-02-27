using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {
    public GameObject CanvasPanel1;
    public GameObject CanvasPanel2;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            CanvasPanel1.SetActive(true);
        }    

    }
    public void QuitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
    public void AudioOptions()
    {
        CanvasPanel1.SetActive(false);
        CanvasPanel2.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = 1;
        CanvasPanel1.SetActive(false);
    }
}
