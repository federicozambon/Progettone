using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

    public ClipController[] clipC;
	
	void Start ()
    {
        clipC = this.GetComponentsInChildren<ClipController>();
    }
	
	public void playSound(AudioClip a)
    {
    
        

        foreach (var clip in clipC)
        {
            
                if (clip.playable == true)
                {
                    clip.playable = false;
                    clip.Play(a);
                    break;
                }
        }
	}
}
