using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class CameraFollow : MonoBehaviour
    {
        public Material matRef;
        public Texture2D textureRef;
        public Transform target;  
        Vector3 offset; 

        void Start ()
        {
            offset = transform.position - target.position;
            transform.LookAt(target);
        }

        void FixedUpdate ()
        {
            /*
           Color[] color = new Color[1024];
            for (int i = 0; i < 1023; i++)
            {
                color[i] = textureRef.GetPixel(1024, i);      
            }
           for (int f = 0; f < 1023; f++)
            {
                if (f != 0)
                {
                    textureRef.SetPixel(1024 - f, f, textureRef.GetPixel(1024 - f - 1, f));
                }
                else
                {
                    textureRef.SetPixel(0,f, color[f]);
     
                }
            }
            textureRef.Apply();
            //matRef.SetTexture("_MainTex", textureRef);
            */
            transform.position = target.position + offset;
        }
    }
}