using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class plantbase : MonoBehaviour
{

    protected int hp;
    public bool beingAttacked;

    protected GridList curGrid;

    protected Animator animator;
    public SpriteRenderer spriteRenderer;

    private PlantType plantType;

    public int Hp { 
        get => hp;
        protected set
        {
            hp = value;
            HpUpdateEvent();
        }
    }

    public void getHurt(int atkValue)
    {
        Hp -= atkValue;
        StartCoroutine(ColorEF(0.5f, new Color(0.6f, 0.6f, 0.6f, 0.9f), 0.05f, null));
        if (Hp <= 0)
        {
            Dead();
        }
    }
    
    public void InitialForall(PlantType type)
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        plantType = type;
        if(type != PlantType.WallNut)
        {
            transform.localScale = new Vector3(1.6f, 1.6f, 1f);
        }
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void InitForCreate(bool inGrid, PlantType type)
    {
        InitialForall(type);
        animator.speed = 0;
        if (inGrid)
        {
            spriteRenderer.sortingOrder = -1;
            spriteRenderer.color = new Color(1, 1, 1, 0.65f);
            return;
        }
        spriteRenderer.sortingOrder = 1;
    }

    public void InitForPlant(GridList grid, PlantType type)
    {
        animator.speed = 1;
        spriteRenderer.sortingOrder = 0;
        curGrid = grid;
        
        grid.CurrPlantBase = this;

        OnInitForPlant();
    }

    public void Dead()
    {
        if(curGrid != null)
        {
            curGrid.CurrPlantBase = null;
            curGrid = null;
        }
        //AudioManager.Instance.PlayEFAudio(LevelManager.instance.gameConf.);

        StopAllCoroutines();
        CancelInvoke();
        PoolManager.Instance.PushObj(PlantManager.instance.GetPlantByType(plantType), gameObject);
    }

    public virtual void OnInitForPlant() {}

    protected virtual void HpUpdateEvent() { }

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
