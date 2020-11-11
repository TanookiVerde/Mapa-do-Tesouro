using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/BlockList")]
public class BlockList : ScriptableObject {
    public List<BlockData> blockDatas;
}
