using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBuilder : MonoBehaviour {
    [SerializeField] private Tilemap map;
    [SerializeField] private Transform obstacleRoot;
    [SerializeField] private Transform treasureRoot;
    [SerializeField] private Transform beginningBlocksRoot;

    [Header("Tiles")]
    [SerializeField] private TileBase waterTile;
    [SerializeField] private TileBase groundTile;

    [Header("Prefabs")]
    public GameObject verticalRaft;
    public GameObject horizontalRaft;
    [SerializeField] private GameObject horizontalBarrel;
    [SerializeField] private GameObject verticalBarrel;
    [SerializeField] private GameObject woodenBox;
    [SerializeField] private GameObject pointyRocks;
    [SerializeField] private GameObject cannon;
    [SerializeField] private GameObject bigTreasure;
    [SerializeField] private GameObject smallTreasure;

    [Header("LevelMaps")]
    [SerializeField] public LevelList levels;

    public void BuildMap()
    {
        Clear();
        ObstacleMap.ResetMap();
        LevelMap levelMap = LoadMap(MemoryCard.GetSelectedLevel());
        DrawFloor(groundTile, levelMap.ground);
        DrawFloor(waterTile, levelMap.borders);
        CreateObstacle(horizontalBarrel, levelMap.horizonalBarrel);
        CreateObstacle(verticalBarrel, levelMap.verticalBarrel);
        CreateObstacle(woodenBox, levelMap.woodenBox);
        CreateObstacle(pointyRocks, levelMap.pointyRocks);
        CreateObstacle(verticalRaft, levelMap.verticalRaft);
        CreateObstacle(horizontalRaft, levelMap.horizontalRaft);
        CreateCannon(levelMap.upCannon, CannonType.UP);
        CreateCannon(levelMap.downCannon, CannonType.DOWN);
        CreateCannon(levelMap.leftCannon, CannonType.LEFT);
        CreateCannon(levelMap.rightCannon, CannonType.RIGHT);
        CreateTreasures();
    }
    private void DrawFloor(TileBase tile, List<Vector2> positions)
    {
        foreach(var pos in positions)
        {
            var p = new Vector3Int((int)pos.x, (int)pos.y, 0);
            map.SetTile(p, tile);
        }
    }
    public void CreateObstacle(GameObject objectPrefab, List<Vector2> positions)
    {
        foreach (var pos in positions)
        {
            var obstacle = Instantiate(objectPrefab, obstacleRoot).GetComponent<Obstacle>();
            obstacle.transform.position = new Vector3(pos.x, pos.y, 0) * Globals.TILE_SIZE + new Vector3(0.5f, 0.5f, 0f);
            obstacle.GetPosition();
            ObstacleMap.AddObstacle(obstacle);
        }
    }
    public void CreateCannon(List<Vector2> positions, CannonType type)
    {
        foreach (var pos in positions)
        {
            var obstacle = Instantiate(cannon, obstacleRoot).GetComponent<Obstacle>();
            obstacle.transform.position = new Vector3(pos.x, pos.y, 0) * Globals.TILE_SIZE + new Vector3(0.5f, 0.5f, 0f);
            obstacle.GetPosition();
            obstacle.gameObject.GetComponent<Cannon>().SetType(type);
            ObstacleMap.AddObstacle(obstacle);
        }
    }
    public void CreateTreasures()
    {
        LevelMap levelMap = LoadMap(MemoryCard.GetSelectedLevel());
        foreach (var pos in levelMap.treasures)
        {
            var smallT = Instantiate(smallTreasure, treasureRoot);
            smallT.transform.position = new Vector3(pos.x, pos.y, 0) * Globals.TILE_SIZE + new Vector3(0.5f, 0.5f, 0f);
            smallT.GetComponent<Treasure>().GetPosition();
        }
        foreach (var pos in levelMap.bigTreasures)
        {
            var bigT = Instantiate(bigTreasure, treasureRoot);
            bigT.transform.position = new Vector3(pos.x, pos.y, 0) * Globals.TILE_SIZE + new Vector3(0.5f, 0.5f, 0f);
            bigT.GetComponent<Treasure>().GetPosition();
        }
    }
    private void Clear()
    {
        map.ClearAllTiles();
        for(int i = obstacleRoot.childCount - 1; i >= 0; i--)
        {
            Destroy(obstacleRoot.GetChild(i).gameObject);
        }
        for (int i = treasureRoot.childCount - 1; i >= 0; i--)
        {
            Destroy(treasureRoot.GetChild(i).gameObject);
        }
    }
    public static LevelMap LoadMap(int index)
    {
        var l = FindObjectOfType<MapBuilder>();
        return l.levels.levels[index].map;
    }
}
