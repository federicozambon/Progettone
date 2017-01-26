using UnityEngine;
using System.Collections;

public class CutSceneDeleter : MonoBehaviour
{
    public Player playerRef;
    public FlyCamManager flyRef;

	void Awake()
    {
        playerRef = FindObjectOfType<Player>();
        flyRef = FindObjectOfType<FlyCamManager>();
	}
	
	void Update ()
    {
        transform.LookAt(new Vector3(playerRef.transform.position.x, this.transform.position.y, playerRef.transform.position.z));
        if (flyRef.endedCutScene)
        {
            Destroy(this.gameObject);
        }
	}
}
