using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {
    public static LoadingScreen me;
    private CanvasGroup canvasGroup;
    [SerializeField] private Transform icon;

    private void Start()
    {
        me = this;
    }
    public static void LoadScreen(string name)
    {
        me.transform.SetAsLastSibling();
        me.LoadSceneWithDelay(name, 1);
    }
    private void LoadSceneWithDelay(string name, float delay)
    {
        StartCoroutine(LoadScreenInSeconds(name, delay));
    }
    private IEnumerator LoadScreenInSeconds(string name, float delay)
    {
        me.Show();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadSceneAsync(name);
    }
    public void Show(float duration = 0.25f)
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.DOFade(1, duration);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    public void Hide(float duration = 0.25f)
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0, duration);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
