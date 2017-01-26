using UnityEngine;
using System.Collections;

public class FollowingPlayerLight : MonoBehaviour
{
    public Transform player;

	void Update ()
    {
        this.transform.LookAt(player);
	}
}
