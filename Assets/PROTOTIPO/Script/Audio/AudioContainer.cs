using UnityEngine;
using System.Collections;

public class AudioContainer : MonoBehaviour {

    
    protected static AudioContainer _self;
    public static AudioContainer Self

    {
        get
        {
            if (_self == null)
                _self = FindObjectOfType(typeof(AudioContainer)) as AudioContainer;
            return _self;
        }
    }

    public AudioClip Laser_Sparo;
    public AudioClip Shotgun_Sparo;
    public AudioClip RocketLauncher_Sparo;
    public AudioClip Dash;

    public AudioClip Armor_PickUp;
    public AudioClip Weapon_PickUp;
    public AudioClip Ammo_PickUp;
    public AudioClip Health_PickUp;
    


    void Start ()
    {
	
	}
	
	
}
