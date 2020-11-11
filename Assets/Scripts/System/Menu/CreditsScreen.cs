using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScreen : Screen {
    [SerializeField] private ScrollEffect title;

    public override void OnShow()
    {
        title.Open();
        base.OnShow();
    }
    public override void OnHide()
    {
        title.Close();
        base.OnHide();
    }
}
