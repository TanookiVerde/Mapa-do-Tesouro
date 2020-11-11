using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HideOnExecution : MonoBehaviour {
    [SerializeField] private float yOffset;
    private float originalY;

    private void Start()
    {
        originalY = GetComponent<RectTransform>().anchoredPosition.y;
    }
    public void Show()
    {
        GetComponent<CanvasGroup>().interactable = true;
        var rect = GetComponent<RectTransform>();
        rect.DOAnchorPosY(originalY, 0.25f);
    }
    public void Hide()
    {
        GetComponent<CanvasGroup>().interactable = false;
        var rect = GetComponent<RectTransform>();
        rect.DOAnchorPosY(originalY - yOffset, 0.25f);
    }
}
