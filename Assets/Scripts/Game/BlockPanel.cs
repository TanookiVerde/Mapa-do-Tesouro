using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockPanel : MonoBehaviour, IDropHandler
{
    [Header("Block Prefabs")]
    [SerializeField] private GameObject waitBlock;
    [SerializeField] private GameObject shootBlock;
    [SerializeField] private GameObject riseFlagBlock;

    public void Create(LevelData data)
    {
        if (data.shoot)
        {
            var go = Instantiate(shootBlock, transform);
            go.GetComponent<Block>().onPanelBlock = true;
        }
        if (data.wait)
        {
            var go = Instantiate(waitBlock, transform);
            go.GetComponent<Block>().onPanelBlock = true;
        }
        if (data.riseFlag)
        {
            var go = Instantiate(riseFlagBlock, transform);
            go.GetComponent<Block>().onPanelBlock = true;
        }
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
