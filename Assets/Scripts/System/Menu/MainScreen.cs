using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : Screen {
    [SerializeField] private List<ScrollEffect> scrolls;

    public override void OnShow()
    {
        foreach (var s in scrolls)
            s.Open();
    }
    public override void OnHide()
    {
        foreach (var s in scrolls)
            s.Close();
    }
}
