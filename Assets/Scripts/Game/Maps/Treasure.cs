using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour {
    public bool bigTreasure;
    public bool found;
    public Vector2Int position;
    public GameObject jewelPrefab;

    public void GetPosition()
    {
        position = new Vector2Int((int)(transform.position.x - 0.5f), (int)(transform.position.y - 0.5f));
    }
    public void Find()
    {
        found = true;
        GetComponent<AudioSource>().Play();
        var j = Instantiate(jewelPrefab);
        j.transform.position = transform.position;
    }
    public void ResetState()
    {
        found = false;
    }
}
