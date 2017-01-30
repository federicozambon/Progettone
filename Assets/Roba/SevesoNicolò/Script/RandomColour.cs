using UnityEngine;
using System.Collections;

public class RandomColour : MonoBehaviour
{
    public Color red = Color.red;
    public Color green = Color.red;
    public Color blue = Color.red;
   
    public Color yellow = Color.red;
    public Color pink = Color.red;
    public Color purple = Color.red;
    public Color orange = Color.red;
   
    public Color white = Color.red;
    public Color lol = Color.red;

    private int RN;
    private float t, t2;
    Light light;

    // Use this for initialization
    void Start ()
    {
        light = GetComponent<Light>();
        t = 1;
	}

    public Color color;

	void Update ()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material mat = renderer.material;

        t += Time.deltaTime;
        t2 += Time.deltaTime;

        if (t2 >= 3)
        {
            t2 = 0;
            RN = Random.Range(1, 8);
        }

        if (RN == 1 && t > 1)
        {       
            color = Color.Lerp(mat.color, red, t2 / 3);
    
            light.color = (color);
          //  mat.SetColor("_EmissionMap", color);
            mat.SetColor("_EmissionColor", new Color(color.r, color.g, color.b, 1));
        }
        if (RN == 2 && t > 1)
        {
            color = Color.Lerp(mat.color, green, t2 / 3);
   
            light.color = (color);
          //  mat.SetColor("_EmissionMap", color);
            mat.SetColor("_EmissionColor", new Color(color.r, color.g, color.b, 1));
        }
        if (RN == 3 && t > 1)
        {
            color = Color.Lerp(mat.color, red, t2 / 3);
 
            light.color = (color);
           // mat.SetColor("_EmissionMap", color);
            mat.SetColor("_EmissionColor", new Color(color.r,color.g,color.b,1));
        }
        if (RN == 4 && t > 1)
        {
            color = Color.Lerp(mat.color, pink, t2 / 3);
 
            light.color = (color);
           // mat.SetColor("_EmissionMap", color);
            mat.SetColor("_EmissionColor", new Color(color.r, color.g, color.b, 1));
        }
        if (RN == 5 && t > 1)
        {
            color = Color.Lerp(mat.color, yellow, t2 / 3);
   
            light.color = (color);
          //  mat.SetColor("_EmissionMap", color);
            mat.SetColor("_EmissionColor", new Color(color.r, color.g, color.b, 1));
        }
        if (RN == 6 && t > 1)
        {
            color = Color.Lerp(mat.color, purple, t2 / 3);
   
            light.color = (color);
          //  mat.SetColor("_EmissionMap", color);
            mat.SetColor("_EmissionColor", new Color(color.r, color.g, color.b, 1));
        }
        if (RN == 7 && t > 1)
        {
            color = Color.Lerp(mat.color, orange, t2 / 3);
  
            light.color = (color);
         //   mat.SetColor("_EmissionMap", color);
            mat.SetColor("_EmissionColor", new Color(color.r, color.g, color.b, 1));
        }
        if (RN == 8 && t > 1)
        {
            color = Color.Lerp(mat.color, white, t2 / 3);

            light.color = (color);
          //  mat.SetColor("_EmissionMap", color);
            mat.SetColor("_EmissionColor", new Color(color.r, color.g, color.b, 1));
        }


    }
}
