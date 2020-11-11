using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockFactory : MonoBehaviour, IDropHandler {

    public GameObject blockPrefab;

    public void Create()
    {
        Instantiate(blockPrefab, FindObjectOfType<Canvas>().transform);
    }
    public void OnDrop(PointerEventData eventData)
    {
        CameraMovement.canBeMoved--;
        if (Block.dragged != null)
        {
            Destroy(Block.dragged.gameObject);
        }
    }
}
