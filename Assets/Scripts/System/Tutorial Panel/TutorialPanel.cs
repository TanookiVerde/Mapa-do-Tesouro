using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class TutorialPanel : MonoBehaviour {
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Image image;

    [SerializeField] private CanvasGroup group;
    [SerializeField] private RectTransform panel;

    private int index;
    private List<TutorialInfo> info;

    public void OpenTutorial(List<TutorialInfo> info)
    {
        CameraMovement.canBeMoved++;
        this.info = info;
        index = 0;

        group.DOFade(1, 0.5f);
        group.interactable = true;
        group.blocksRaycasts = true;
        panel.DOAnchorPosY(-680, 0);
        panel.DOAnchorPosY(40, 0.5f);

        title.text = info[index].title;
        description.text = info[index].description;
        image.sprite = info[index].image;
    }
    public void Next()
    {
        index++;
        if (index >= info.Count)
        {
            Hide();
            return;
        }
        title.text = info[index].title;
        description.text = info[index].description;
        image.sprite = info[index].image;
    }
    public void Hide()
    {
        CameraMovement.canBeMoved--;
        group.DOFade(0, 0.5f);
        group.interactable = false;
        group.blocksRaycasts = false;
        panel.DOAnchorPosY(panel.anchoredPosition.y - 720, 0.5f);
    }
}
[System.Serializable]
public class TutorialInfo
{
    public string title;
    public string description;
    public Sprite image;
}
