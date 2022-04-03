using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHead : MonoBehaviour
{
    private Animator animator;
    private bool isOver;

    public void Init(Vector3 pos)
    {   
        animator = GetComponent<Animator>();
        transform.position = pos;
        isOver = false;
        animator.speed = 1;
        animator.Play("LostHead", 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !isOver)
        {
            isOver = true;
            animator.speed = 0;
            Invoke("Dead", 2);
        }
    }

    private void Dead()
    {
        CancelInvoke();
        PoolManager.Instance.PushObj(LevelManager.instance.gameConf.ZombieHead, gameObject);
    }
}
