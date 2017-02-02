using UnityEngine;
using System.Collections;

public class ReferenceManager : MonoBehaviour
{
    public Spawner spawnRef;
    public WaveController waveRef;
    public UIController uicontroller;
    public GameObject playerObj;
    public MiniMap miniMapRef;

    public FlyCamManager flyCamRef;

    void Awake()
    {
        spawnRef = FindObjectOfType<Spawner>();
        flyCamRef = FindObjectOfType<FlyCamManager>();
        waveRef = FindObjectOfType<WaveController>();
        uicontroller = FindObjectOfType<UIController>();
        playerObj = FindObjectOfType<Player>().gameObject;
        miniMapRef = FindObjectOfType<MiniMap>();
    }
}
