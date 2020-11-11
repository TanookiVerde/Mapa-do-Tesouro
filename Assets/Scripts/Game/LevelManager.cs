using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static List<BeginBlock> beginBlocks = new List<BeginBlock>();
    public static LevelManager me;

    [Header("Managers")]
    [SerializeField] private MapBuilder mapBuilder;
    [SerializeField] private ObstacleMap obstacleMap;
    [SerializeField] private GroundMap groundMap;

    [Header("Prefabs")]
    [SerializeField] private GameObject beginBlock;
    [SerializeField] private GameObject player;

    [Header("Misc")]
    [SerializeField] private Transform beginBlockRoot;
    [SerializeField] private List<Color> playerColors;

    [Header("LevelMaps")]
    [SerializeField] public LevelList levels;

    private void Start () {
        FindObjectOfType<BlockFitter>().UpdateSize();
        me = this;
        var level = levels.levels[MemoryCard.GetSelectedLevel()];
        if(level.tutorial.Count > 0)
            FindObjectOfType<TutorialPanel>().OpenTutorial(level.tutorial);
        CreatePlayer();
        FindObjectOfType<BlockPanel>().Create(level);
        mapBuilder.BuildMap();
        transform.position = (Vector3) levels.levels[MemoryCard.GetSelectedLevel()].cameraInitialPosition - Vector3.forward*10;
    }
    private void CreatePlayer()
    {
        beginBlocks = new List<BeginBlock>();
        LevelMap level = MapBuilder.LoadMap(MemoryCard.GetSelectedLevel());
        for(int i = 0; i < level.initialPosition.Count;i++)
        {
            var p = Instantiate(player).GetComponent<Player>();
            p.SetLook(0,-1);
            p.SetPosition((int)level.initialPosition[i].x, (int)level.initialPosition[i].y);
            p.SetColor(playerColors[i]);
            var b = Instantiate(beginBlock, beginBlockRoot).GetComponent<BeginBlock>();
            b.player = p;
            b.SetColor(playerColors[i]);
            beginBlocks.Add(b);
        }
    }
}
