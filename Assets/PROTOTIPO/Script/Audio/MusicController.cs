using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{
    public AudioClip[] musicFiles = new AudioClip[2];
    AudioSource audioRef;

	void Start ()
    {
        //audioRef = GetComponent<AudioSource>();
        //GetComponent<AudioSource>().clip = musicFiles[0];
        //audioRef.Play();
	}

    IEnumerator Player()
    {
        while (audioRef.isPlaying)
        {
            yield return null;
        }
        audioRef.clip = musicFiles[1];
        audioRef.Play();
        while (audioRef.isPlaying)
        {
            yield return null;
        }
        audioRef.clip = musicFiles[0];
        audioRef.Play();
        StartCoroutine(Player());
    }
}
