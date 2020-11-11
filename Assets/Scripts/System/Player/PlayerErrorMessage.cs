using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerErrorMessage : MonoBehaviour {
    public Transform messageTransform;

	public void ShowError()
    {
        messageTransform.DOLocalMoveY(1, 0.1f);
        GetComponent<SpriteRenderer>().DOFade(1, 0.1f);
        GetComponent<AudioSource>().Play();
    }
    public void HideError()
    {
        messageTransform.DOLocalMoveY(0, 0.1f);
        GetComponent<SpriteRenderer>().DOFade(0, 0.1f);
    }
}
