using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour {

	public IEnumerator Shoot(Vector2Int init, Vector2Int target)
    {
        Vector3 i = new Vector3(init.x + 0.5f, init.y + 0.5f, 0);
        Vector3 f = new Vector3(target.x + 0.5f, target.y + 0.5f, 0);
        transform.DOMove(i, 0);
        transform.DOMove(f, Globals.TIME_BETWEEN_TURNS).SetEase(Ease.Linear);
        yield return new WaitForSeconds(Globals.TIME_BETWEEN_TURNS);
        if (ObstacleMap.ObstacleIn(target) != null && ObstacleMap.ObstacleIn(target).GetComponent<IBulletTarget>() != null)
        {
            yield return ObstacleMap.ObstacleIn(target).GetComponent<IBulletTarget>().OnBulletTouch();
        }
        Destroy(this.gameObject);
    }
}
