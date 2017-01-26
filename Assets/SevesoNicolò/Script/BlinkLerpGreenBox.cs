using UnityEngine;
using System.Collections;

public class BlinkLerpGreenBox : MonoBehaviour
{
    Light luce;
    
    [Range(1, 100)]
    public float IntensityChangeValue;
    public bool intensitychange = false;
    // Use this for initialization
    void Start()
    {
        luce = GetComponent<Light>();
        GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {

        
        if ( intensitychange == false)
        {
            
            luce.intensity += IntensityChangeValue * Time.deltaTime;
            
        }
        else
        {
            luce.intensity -= IntensityChangeValue * Time.deltaTime;
            
        }
        if (luce.intensity >= 4)
        {
            intensitychange = true;
        }
        if (luce.intensity <= 0)
        {
            intensitychange = false;
        }
    }
}
