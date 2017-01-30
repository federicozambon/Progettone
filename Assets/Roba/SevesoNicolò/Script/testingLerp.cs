using UnityEngine;
using System.Collections;

public enum ColoreMIO
{
    Verde,
    Rosso,
    Giallo
}

public class testingLerp : MonoBehaviour {

    public Color a;
    public Color b;
    public Color c;
    [Range(0,1)]
    public float i = 0;
    float timer = 0;
    [Range (0,1)]
    public float scalato = 0;
    Light luce;
    GameObject player;
    public float distanza;
    [Range(100, 1000)]
    public float rangeLight;

    ColoreMIO test = ColoreMIO.Verde;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        luce = GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update ()
    {

        distanza = Vector3.Distance(this.gameObject.transform.position, player.transform.position);
        distanza = (distanza / rangeLight) * distanza;
        timer += Time.deltaTime * scalato;
        
        c = Color.Lerp(a,b, distanza);
        if (timer >= 1)
        {
            timer = 0;
            a = b;
            b = new Color(Random.value, Random.value, Random.value);
            
          

            /* Colore Switch Enum
            switch (test)
            {
                case ColoreMIO.Verde:
                    b = Color.green;
                    break;
                case ColoreMIO.Rosso:
                    b = Color.red;
                    break;
                case ColoreMIO.Giallo:
                    b = Color.yellow;
                    
                    break;
                default:
                    break;
            }
            //test++;
            
            if (++test > ColoreMIO.Giallo)
            {
                test = ColoreMIO.Verde;
            }
            */
        }
        GetComponent<Renderer>().material.SetColor("_EmissionColor", c);
        luce.color = (c);


    }

    IEnumerator ColorTimed()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(scalato);
        }
        
    }
}
