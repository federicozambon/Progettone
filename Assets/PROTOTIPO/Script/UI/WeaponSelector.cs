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
                
                break;
            case 2:
                weaponSelected.sprite = Resources.Load("Weapons/Two", typeof(Sprite)) as Sprite;
                
                break;
           
        }

    }
    
    void Update()
    {
       
    }

}
