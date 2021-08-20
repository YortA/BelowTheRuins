using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
	public int health;
	public Vector3 position;

	// constructor
	public PlayerData(PlayerAttributes playerAtr)
	{
		health = playerAtr.health;
		position = playerAtr.transform.position;
	}
}
