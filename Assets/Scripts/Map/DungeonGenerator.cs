using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonGenerator : DungeonController
{
	[SerializeField]
	private int minRoomX = 10, minRoomY = 10;
	[SerializeField]
	private int dungeonX = 100, dungeonY = 100;	// total map size
	[SerializeField]
	[Range(0, 10)]
	private int tileOffset = 3; // how far away we want our tiles to be from other rooms


	protected override void RunBSP()
	{
		GenerateRooms();
	}

	public void GenerateRooms()
	{
		// call our MapAlgorithm 
		var roomList = MapAlgorithm.BSP(new BoundsInt((Vector3Int)startPos, new Vector3Int(dungeonX, dungeonY, 0)), minRoomX, minRoomY);

		HashSet<Vector2Int> ground = new HashSet<Vector2Int>();
		ground = CreateRooms(roomList);
		//foreach(var position in ground)
		//{
		//	Debug.Log(position.x);
		//}

		List<Vector2Int> roomCenter = new List<Vector2Int>();

		foreach (var room in roomList)
		{
			roomCenter.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
		}

		HashSet<Vector2Int> hallways = ConnectRooms(roomCenter);
		ground.UnionWith(hallways);		// find our unique locations with hallways

		// generate our tiles for our rooms
		tileManager.ClearTiles();
		tileManager.GenerateTiles(ground);
		WallManager.GenerateWalls(ground, tileManager);

		// call object spawns
		playerAttributes.CenterSpawn(roomList);		// player
	}

	private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenter)
	{
		HashSet<Vector2Int> hallways = new HashSet<Vector2Int>();
		var currentCenter = roomCenter[Random.Range(0, roomCenter.Count)];
		roomCenter.Remove(currentCenter);

		while(roomCenter.Count > 0)
		{
			Vector2Int nearest = FindNearestPoint(currentCenter, roomCenter);
			roomCenter.Remove(nearest);     // remove it so we don't keep looking for it
			HashSet<Vector2Int> newHallway = CreateHallway(currentCenter, nearest);
			currentCenter = nearest;
			hallways.UnionWith(newHallway);
		}
		return hallways;
	}

	private HashSet<Vector2Int> CreateHallway(Vector2Int currentCenter, Vector2Int location)
	{
		HashSet<Vector2Int> hallway = new HashSet<Vector2Int>();
		var pos = currentCenter;
		hallway.Add(pos);

		while (pos.y != location.y)
		{
			if(location.y > pos.y)
			{
				pos += Vector2Int.up;
			}
			else if(location.y < pos.y)
			{
				pos += Vector2Int.down;
			}
			hallway.Add(pos);
		}

		while (pos.x != location.x)
		{
			if(location.x > pos.x)
			{
				pos += Vector2Int.right;
			}
			else if(location.x < pos.x)
			{
				pos += Vector2Int.left;
			}
			hallway.Add(pos);
		}
		return hallway;
	}

	private Vector2Int FindNearestPoint(Vector2Int currentCenter, List<Vector2Int> roomCenter)
	{
		Vector2Int nearest = Vector2Int.zero;
		float distance = float.MaxValue;
		foreach (var pos in roomCenter)
		{
			float currentDistance = Vector2.Distance(pos, currentCenter);		// check distance between pos and current
			if(currentDistance < distance)
			{
				distance = currentDistance;
				nearest = pos;
			}
		}
		return nearest;		// return closest point to the center
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
					//Debug.Log(pos);
				}
			}
		}
		return ground;
	}




}