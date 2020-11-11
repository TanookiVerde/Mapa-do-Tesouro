using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelMap {
    public List<Vector2> initialPosition = new List<Vector2>();
    public List<Vector2> borders = new List<Vector2>();
    public List<Vector2> ground = new List<Vector2>();
    public List<Vector2> woodenBox = new List<Vector2>();
    public List<Vector2> pointyRocks = new List<Vector2>();
    public List<Vector2> verticalBarrel = new List<Vector2>();
    public List<Vector2> horizonalBarrel = new List<Vector2>();
    public List<Vector2> verticalRaft = new List<Vector2>();
    public List<Vector2> horizontalRaft = new List<Vector2>();
    public List<Vector2> upCannon = new List<Vector2>();
    public List<Vector2> downCannon = new List<Vector2>();
    public List<Vector2> leftCannon = new List<Vector2>();
    public List<Vector2> rightCannon = new List<Vector2>();
    public List<Vector2> treasures = new List<Vector2>();
    public List<Vector2> bigTreasures = new List<Vector2>();
}
