using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject CanvasPanel1;

    public Player playerRef;

    FlyCamManager flyRef;

    public bool paused;
    bool audioMode = false;
    public Text textRef;

    void Awake()
    {
        playerRef = FindObjectOfType<Player>();
        flyRef = FindObjectOfType<FlyCamManager>();
        CanvasPanel1.SetActive(false);
    }

    bool moved = false;
    float timer = 0;
    public float late = 0;
    void Update()
    {
        if ((Input.GetButtonDown("GodMode") || (audioMode == false && paused && Input.GetButtonDown("Cancel"))) && !flyRef.cutScene && playerRef.currentHealth > 0)
        {
            paused = true;
            CanvasPanel1.SetActive(true);
        }
        if ((Input.GetButton("GodMode") && paused))
        {
            late += Time.deltaTime;
            textRef.text = "Tieni premuto start se vuoi ritirarti... " + (3-Mathf.RoundToInt(late));
        }
        else
        {
            late = 0;
            CanvasPanel1.SetActive(false);
            paused = false;
        }
        if (late > 3)
        {
            if (FindObjectOfType<Achievement>())
            {
                FindObjectOfType<Achievement>().SaveScore(FindObjectOfType<UIController>().score);
            }            
            SceneManager.LoadScene("Menu");
        }
    }
}
