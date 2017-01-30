using UnityEngine;
using System.Collections;

public class skyboxrotation : MonoBehaviour {

    public float rot = 0;
    public int speed = 2;
    Material sky;
    void Start()
    {
        sky = RenderSettings.skybox;
    }
    void Update()
    {
        rot += speed * Time.deltaTime;
        rot %= 360;
        sky.SetFloat("_Rotation", rot);
    }
}
