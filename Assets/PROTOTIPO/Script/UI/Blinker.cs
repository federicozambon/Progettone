using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Blinker : MonoBehaviour
{
    Text textRef;
    Image imageRef;

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
        float ping = Mathf.PingPong(Time.time, 0.8f);

        if (textRef)
        {
            textRef.color = new Color(1, 1, 1, ping);
        }
        else
        {
            imageRef.color = new Color(1, 1, 1, ping);
        }
    }
}
