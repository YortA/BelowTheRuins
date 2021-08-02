using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
   // public Transform playerTransform;
    public Vector3 zOffset;

	private GameObject player;

	private void Start()
	{
		// fix camera clipping through
		zOffset.z = -10;
		// find our prefab named "player" (i'm SURE there's a better way to do this.)
		player = GameObject.Find("Player");
	}

	private void FixedUpdate()
    {
		if(player)
		{
			transform.position = player.transform.position + zOffset;
		}
		else
		{
			Debug.Log("ERROR: No player found.");
			//Debug.LogError("No player found.");
		}
		//transform.position = playerTransform.position + zOffset;
	}
}
