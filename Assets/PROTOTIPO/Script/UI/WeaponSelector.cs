using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponSelector : MonoBehaviour {

    public bool isAxisInUse = false;
    public int currentWaepon = 1;
    public Image weaponSelected;
    UIController uiElements;
    float timer = 0;
    void Start()
    {
        uiElements = FindObjectOfType<UIController>();
    }

    public IEnumerator ChangeWeapon(int weaponNumber)
    {
        switch (weaponNumber)
        {
            case 1:
                while (timer < 1)
                {
                    timer += Time.deltaTime;
                    float a = Mathf.Lerp(0, 0.9f, timer);
                    uiElements.assault.color = new Color(1, 1, 1, a);
                    uiElements.shotgun.color = new Color(1, 1, 1, 1-a);
                    yield return null;
                }
                timer = 0;
                break;
            case 2:
                while (timer < 1)
                {
                    timer += Time.deltaTime;
                    float a = Mathf.Lerp(0, 0.9f, timer);
                    uiElements.shotgun.color = new Color(1, 1, 1, a);
                    uiElements.assault.color = new Color(1  ,1, 1, 1 - a);
                    yield return null;
                }
                timer = 0;
                break;        
        }
    }
}
