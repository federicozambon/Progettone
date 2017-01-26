using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponSelector : MonoBehaviour {

    public bool isAxisInUse = false;
    public int currentWaepon = 1;
    public Image weaponSelected;
    UIController uiElements;
  
    void Start()
    {
        uiElements = FindObjectOfType<UIController>();
    }

    public void ChangeWeapon(int weaponNumber)
    {
        switch (weaponNumber)
        {
            case 1:
                weaponSelected.sprite = Resources.Load("Weapons/One", typeof(Sprite)) as Sprite;
                uiElements.ammo.GetComponent<Text>().enabled = false;
                break;
            case 3:
                weaponSelected.sprite = Resources.Load("Weapons/Two", typeof(Sprite)) as Sprite;
                uiElements.ammo.GetComponent<Text>().enabled = false;
                break;
            case 2:
                weaponSelected.sprite = Resources.Load("Weapons/Tree", typeof(Sprite)) as Sprite;
                uiElements.ammo.GetComponent<Text>().enabled = true;

                break;
        }

    }
        public void ChangeWeaponNegative()
    {
        if (currentWaepon > 1)
            currentWaepon--;

        else
            currentWaepon = 3;

        switch (currentWaepon)
        {
            case 1:
                weaponSelected.sprite = Resources.Load("Weapons/One", typeof(Sprite)) as Sprite;
                uiElements.ammo.GetComponent<Text>().enabled = false;
                break;
            case 2:
                weaponSelected.sprite = Resources.Load("Weapons/Two", typeof(Sprite)) as Sprite;
                uiElements.ammo.GetComponent<Text>().enabled = false;
                break;
            case 3:
                weaponSelected.sprite = Resources.Load("Weapons/Tree", typeof(Sprite)) as Sprite;
                uiElements.ammo.GetComponent<Text>().enabled = true;
                break;
        }

    }

    void Update()
    {
        if (Input.GetAxis("Next Weapon") == 0)
            isAxisInUse = false;
    }

}
