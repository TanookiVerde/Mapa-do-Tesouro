using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {
    [HideInInspector] public int levelIndex;
    [SerializeField] private TMP_Text index;
    [SerializeField] private TMP_Text title;
    [SerializeField] private Image thumbnail;

    [SerializeField] private TMP_Text bigTreasureValue;
    [SerializeField] private TMP_Text treasuresValue;
    [SerializeField] private TMP_Text maxStepsValue;

    public void SetData(LevelData levelData, LevelProgress levelProgress, int index)
    {
        this.levelIndex = index;
        this.index.text = (index + 1).ToString();
        this.title.text = levelData.title;
        this.thumbnail.sprite = levelData.thumbnail;

        this.bigTreasureValue.text = IsLevelCompleted(levelProgress, levelData) ? 
            levelProgress.bigTreasures +"/"+levelData.map.bigTreasures.Count 
            : "-/" + levelData.map.bigTreasures.Count;

        this.treasuresValue.text = IsLevelCompleted(levelProgress, levelData) ?
            levelProgress.treasures + "/" + levelData.map.treasures.Count
            : "-/" + levelData.map.treasures.Count;

        this.maxStepsValue.text = IsLevelCompleted(levelProgress, levelData) ?
            levelProgress.steps + "/" + levelData.maxSteps
            : "-/" + levelData.maxSteps;
    }
    public void OpenLevel()
    {
        MemoryCard.SetSelectedLevel(levelIndex);
        print("Opening Level: " + levelIndex);
        LoadingScreen.LoadScreen("SampleScene");
    }
    private bool IsLevelCompleted(LevelProgress progress, LevelData data)
    {
        return progress.bigTreasures == data.map.bigTreasures.Count;
    }
}

