using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToTutorial : MonoBehaviour {
    public GameObject A;
    public GameObject LoadingText;

	
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            A.SetActive(false);
            LoadingText.SetActive(true);
            int indexSC = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(indexSC + 1);
        }
    }
}
