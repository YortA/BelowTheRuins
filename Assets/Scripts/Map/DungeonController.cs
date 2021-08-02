using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DungeonController : MonoBehaviour
{
    [SerializeField]
    protected TileManager tileManager = null;
    [SerializeField]
    protected PlayerAttributes playerAttributes;
    [SerializeField]
    protected Vector2Int startPos = Vector2Int.zero;


    public void GenerateDungeon()
    {
        tileManager.ClearTiles();
        RunBSP();
    }

    protected abstract void RunBSP();
}
