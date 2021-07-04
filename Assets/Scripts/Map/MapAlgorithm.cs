using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/* NOTES
 * BoundsInt : https://docs.unity3d.com/ScriptReference/BoundsInt.html
 * Description: Unity based struct that provides us information about our bounding box.
 * We can get our X,Y, SIZE, CENTER, and so on from this without having to write code
 * for it specifically.
 */
public static class MapAlgorithm
{
	//public
	public static List<BoundsInt> BSP(BoundsInt space, int width, int height)
	{
		Queue<BoundsInt> roomQue = new Queue<BoundsInt>();
		List<BoundsInt> roomList = new List<BoundsInt>();
		roomQue.Enqueue(space);

		while(roomQue.Count > 0)
		{
			var room = roomQue.Dequeue();   // release our room
			if (room.size.x >= width * 2)
			{
				SplitX(height, roomQue, room);
			}
			else if (room.size.y >= height * 2)
			{
				SplitY(width, roomQue, room);
			}
			else if (room.size.x >= width && room.size.y >= height)
			{
				roomList.Add(room);		// once wer're satisfied, add a room to our list
			}
		}
		Debug.Log("Finished getting room sizes.");
		return roomList;		// pass our room list tile bounds to be generated
	}

	// vertical split
	private static void SplitX(int width, Queue<BoundsInt> roomQue, BoundsInt room)
	{
		var split = Random.Range(1, room.size.x);
		BoundsInt firstRoom = new BoundsInt(room.min, new Vector3Int(split, room.size.y, room.size.z));
		BoundsInt secondRoom = new BoundsInt
			(new Vector3Int(room.min.x + split, room.min.y, room.min.z), new Vector3Int(room.size.x - split, room.size.y, room.size.z));

		roomQue.Enqueue(firstRoom);
		roomQue.Enqueue(secondRoom);
		Debug.Log("SplitX");
	}

	// horizontal split
	private static void SplitY(int height, Queue<BoundsInt> roomQue, BoundsInt room)
	{
		var split = Random.Range(1, room.size.y);
		BoundsInt firstRoom = new BoundsInt(room.min, new Vector3Int(room.size.x, split, room.size.z));
		BoundsInt secondRoom = new BoundsInt
			(new Vector3Int(room.min.x, room.min.y + split, room.min.z), new Vector3Int(room.size.x, room.size.y - split, room.size.z));

		roomQue.Enqueue(firstRoom);
		roomQue.Enqueue(secondRoom);
		Debug.Log("SplitY");
	}
}
