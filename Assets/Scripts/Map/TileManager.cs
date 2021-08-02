using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
	// get our tile map
    [SerializeField]
    private Tilemap groundTileMap, wallTileMap;
	// choose our specific tile
	[SerializeField]
    private TileBase groundTile, wallTile;

	public void GenerateTiles(IEnumerable<Vector2Int> groundPos)
	{
		//Debug.Log("GenerateTiles");
		CreateTiles(groundPos, groundTileMap, groundTile);
	}
	public void ClearTiles()
	{
		groundTileMap.ClearAllTiles();
		wallTileMap.ClearAllTiles();
	}

	private void CreateTiles(IEnumerable<Vector2Int> groundPos, Tilemap groundTileMap, TileBase groundTile)
	{
		foreach(var position in groundPos)
		{
			CreateSingleTile(position, groundTileMap, groundTile);
		}
	}
	private void CreateSingleTile(Vector2Int position, Tilemap groundTileMap, TileBase groundTile)
	{
		var tilePos = groundTileMap.WorldToCell((Vector3Int)position);
		groundTileMap.SetTile(tilePos, groundTile);
		//Debug.Log("CreateSingleTile");
	}

	public void CreateSingleWall(Vector2Int position)
	{
		CreateSingleTile(position, wallTileMap, wallTile);
	}

}