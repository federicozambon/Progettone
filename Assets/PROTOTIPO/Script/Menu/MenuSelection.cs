using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuSelection : MonoBehaviour {

	
	void Start () {
	
	}

    public void Tutorial()
    {
        SceneManager.LoadScene("Intro");
    }

    public void Montacarichi1()
    {
        SceneManager.LoadScene("Intro_Montacarichi1");
    }

    public void Discarica()
    {
        SceneManager.LoadScene("Intro_Discarica");
   
    }

    public void Montacarichi2()
    {
        SceneManager.LoadScene("Intro_Montacarichi2");
    }

    public void Tetto()
    {
        SceneManager.LoadScene("Intro_Tetto");

    }
}
