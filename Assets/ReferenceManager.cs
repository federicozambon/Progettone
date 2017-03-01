using UnityEngine;
using System.Collections;

public class ReferenceManager : MonoBehaviour
{
    public Spawner spawnRef;
    public WaveController waveRef;
    public UIController uicontroller;
    public GameObject playerObj;
    public MiniMap miniMapRef;
    public Player playerRef;
    public Transform camTr;

    public FlyCamManager flyCamRef;

    void Awake()
    {
        spawnRef = FindObjectOfType<Spawner>();
        flyCamRef = FindObjectOfType<FlyCamManager>();
        waveRef = FindObjectOfType<WaveController>();
        uicontroller = FindObjectOfType<UIController>();
        playerRef = FindObjectOfType<Player>();
        playerObj = playerRef.gameObject;
        miniMapRef = FindObjectOfType<MiniMap>();
        camTr = GameObject.Find("MainCamera").transform;
    }
}
