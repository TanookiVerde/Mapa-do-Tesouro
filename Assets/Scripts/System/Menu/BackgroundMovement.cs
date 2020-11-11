using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update () {
        spriteRenderer.size += new Vector2(speed, 0) * Time.deltaTime ;
	}
}
