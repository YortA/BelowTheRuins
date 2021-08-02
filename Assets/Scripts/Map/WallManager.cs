using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallManager
{
	public static void GenerateWalls(HashSet<Vector2Int> groundPos, TileManager tileManager)
	{
		var WallPos = WallDirection(groundPos, Direction.directionsList);

		foreach (var pos in WallPos)
		{
			tileManager.CreateSingleWall(pos);
		}
	}

	private static HashSet<Vector2Int> WallDirection(HashSet<Vector2Int> groundPos, List<Vector2Int> directionList)
	{
		HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();
		// for each position in our ground list, check the nearby grid square for an empty position
		foreach (var pos in groundPos)
		{
			foreach (var direction in directionList)
			{
				var neighborPos = pos + direction;
				// if our ground pos doesn't contain a tile in one of the directions
				if(!groundPos.Contains(neighborPos))
				{
					wallPos.Add(neighborPos);
				}
			}
		}
		// returns our empty locations near the ground pos
		return wallPos;	
	}
}


/***********************
*DIRECTION STATIC CLASS*
************************/
public static class Direction
{
	// we could use a struct or enum here, but we want to be able to use this with other algroithms if we choose to. keep it a static class
	// we also don't need a value type (struct for example) and want to keep this on the heap to be used when called.
	public static List<Vector2Int> directionsList = new List<Vector2Int>
	{
		new Vector2Int(0,1), // top	(x,y)
		new Vector2Int(1,1), // top right
        new Vector2Int(1,0), // right
		new Vector2Int(1,-1), // bottom right
        new Vector2Int(0, -1), // bottom
		new Vector2Int(-1,-1), // bottom left
        new Vector2Int(-1,0), // left
		new Vector2Int(-1,1) // top left
    };
}