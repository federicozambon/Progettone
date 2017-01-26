using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallMover: MonoBehaviour
{
    WaveController wcRef;
    Vector3 startPos;
    Vector3 endPos;
    Vector3 currentVelocity;

    float timer;

    void Awake()
    {
        wcRef = FindObjectOfType<WaveController>();
    }

    void Start()
    {
        startPos = this.transform.localPosition;
        endPos = startPos - new Vector3(0,1,0);
        wcRef.firstDoorsList.Add(this);
    }

    public IEnumerator HideWall ()
    {
        while (endPos.y < transform.localPosition.y-0.2f)
        {
            timer += Time.deltaTime;
            this.transform.localPosition = Vector3.Lerp(startPos, endPos, timer/2);
            yield return null;
        }
    }
}
