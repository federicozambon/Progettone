using UnityEngine;
using System.Collections;

public class Ramp_Rotation : MonoBehaviour {

    public int randomValue;

    void Start()
    {
        randomValue = Random.Range(30,60);
        //StartCoroutine(Fade());   
    }

    IEnumerator Fade()
    {
        for (float pippo = 0; pippo <= 130; pippo += Time.deltaTime * randomValue)
        { 
            transform.Rotate(0, 0, Time.deltaTime * randomValue);          
            yield return null;
        }
    }
}
