using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBox : Obstacle, ICannonBulletTarget, IBulletTarget
{
    public IEnumerator OnBulletTouch()
    {
        yield return null;
    }
    public IEnumerator OnCannonBulletTouch()
    {
        ObstacleMap.RemoveObstacle(this);
        yield return null;
        Destroy(this.gameObject);
    }
}