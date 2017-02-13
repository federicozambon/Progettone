using UnityEngine;
using System.Collections;


public class Movement : MonoBehaviour
{
    public BlackBoard blackRef;

    public Vector3 destination;
    public float rangeAspeed = 8;
    public float rangeBspeed = 5;
    public float rangeCspeed = 3;

    IEnumerator UpdateDestination()
    {
        if (blackRef.navRef)
        {
            blackRef.navRef.SetDestination(destination);
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(UpdateDestination());  
    }

    private void Awake()
    {
        blackRef = GetComponent<BlackBoard>();
    }

    bool firstTime = true;
    private void OnEnable()
    {
        if (!firstTime)
        {
            StartCoroutine(UpdateDestination());
        }
        firstTime = false;
    }

    void Start()
    {
        StartCoroutine(UpdateDestination());
    }
}
