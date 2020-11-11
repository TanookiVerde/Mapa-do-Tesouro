using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    private Animator animator;

    public void ChangeAnimation(Vector2Int look)
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        if(look == Vector2Int.up)
        {
            animator.Play("player_idle_up");
        }else if (look == Vector2Int.right)
        {
            animator.Play("player_idle_right");
        }
        else if (look == Vector2Int.down)
        {
            animator.Play("player_idle_down");
        }
        else if (look == Vector2Int.left)
        {
            animator.Play("player_idle_left");
        }
    }
    public void DigAnimation()
    {
        animator.Play("player_dig_down");
    }
}
