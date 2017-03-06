using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Blinker : MonoBehaviour
{
    Text textRef;
    Image imageRef;
    public float timeToSmooth;

	void Awake ()
    {
        textRef = GetComponent<Text>();
        if (!textRef)
        {
            imageRef = GetComponent<Image>();
        }
	}

	void Update ()
    {
        float ping = Mathf.PingPong(Time.time, timeToSmooth);

        if (textRef)
        {
            textRef.color = new Color(textRef.color.r, textRef.color.g, textRef.color.b, ping);
        }
        else
        {
            imageRef.color = new Color(imageRef.color.r, imageRef.color.g, imageRef.color.b, ping);
        }
    }
}
