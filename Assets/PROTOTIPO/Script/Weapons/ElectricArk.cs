using UnityEngine;

public class ElectricArk : Weapon
{
    public void ChooseWapon()
    {
        if (equippedWeapon == 1)
        {
            Shoot();
        }
        else if (equippedWeapon == 2)
        {
            GetComponent<LaserShotgun>().enabled = true;
            this.enabled = false;
        }
        else if (equippedWeapon == 3)
        {
            GetComponent<ElectricArk>().enabled = true;
            this.enabled = false;
        }
    }

    public void Shoot()
    {

    }
}
