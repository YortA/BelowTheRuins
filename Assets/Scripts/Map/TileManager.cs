using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap groundTileMap;
    [SerializeField]
    private TileBase groundTile;    // choose our specific tile

	public void GenerateTiles(IEnumerable<Vector2Int> groundPos)
	{
		Debug.Log("GenerateTiles");
		CreateTiles(groundPos, groundTileMap, groundTile);
	}
	public void ClearTiles()
	{
		groundTileMap.ClearAllTiles();
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
		Debug.Log("CreateSingleTile");
	}
}
