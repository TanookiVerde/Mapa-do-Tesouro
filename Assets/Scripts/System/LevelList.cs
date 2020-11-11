using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/LevelList")]
public class LevelList : ScriptableObject {
    public List<LevelData> levels;
}
