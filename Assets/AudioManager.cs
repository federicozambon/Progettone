using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    
    static Slider sliderRef;

    void Awake()
    {
        sliderRef = GetComponent<Slider>();
        //AudioListener.volume = sliderRef.value;
    }


    public void SetVolume ()
    {
        //AudioListener.volume = sliderRef.value;
    }
}
