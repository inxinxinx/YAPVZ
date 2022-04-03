using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHead : MonoBehaviour
{
    private Animator animator;
    private bool isOver;

    private void Start()
    {
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !isOver)
        {
            isOver = true;
            animator.speed = 0;
            Destroy(gameObject, 2);
        }
    }
}
