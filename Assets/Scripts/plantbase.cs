using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class plantbase : MonoBehaviour
{

    protected int hp;
    public bool beingAttacked;


    protected Animator animator;
    public SpriteRenderer spriteRenderer;

    public int Hp { 
        get => hp; 
        //set => hp = value; 
    }

    public void getHurt(int atkValue)
    {
        hp -= atkValue;
        StartCoroutine(ColorEF(0.5f, new Color(0.6f, 0.6f, 0.6f, 0.9f), 0.05f, null));
        if (Hp <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
    
    public void Find()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void InitForCreate(bool inGrid)
    {
        Find();
        animator.speed = 0;
        if (inGrid)
        {
            spriteRenderer.sortingOrder = -1;
            //Debug.Log("color");
            spriteRenderer.color = new Color(1, 1, 1, 0.65f);
        }

    }

    public void InitForPlant(GridList grid, bool inGrid)
    {
        animator.speed = 1;
        spriteRenderer.sortingOrder = 0;
        grid.CurrPlantBase = this;

        OnInitForPlant();
    }

    public virtual void OnInitForPlant()
    {

    }

    protected IEnumerator ColorEF(float wantTime, Color targetColor, float delayTime, UnityAction fun)
    {
        float currTime = 0;
        float lerp;
        while (currTime < wantTime)
        {
            yield return new WaitForSeconds(delayTime);
            lerp = currTime / wantTime;
            currTime += delayTime;
            spriteRenderer.color = Color.Lerp(Color.white, targetColor, lerp);
        }
        spriteRenderer.color = Color.white;
        if (fun != null) fun();
    }

}
