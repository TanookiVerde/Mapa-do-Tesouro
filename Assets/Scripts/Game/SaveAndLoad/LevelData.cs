using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/LevelData")]
public class LevelData : ScriptableObject {
    [Header("General")]
    public string title;
    public Sprite thumbnail;
    public LevelMap map;
    public int maxSteps;
    public List<TutorialInfo> tutorial;
    public Vector2 cameraInitialPosition;

    [Header("Available Blocks")]
    public bool wait;
    public bool shoot;
    public bool riseFlag;
}
