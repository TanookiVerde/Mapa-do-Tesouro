using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Vector2Int position;
    public Vector2Int lookDirection;

    public GameObject bulletPrefab;
    public SpriteRenderer color;

    public bool isWaiting;
    public bool flag;

    public void SetColor(Color c)
    {
        color.color = c;
    }
    public void SetPosition(int x, int y)
    {
        position.x = x;
        position.y = y;
        var targetPosition = new Vector3(position.x, position.y, 0);
        transform.position = (targetPosition + new Vector3(0.5f, 0.5f, 0)) * Globals.TILE_SIZE;
    }
    public void SetLook(int x, int y)
    {
        lookDirection.x = x;
        lookDirection.y = y;
        transform.GetChild(0).GetComponent<PlayerAnimation>().ChangeAnimation(lookDirection);
    }
    public IEnumerator Move()
    {
        position += lookDirection;
        var targetPosition = new Vector3(position.x, position.y, 0);
        transform.DOMove(targetPosition + new Vector3(0.5f, 0.5f, 0) * Globals.TILE_SIZE, Globals.TIME_BETWEEN_TURNS);
        yield return null;
    }
    public IEnumerator Walk()
    {
        transform.GetChild(0).GetComponent<PlayerAnimation>().ChangeAnimation(lookDirection);
        isWaiting = false;
        if (FindObjectOfType<GroundMap>().IsBorderTile(position + lookDirection))
            yield return null;
        if (ObstacleMap.ObstacleIn(position + lookDirection) == null)
        {
            if (FindObjectOfType<GroundMap>().IsSandTile(position + lookDirection))
            {
                position += lookDirection;
                var targetPosition = new Vector3(position.x, position.y, 0);
                transform.DOMove(targetPosition + new Vector3(0.5f, 0.5f, 0) * Globals.TILE_SIZE, Globals.TIME_BETWEEN_TURNS);
            }
            else
            {
                ErrorHandling.AddError(new Error(Compiler.steps, "WALKING ON WATER"));
                transform.GetChild(2).GetComponent<PlayerErrorMessage>().ShowError();
            }
        }
        else
        {
            if (ObstacleMap.ObstacleIn(position + lookDirection).OnPush(lookDirection))
            {
                position += lookDirection;
                var targetPosition = new Vector3(position.x, position.y, 0);
                transform.DOMove(targetPosition + new Vector3(0.5f, 0.5f, 0) * Globals.TILE_SIZE, Globals.TIME_BETWEEN_TURNS);
            }
            else
            {
                ErrorHandling.AddError(new Error(Compiler.steps, "PUSHING OBSTACLE"));
                transform.GetChild(2).GetComponent<PlayerErrorMessage>().ShowError();
            }
        }
        yield return null;
    }
    public IEnumerator Wait()
    {
        isWaiting = true;
        yield return null;
    }
    public IEnumerator Shoot()
    {
        GetComponent<AudioSource>().Play();
        isWaiting = false;
        Vector2Int bulletPosition = position;
        for (int i = 0; i < Globals.BULLET_RANGE; i++)
        {
            bulletPosition += lookDirection;
            var obstacle = ObstacleMap.ObstacleIn(bulletPosition);
            if (obstacle != null && obstacle.GetComponent<IBulletTarget>() != null)
            {
                print("Bullet Start at <" + position.x + "," + position.y +
                    "> until reaches <" + bulletPosition.x + "," + bulletPosition.y + ">");
                break;
            }
        }
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        yield return bullet.GetComponent<Bullet>().Shoot(position, bulletPosition);
        yield return null;
    }
    public IEnumerator Look(Vector2Int direction)
    {
        isWaiting = false;
        lookDirection = direction;
        transform.GetChild(0).GetComponent<PlayerAnimation>().ChangeAnimation(direction);
        yield return null;
    }
    public IEnumerator RiseFlag()
    {
        isWaiting = false;
        flag = true;
        yield return null;
    }
    public IEnumerator DropFlag()
    {
        isWaiting = false;
        flag = false;
        yield return null;
    }
    public bool IsAbove(Vector2Int position)
    {
        return this.position == position;
    }
    public IEnumerator Dig()
    {
        transform.GetChild(0).GetComponent<PlayerAnimation>().DigAnimation();
        var treasure = TreasureMap.TreasureIn(position);
        if (treasure != null && treasure.found == false)
        {
            treasure.Find();
        }
        else
        {
            ErrorHandling.AddError(new Error(Compiler.steps, "DIGGING IN WRONG PLACE"));
            transform.GetChild(2).GetComponent<PlayerErrorMessage>().ShowError();
        }
        yield return null;
    }
}
