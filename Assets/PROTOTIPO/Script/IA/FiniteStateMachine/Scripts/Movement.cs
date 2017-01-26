using UnityEngine;
using System.Collections;


public class Movement : MonoBehaviour
{
    public NavMeshAgent navRef;
    public Vector3 destination;
    public float rangeAspeed = 8;
    public float rangeBspeed = 5;
    public float rangeCspeed = 3;

    IEnumerator UpdateDestination()
    {
        if (navRef.isActiveAndEnabled)
        {
            navRef.SetDestination(destination);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(UpdateDestination());  
    }

    void Start()
    {
        StartCoroutine(UpdateDestination());
    }
}
