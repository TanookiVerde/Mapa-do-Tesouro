using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalRaft : Obstacle{
    public Vector2Int direction = Vector2Int.right;

    private void Start()
    {
        transform.GetChild(0).transform.localPosition = (Vector2)direction * 0.6f;
        transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = direction.x < 0;
    }
    public override IEnumerator TurnUpdate()
    {
        foreach (var p in FindObjectsOfType<Player>())
        {
            if (CanMoveForward())
            {
                if (p.isWaiting && position == p.position)
                {
                    Move(direction);
                    yield return p.Look(direction);
                    yield return p.Move();
                }
            }
            else
                yield return null;
        }
    }
    public bool CanMoveForward()
    {
        var pos = position + direction;
        Vector3Int targetPosition = new Vector3Int(pos.x, pos.y, 0);
        var groundMap = FindObjectOfType<GroundMap>();
        bool canMove = !(groundMap.IsSandTile(targetPosition) || groundMap.IsBorderTile(pos));
        if (!canMove)
        {
            InvertDirection();
            targetPosition += new Vector3Int(direction.x, direction.y, 0) * 2;
            canMove = !(groundMap.IsSandTile(targetPosition) || groundMap.IsBorderTile(pos));
        }
        return canMove;
    }
    public override bool OnPush(Vector2Int direction)
    {
        return true;
    }
    public void InvertDirection()
    {
        direction *= -1;
        transform.GetChild(0).transform.localPosition = (Vector2)direction * 0.6f;
        transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = direction.x < 0;
    }
}
