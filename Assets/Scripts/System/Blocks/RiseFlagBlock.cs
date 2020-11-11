using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseFlagBlock : Block {

    public override IEnumerator Execute(Player player)
    {
        StartCoroutine(base.ExecutionAnimation());
        yield return player.RiseFlag();
    }
}
