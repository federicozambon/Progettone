using UnityEngine;
using System.Collections;

public class ClipController : MonoBehaviour {

    public bool playable = true;
    AudioSource aSource;
	
	void Start ()
    {
        aSource = GetComponent<AudioSource>();
	}

    public void Play(AudioClip a)
    {
        
            StartCoroutine(playFx(a));
        
    }
	
	IEnumerator playFx(AudioClip sfx)
    {
        aSource.clip = sfx;
        aSource.Play();

        yield return new WaitForSeconds(sfx.length);

        playable = true;
    }
}
