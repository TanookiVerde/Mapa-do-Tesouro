using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Obstacle, IBulletTarget {
    [SerializeField] private GameObject cannonBulletPrefab;
    [SerializeField] private CannonType type;
    [SerializeField] private List<Sprite> cannonDirections;
    private Vector2Int direction;

    public void SetType(CannonType type)
    {
        switch (type)
        {
            case CannonType.UP:
                direction = Vector2Int.up;
                break;
            case CannonType.DOWN:
                direction = Vector2Int.down;
                break;
            case CannonType.LEFT:
                direction = Vector2Int.left;
                break;
            case CannonType.RIGHT:
                direction = Vector2Int.right;
                break;
        }
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = cannonDirections[(int)type];
    }
    public IEnumerator OnBulletTouch()
    {
        yield return ShootBullet();
    }
    public IEnumerator ShootBullet()
    {
        GetComponent<AudioSource>().Play();
        Vector2Int bulletPosition = position;
        for (int i = 0; i < Globals.BULLET_RANGE; i++)
        {
            bulletPosition += direction;
            var obstacle = ObstacleMap.ObstacleIn(bulletPosition);
            if (obstacle != null && obstacle.GetComponent<ICannonBulletTarget>() != null)
            {
                print("Cannon Bullet Start at <" + position.x + "," + position.y +
                    "> until reaches <" + bulletPosition.x + "," + bulletPosition.y + ">");
                break;
            }
        }
        var bullet = Instantiate(cannonBulletPrefab, transform.position, Quaternion.identity);
        yield return bullet.GetComponent<CannonBullet>().Shoot(position, bulletPosition);
    }
}
public enum CannonType
{
    UP, DOWN, LEFT, RIGHT
}
