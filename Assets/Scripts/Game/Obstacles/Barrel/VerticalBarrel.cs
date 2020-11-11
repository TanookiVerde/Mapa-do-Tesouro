using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalBarrel : Obstacle, ICannonBulletTarget, IBulletTarget
{
    public override bool OnPush(Vector2Int direction)
    {
        if (direction.x == 0 && Mathf.Abs(direction.y) == 1)
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
            Compiler.AddBarrelOnWater(true, position);
        }
        yield return null;
    }
}