using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookBlock : Block {
    public Vector2Int direction;

    public override IEnumerator Execute(Player player)
    {
        StartCoroutine(base.ExecutionAnimation());
        yield return player.Look(direction);
    }
}
