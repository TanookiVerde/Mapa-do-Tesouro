using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compiler : MonoBehaviour {
    private static List<Vector2> horizontalBarrelsOnWater;
    private static List<Vector2> verticalBarrelsOnWater;

    public static int steps = 0;

    public void Run()
    {
        FindObjectOfType<MapBuilder>().BuildMap();
        List<Program> programs = new List<Program>();
        foreach (var b in FindObjectsOfType<BeginBlock>())
        {
            var prog = b.GetProgram();
            if(prog != null)
                programs.Add(prog);
        }
        StartCoroutine(Execution(programs));
    }
    private IEnumerator Execution(List<Program> programs)
    {
        HideOnExecution(true);
        steps = 0;
        yield return null;
        TreasureMap.LoadMap();
        ResetPlayer();

        while (programs.Count > 0)
        {
            ErrorHandling.ResetError();
            steps++;

            horizontalBarrelsOnWater = new List<Vector2>();
            verticalBarrelsOnWater = new List<Vector2>();

            if (AllFlagsRised())
            {
                foreach (var p in FindObjectsOfType<Player>())
                    yield return p.DropFlag();
            }

            foreach (var e in ObstacleMap.obstacles)
                yield return e.Value.TurnStart();

            int playerIndex = GetRandomPlayerWithFlagNotRised(programs);
            FindObjectOfType<BlockFitter>().ShowBlock(programs[playerIndex].length - steps);

            CameraMovement.MoveTo(programs[playerIndex].player.position);
            yield return programs[playerIndex].blocks[0].Execute(programs[playerIndex].player);
            programs[playerIndex].blocks.Remove(programs[playerIndex].blocks[0]);
            if (programs[playerIndex].blocks.Count == 0)
                programs.Remove(programs[playerIndex]);

            foreach (var e in ObstacleMap.obstacles)
                yield return e.Value.TurnUpdate();

            yield return new WaitForSeconds(Globals.TIME_BETWEEN_TURNS);
            
            ObstacleMap.FixDictionary();
            SwapBarrelForRaft();

            if (ErrorHandling.errors.Count > 0)
                break;
        }
        HideOnExecution(false);
        yield return new WaitForSeconds(1f);
        if (TreasureMap.FinishLevel())
        {
            FindObjectOfType<EndLevelPanel>().OpenPanel(TreasureMap.GetTreasureInfo(), steps);
        }
        else
        {
            FindObjectOfType<MapBuilder>().BuildMap();
            ResetPlayer();
        }
    }
    private void ResetPlayer()
    {
        for (int i = 0; i < LevelManager.beginBlocks.Count; i++)
        {
            var p = MapBuilder.LoadMap(MemoryCard.GetSelectedLevel()).initialPosition[i];
            LevelManager.beginBlocks[i].player.SetPosition((int)p.x, (int)p.y);
            LevelManager.beginBlocks[i].player.SetLook(0, -1);
            LevelManager.beginBlocks[i].player.transform.GetChild(2).GetComponent<PlayerErrorMessage>().HideError();
        }
    }
    private int GetRandomPlayerWithFlagNotRised(List<Program> programs)
    {
        List<int> possible = new List<int>();
        for(int i = 0; i < programs.Count; i++)
        {
            if (!programs[i].player.flag)
                possible.Add(i);
        }
        return possible[Random.Range(0, possible.Count)];
    }
    private bool AllFlagsRised()
    {
        var players = FindObjectsOfType<Player>();
        for (int i = 0; i < players.Length; i++)
        {
            if (!players[i].flag)
                return false;
        }
        return true;
    }
    public static void AddBarrelOnWater(bool vertical, Vector2 position)
    {
        if (vertical)
            verticalBarrelsOnWater.Add(position);
        else
            horizontalBarrelsOnWater.Add(position);
    }
    private void SwapBarrelForRaft()
    {
        var mapBuilder = FindObjectOfType<MapBuilder>();

        foreach (var o in horizontalBarrelsOnWater)
        {
            GameObject obj = ObstacleMap.ObstacleIn(new Vector2Int((int)o.x, (int)o.y)).gameObject;
            ObstacleMap.obstacles.Remove(new Vector2Int((int)o.x, (int)o.y));
            Destroy(obj);
        }
        foreach (var o in verticalBarrelsOnWater)
        {
            GameObject obj = ObstacleMap.ObstacleIn(new Vector2Int((int)o.x, (int)o.y)).gameObject;
            ObstacleMap.obstacles.Remove(new Vector2Int((int)o.x, (int)o.y));
            Destroy(obj);
        }

        mapBuilder.CreateObstacle(mapBuilder.horizontalRaft, horizontalBarrelsOnWater);
        mapBuilder.CreateObstacle(mapBuilder.verticalRaft, verticalBarrelsOnWater);
    }
    private void HideOnExecution(bool hide)
    {
        foreach(var o in FindObjectsOfType<HideOnExecution>())
        {
            if (hide)
                o.Hide();
            else
                o.Show();
        }
    }
}
public class Program
{
    public Player player;
    public List<Block> blocks;
    public int length;
}
