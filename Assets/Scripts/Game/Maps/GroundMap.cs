using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundMap : MonoBehaviour {
    [SerializeField] private Tilemap map;

    [SerializeField] private TileBase waterTile;
    [SerializeField] private TileBase sandTile;

    public bool IsSandTile(Vector3Int position)
    {
        return map.GetTile(position) == sandTile;
    }
    public bool IsSandTile(Vector2Int position)
    {
        return map.GetTile(new Vector3Int(position.x, position.y, 0)) == sandTile;
    }
    public bool IsBorderTile(Vector2Int position)
    {
        var p = new Vector3Int(position.x, position.y, 0);
        return map.GetTile(p) == waterTile;
    }
}
