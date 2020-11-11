using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Jewel : MonoBehaviour {
    [SerializeField] private float yOffset;

	public void Start()
    {
        transform.DOMoveY(transform.position.y + yOffset, 0.5f);
        transform.DOScale(1.5f, 0.5f);
        Destroy(this.gameObject, 1f);
    }
}
