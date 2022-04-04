using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVStartEF : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        spriteRenderer.color = new Color(1, 1, 1, 0);
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// œ‘ æ◊‘…Ì
    /// </summary>
    public void Show()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        Debug.Log("3");
        gameObject.SetActive(true);
        animator.speed = 0.5f;
        animator.Play("LVStartEF", 0, 0);
    }
}
