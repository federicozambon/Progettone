using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponSelector : MonoBehaviour {

    public bool isAxisInUse = false;
    public int currentWaepon = 1;
    public Image weaponSelected;
    UIController uiElements;
    public float timer = 0;

    AssaultRifle assault;
    LaserShotgun shotgun;
    public bool canSwitch = true;

    void Start()
    {
        uiElements = FindObjectOfType<UIController>();
        assault = FindObjectOfType<AssaultRifle>();
        shotgun = FindObjectOfType<LaserShotgun>();
    }

    public void ChangeWeapon(int weaponNumber)
    {
        if (canSwitch)
        {
            if (weaponNumber == 1)
            {
                uiElements.assault.color = new Color(1, 1, 1, 1);
                uiElements.shotgun.color = new Color(1, 1, 1, 0.2f);
            }
            else
            {
                uiElements.shotgun.color = new Color(1, 1, 1, 1);
                uiElements.assault.color = new Color(1, 1, 1, 0.2f);
            }
        }
    }
}
