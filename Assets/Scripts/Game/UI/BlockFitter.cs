using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BlockFitter : MonoBehaviour {
    private const float BLOCK_SIZE = 95;
    [SerializeField] private RectTransform contentRect;
    [SerializeField] private ScrollRect scroll;

    public void UpdateSize()
    {
        int bigger = GetBiggerCommandSequence();
        Fit(bigger);
    }
    private void Fit(int blocksCount)
    {
        float total = blocksCount * BLOCK_SIZE;
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, total);
    }
    public void ShowBlock(int blockIndex)
    {
        int total = GetBiggerCommandSequence();
        float perc = (float) blockIndex / total;
        scroll.DONormalizedPos(Vector2.up * perc, 0.25f);
    }
    private int GetBiggerCommandSequence()
    {
        int bigger = 0;
        foreach (var b in FindObjectsOfType<BeginBlock>())
        {
            if (b.GetCommandList().Count > bigger)
                bigger = b.GetCommandList().Count;
        }
        return bigger;
    }
}
