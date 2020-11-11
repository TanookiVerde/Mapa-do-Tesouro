using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/BlockData")]
public class BlockData : ScriptableObject {
    public string label;
    public Sprite icon;
    public Color color;
}
