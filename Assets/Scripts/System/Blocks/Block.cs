using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public abstract class Block : MonoBehaviour, IDragHandler, IBeginDragHandler, IDropHandler, IEndDragHandler{
    private const float OFFSET = 20;

    public static Block dragged;

    [HideInInspector] public Block previous;
    [HideInInspector] public Block next;
    [HideInInspector] public bool initialBlock;
    public bool onPanelBlock;
    public BlockData data;

    private Transform blockRoot;
    private RectTransform rect;

    public abstract IEnumerator Execute(Player player);

    private void Start()
    {
        if(!initialBlock)
            GetComponent<Image>().color = data.color;
        rect = GetComponent<RectTransform>();
        blockRoot = FindObjectOfType<Canvas>().transform;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        CameraMovement.canBeMoved++;
        if (onPanelBlock)
        {
            int siblingIndex = transform.GetSiblingIndex();
            transform.SetParent(blockRoot);
            var go = Instantiate(this.gameObject, GameObject.Find("Blocks List").transform);
            go.transform.SetSiblingIndex(siblingIndex);
            GetComponent<Image>().raycastTarget = false;
            dragged = this;
            onPanelBlock = false;
            CorrectBlockAnchor();
        }
        else
        {
            if (previous != null)
            {
                transform.SetParent(blockRoot);
                previous.SetNextBlock(null);
                previous = null;
            }
            GetComponent<Image>().raycastTarget = false;
            dragged = this;
        }
        SetBlockAlpha();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(!initialBlock)
            rect.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        SetBlockAlpha();
        CameraMovement.canBeMoved--;
        GetComponent<Image>().raycastTarget = true;
        dragged = null;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (onPanelBlock)
        {
            CameraMovement.canBeMoved--;
            if (Block.dragged != null)
            {
                Destroy(Block.dragged.gameObject);
            }
        }
        else if(!Block.dragged.initialBlock)
        {
            GetComponent<AudioSource>().Play();
            if (next == null)
            {
                dragged.SetPreviousBlock(this);
                SetNextBlock(dragged);
            }
            else
            {
                dragged.SetPreviousBlock(this);
                dragged.SetNextBlock(next);
                next.SetPreviousBlock(dragged);
                SetNextBlock(dragged);
            }
        }
        FindObjectOfType<BlockFitter>().UpdateSize();
    }
    public void SetNextBlock(Block block)
    {
        next = block;
        if (next != null)
        {
            next.SetPreviousBlock(this);
            next.rect.anchoredPosition = Vector2.down * (rect.sizeDelta.y - OFFSET);
        }
    }
    public void SetPreviousBlock(Block block)
    {
        previous = block;
        transform.SetParent(previous.transform);
    }
    public List<Block> GetCommandList()
    {
        if (next != null)
        {
            var list = next.GetCommandList();
            list.Add(this);
            return list;
        }
        else
        {
            var list = new List<Block>();
            list.Add(this);
            return list;
        }
    }
    protected IEnumerator ExecutionAnimation()
    {
        Color originalColor = GetComponent<Image>().color;
        GetComponent<Image>().DOColor(Color.black, Globals.TIME_BETWEEN_TURNS);
        yield return new WaitForSeconds(Globals.TIME_BETWEEN_TURNS);
        GetComponent<Image>().DOColor(originalColor, Globals.TIME_BETWEEN_TURNS);
    }
    private void CorrectBlockAnchor()
    {
        rect.DOSizeDelta(new Vector2(100, 100), 0.25f);
        rect.anchorMin = new Vector2(0.5f, 1f);
        rect.anchorMax = new Vector2(0.5f, 1f);
        rect.pivot = new Vector2(0.5f, 1f);
    }
    private void SetBlockAlpha()
    {
        if (previous == null)
            GetComponent<CanvasGroup>().DOFade(0.75f, 0);
        else
            GetComponent<CanvasGroup>().DOFade(1, 0);
    }
}
