using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;		// HashSet.ElementAt fn

public class PlayerAttributes : Attributes		// inherit from attributes
{
	public string playerName;
	protected Vector2Int startPos = Vector2Int.zero;	// start pos @ 0,0

	private void Awake()
	{
		//transform.position = (Vector3Int)startPos;
		//playerName = gamemanager playernames
	}
	
	// we only need to call this class from our dungeon generator script to get our starting pos
	//public void SelectSpawn(HashSet<Vector2Int> groundPos)
	//{
	//	var randomTile = Random.Range(0, groundPos.Count);                  // get a random element
	//	var location = (Vector3Int)groundPos.ElementAt(randomTile);
	//	//transform.position = new Vector3(location.x, location.y, location.z);
	//	transform.position = location;	// cast as Vector3Int (for pos) and assign our new position
	//	Debug.Log("POS: " + transform.position);
	//}

	public void CenterSpawn(List<BoundsInt> groundPos)
	{
		var randomTile = Random.Range(0, groundPos.Count);                  // get a random element
		var location = groundPos[randomTile];
		transform.position = location.center;  // spawnn in center of one of our listed rooms
		Debug.Log("POS: " + transform.position);
	}


	public override void ActorDied()
	{
		Debug.Log(playerName + " has died...");
		// play animations
		// pause game
	}
}
