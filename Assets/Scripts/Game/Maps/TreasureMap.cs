using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureMap : MonoBehaviour {
    public static Dictionary<Vector2Int, Treasure> treasures = new Dictionary<Vector2Int, Treasure>();

    public static void LoadMap()
    {
        treasures = new Dictionary<Vector2Int, Treasure>();
        var tr = FindObjectsOfType<Treasure>();
        foreach (Treasure t in tr)
        {
            t.ResetState();
            treasures.Add(t.position, t);
        }
    }
    public static Treasure TreasureIn(Vector2Int position)
    {
        if (treasures.ContainsKey(position))
            return treasures[position];
        return null;
    }
    public static TreasureInfo GetTreasureInfo()
    {
        TreasureInfo ti = new TreasureInfo();
        foreach(Treasure t in treasures.Values)
        {
            if (t.bigTreasure)
                ti.bigTreasures.Add(t);
            else if(t.found)
                ti.littleTreasures.Add(t);
        }
        return ti;
    }
    public static bool FinishLevel()
    {
        foreach(var t in treasures.Values)
        {
            if (t.bigTreasure && !t.found)
                return false;
        }
        return true;
    }
}
public class TreasureInfo
{
    public List<Treasure> bigTreasures = new List<Treasure>();
    public List<Treasure> littleTreasures = new List<Treasure>();
}
