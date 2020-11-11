using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelSelectionScreen : Screen {
    private const float TITLE_OPENED_X = 200;

    [SerializeField] private LevelList data;
    [SerializeField] private Transform buttonRoot;
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private RectTransform title;

    public override void OnShow()
    {
        title.DOSizeDelta(new Vector2(0, title.sizeDelta.y), 0);
        title.DOSizeDelta(new Vector2(TITLE_OPENED_X, title.sizeDelta.y), 0.5f);
        StartCoroutine(ShowLevelButtons());
    }
    public override void OnHide()
    {
        title.DOSizeDelta(new Vector2(0, title.sizeDelta.y), 0.5f);
        for (int i = buttonRoot.childCount-1; i >= 0; i--)
        {
            Destroy(buttonRoot.GetChild(i).gameObject);
        }
    }
    private IEnumerator ShowLevelButtons()
    {
        var memory = MemoryCard.Load();
        if (memory.levels.Count == 0)
        {
            memory.levels.Add(new LevelProgress());
            memory.Save();
        }
        for (int i = LastUnlockedLevel(memory.levels)-1; i >= 0; i--)
        {
            var go = Instantiate(levelButtonPrefab, buttonRoot);
            go.GetComponent<LevelButton>().SetData(data.levels[i], memory.levels[i], i);
        }
        yield return null;
    }
    private int LastUnlockedLevel(List<LevelProgress> progress)
    {
        print(data.levels.Count);
        return Mathf.Clamp(progress.Count, 1, data.levels.Count);
    }
}
