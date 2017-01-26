using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuSelection : MonoBehaviour {

	
	void Start () {
	
	}
	
	
	public void  Menu()
    {
        SceneManager.LoadScene("Menu Alfa");
    }

    public void Livello1()
    {
        SceneManager.LoadScene("Montacarichi1");
   
    }

    public void Livello2()
    {
        SceneManager.LoadScene("Lv1 + tutorial");
    }

    public void Livello3()
    {
        SceneManager.LoadScene("Montacarichi2");

    }
}
