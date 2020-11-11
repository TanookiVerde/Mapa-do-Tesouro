using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class EndLevelPanel : MonoBehaviour {
    [SerializeField] private TMP_Text bigTreasure;
    [SerializeField] private TMP_Text smallTreasures;
    [SerializeField] private TMP_Text steps;

    [SerializeField] private CanvasGroup group;
    [SerializeField] private RectTransform panel;

    public LevelList levels;

    public void OpenPanel(TreasureInfo info, int steps)
    {
        CameraMovement.canBeMoved++;
        group.DOFade(1, 0.5f);
        group.interactable = true;
        group.blocksRaycasts = true;
        panel.DOAnchorPosY(-680, 0);
        panel.DOAnchorPosY(40, 0.5f);

        var level = LevelManager.me.levels.levels[MemoryCard.GetSelectedLevel()];
        bigTreasure.text = "<color=red>" + info.bigTreasures.Count + "</color> de " + level.map.bigTreasures.Count;
        smallTreasures.text = "<color=red>" + info.littleTreasures.Count + "</color> de " + level.map.treasures.Count;
        this.steps.text = "<color=red>" + steps + "</color> de " + level.maxSteps;

        SaveProgress(info, steps);
    }
    public void TryAgain()
    {
        CameraMovement.canBeMoved--;
        group.DOFade(0, 0.5f);
        group.interactable = false;
        group.blocksRaycasts = false;
        panel.DOAnchorPosY(-680, 0.5f);
    }
    public void OpenMenu()
    {
        LoadingScreen.LoadScreen("MainMenu");
        CameraMovement.canBeMoved = 0;
    }
    public void SaveProgress(TreasureInfo info, int steps)
    {
        var memory = MemoryCard.Load();
        print("A: " + memory.levels[MemoryCard.GetSelectedLevel()].bigTreasures);
        if (memory.levels[MemoryCard.GetSelectedLevel()].bigTreasures == 0)
        {
            print("NEW LEVEL UNLOCKED");
            memory.levels.Add(new LevelProgress());
        }

        memory.levels[MemoryCard.GetSelectedLevel()].steps = steps < memory.levels[MemoryCard.GetSelectedLevel()].steps ?
            steps : memory.levels[MemoryCard.GetSelectedLevel()].steps;

        memory.levels[MemoryCard.GetSelectedLevel()].treasures = info.littleTreasures.Count > memory.levels[MemoryCard.GetSelectedLevel()].treasures ?
            info.littleTreasures.Count : memory.levels[MemoryCard.GetSelectedLevel()].treasures;

        memory.levels[MemoryCard.GetSelectedLevel()].bigTreasures = info.bigTreasures.Count > memory.levels[MemoryCard.GetSelectedLevel()].bigTreasures ?
            info.bigTreasures.Count : memory.levels[MemoryCard.GetSelectedLevel()].bigTreasures;

        memory.Save();
    }
}
