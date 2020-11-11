using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointyRocks : Obstacle, IBulletTarget, ICannonBulletTarget {
    public IEnumerator OnBulletTouch()
    {
        yield return null;
    }
    public IEnumerator OnCannonBulletTouch()
    {
        yield return null;
    }
}
