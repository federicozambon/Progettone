using UnityEngine;

public abstract class Weapon: MonoBehaviour
{
    public GameObject playerGo;
    public GameObject particleGo;
    public LineRenderer gunLine;
    public AudioSource gunAudio;
    public Light gunLight;
    public Player rotRef;

    public AssaultRifle assaultRifle;
    public LaserShotgun laserShotgun;

    public int damagePerShot;
    public float effectsDisplayTime;
    public float timeBetweenBullets;
    public float range;
    public bool collided;
    public int deltaDegrees;
    public float timer;
    public MeshRenderer[] weaponArray = new MeshRenderer[2];

    public int equippedWeapon;
}
