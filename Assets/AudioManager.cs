using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [Range(0, 1)]
    public float audiovolume;


    public void Update ()
    {
        AudioListener.volume = audiovolume;
    }
}
