using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MixerManager : MonoBehaviour {
    public AudioMixer BackgroundMusic;
    public AudioMixer PlayerSounds;
    public AudioMixer PlayeDash;
    public AudioMixer EnemySounds;
    public AudioMixer AmbientSounds;
   
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	public void BackGroundMusicLvl ( float MusicLvl)
    {
        BackgroundMusic.SetFloat("Manager", MusicLvl);
	}
}
