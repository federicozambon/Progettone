using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToTutorial : MonoBehaviour {

	
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            int indexSC = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(indexSC + 1);
        }
    }
}
