using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Screen : MonoBehaviour {
    private const float DURATION = 0.25f;
    private CanvasGroup canvasGroup;

	public virtual void Show(float duration = DURATION) {
        if(canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.DOFade(1, duration);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        OnShow();
    }
    public virtual void Hide(float duration = DURATION) {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0, duration);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        OnHide();
    }
    public virtual void OnShow() { }
    public virtual void OnHide() { }
}
