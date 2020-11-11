using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginBlock : Block {
    public Player player;
    public Image color;

    public override IEnumerator Execute(Player player)
    {
        yield return null;
    }
    public void SetColor(Color c)
    {
        color.color = c;
    }
    public Program GetProgram()
    {
        if(next != null)
        {
            var p = new Program();
            p.blocks = next.GetCommandList();
            p.player = player;
            p.blocks.Reverse();
            p.length = p.blocks.Count;
            return p;
        }
        else
            return null;
    }
}
