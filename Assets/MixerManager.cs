using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerManager : MonoBehaviour {
    public AudioMixer BackgroundMusic;

    public Slider sliderRef;
   
	// Use this for initialization
	void Awake()
    {
        sliderRef = GetComponent<Slider>();
	}

    void Update()
    {
       // BackgroundMusic.SetFloat("MyExposedParam", sliderRef.value);
    }
	
	// Update is called once per frame
	public void BackGroundMusicLvl ()
    {
        BackgroundMusic.SetFloat("MyExposedParam", sliderRef.value);
    }

    public void BackGroundMusicLvl2(float MusicLvl2)
    {
        BackgroundMusic.SetFloat("MyExposedParam 2", MusicLvl2);
    }
}
