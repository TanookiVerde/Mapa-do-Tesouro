using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScrollEffect : MonoBehaviour {
    private const float DURATION = 0.5f;
    private float openedSize;
    public bool randomize;

    [SerializeField] private RectTransform rect;
    [SerializeField] private CanvasGroup content;

    private void Start()
    {
        openedSize = rect.sizeDelta.x;
    }
    public void Open(float duration = DURATION)
    {
        duration += randomize ? Random.Range(0, 0.1f) : 0;
        rect.DOSizeDelta(new Vector2(0, rect.sizeDelta.y), 0);
        rect.DOSizeDelta(new Vector2(openedSize, rect.sizeDelta.y), duration);
    }
    public void Close(float duration = DURATION)
    {
        duration += randomize ? Random.Range(0, 0.1f) : 0;
        rect.DOSizeDelta(new Vector2(0, rect.sizeDelta.y), duration);
    }
}
