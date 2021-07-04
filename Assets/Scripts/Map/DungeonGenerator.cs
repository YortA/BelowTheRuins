using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : DungeonController
{
	[SerializeField]
	private int minRoomX = 10, minRoomY = 10;
	[SerializeField]
	private int dungeonX = 100, dungeonY = 100;	// total map size
	[SerializeField]
	[Range(0, 10)]
	private int tileOffset = 3;	// how far away we want our tiles to be from other rooms

	protected override void RunBSP()
	{
		GenerateRooms();
	}

	private void GenerateRooms()
	{
		// call our MapAlgorithm 
		var roomList = MapAlgorithm.BSP(new BoundsInt((Vector3Int)startPos, new Vector3Int(dungeonX, dungeonY, 0)), minRoomX, minRoomY);

		HashSet<Vector2Int> ground = new HashSet<Vector2Int>();
		ground = CreateRooms(roomList);

		foreach(var position in ground)
		{
			Debug.Log(position.x);
		}

		// generate our tiles for our rooms
		tileManager.ClearTiles();
		tileManager.GenerateTiles(ground);
	}

	/* NOTES
	 * Pass center of our object or closest bound min x,y pos and connect to random point.
	 * surface line needs to be straight in order to connect basic hallways.
	 * when room1.x meets room2.x, iterate to current x,y to room2.y (L shape?)*/

	//private HashSet<Vector2Int> CreateHallway();


	private HashSet<Vector2Int> CreateRooms(List<BoundsInt> roomList)
	{
		Debug.Log("CreateRooms");
		HashSet<Vector2Int> ground = new HashSet<Vector2Int>();

		foreach(var room in roomList)
		{
			for(int x = tileOffset; x < room.size.x - tileOffset; x++)
			{
				for(int y = tileOffset; y < room.size.y - tileOffset; y++)
				{
					Vector2Int pos = (Vector2Int)room.min + new Vector2Int(x, y);
					ground.Add(pos);
					Debug.Log(pos);
				}
			}
		}
		return ground;
	}
}