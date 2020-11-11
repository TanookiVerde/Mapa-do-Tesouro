using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalBarrel : Obstacle, ICannonBulletTarget, IBulletTarget {

    public override bool OnPush(Vector2Int direction)
    {
        if (direction.y == 0 && Mathf.Abs(direction.x) == 1)
        {
            return Move(direction);
        }
        return false;
    }
    public IEnumerator OnCannonBulletTouch()
    {
        ObstacleMap.RemoveObstacle(this);
        yield return null;
        Destroy(this.gameObject);
    }
    public IEnumerator OnBulletTouch()
    {
        yield return null;
    }
    public override IEnumerator TurnUpdate()
    {
        if (!FindObjectOfType<GroundMap>().IsSandTile(position))
        {
            Compiler.AddBarrelOnWater(false, position);
        }
        yield return null;
    }
}
