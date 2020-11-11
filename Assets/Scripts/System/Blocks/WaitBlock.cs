﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitBlock : Block {
    public override IEnumerator Execute(Player player)
    {
        StartCoroutine(base.ExecutionAnimation());
        yield return player.Wait();
    }
}
