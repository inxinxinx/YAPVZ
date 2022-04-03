using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ZombieState
{
    Idel,
    Walk,
    Atk,
    Dead
}

public class Zombie : MonoBehaviour
{
    //僵尸状态
    private ZombieState state;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isAttack = false;

    private int lineOfArray;
    private float lineOfPosY;

    public GridList curGrid;
    public GridList lastGrid;

    private string curWalkAnimationToPlay;
    private string curAtkAnimationToPlay;

    private int hp = 270;
    private float speed = 8;    //移速
    private int atk = 80;
    private float atkCD = 1f;

    private bool isHeadLost = false;

    public ZombieState State { 
        get => state;
        set
        {
            state = value;
            CheckState();
        }
    }

    public int Hp { get => hp;        
        set 
        { 
            hp = value;
            if(hp <= 60 && !isHeadLost)
            {
                //更改状态及动画
                isHeadLost = true;
                curWalkAnimationToPlay = "ZombieLostHead";
                curAtkAnimationToPlay = "ZombieLostHeadAtk";
                //生成头
                GameObject.Instantiate<GameObject>(LevelManager.instance.gameConf.ZombieHead, animator.transform.position, Quaternion.identity);
                CheckState();
            }
            if(hp > 0)
            {
                state = ZombieState.Dead;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        FSM();
    }

    public void init(int lineOfArray, int OrderNum)
    {
        Find();
        animator.speed = 0.7f;

        lineOfPosY = -3.75f + 1.63f * lineOfArray;
        getLineByVerticl(lineOfPosY);
        curWalkAnimationToPlay = "ZombieWalk";

        CheckOrder(OrderNum);
    }

    //单次状态检测
    private void CheckState()
    {
        switch (state)
        {
            case ZombieState.Idel:
                animator.Play("ZombieWalk", 0, 0);
                animator.speed = 0;
                break;
            case ZombieState.Walk:
                animator.Play(curWalkAnimationToPlay, 0, animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
                animator.speed = 0.7f;
                break;
            case ZombieState.Atk:
                animator.Play(curAtkAnimationToPlay, 0, animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
                animator.speed = 0.8f;
                break;
            case ZombieState.Dead:
                Dead();
                break;
        }
    }

    //持续状态检测
    private void FSM()
    {
        switch (State)
        {
            case ZombieState.Idel:              //直接跳到行走
                State = ZombieState.Walk;
                break;

            case ZombieState.Walk:              //行走，检测进攻击
                Move();
                break;

            case ZombieState.Atk:
                if (isAttack) break;
                Attack(curGrid.CurrPlantBase);
                break;

            case ZombieState.Dead:
                break;
        }
    }

    private void CheckOrder(int orderNum)
    {
        //越靠下层数越高
        int startNum = (4 - (int)curGrid.Point.y) * 100;
        spriteRenderer.sortingOrder = startNum + orderNum;
    }
 
    private void Find()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void getLineByVerticl(float verticalNum)
    {
        float posx = -6.75f + 9 * 1.33f;
        transform.position = new Vector3(posx, verticalNum + 0.3f, 0);
    }

    private void Move()
    {

        //if (isAttack) return;
        
        curGrid = GridManager.instance.getClosestGrid(transform.position);

        if (curGrid.HavePlant
            && curGrid.Position.x < transform.position.x
            && transform.position.x - curGrid.Position.x < 0.3f)
        {
            State = ZombieState.Atk;
            return;
        }

        transform.Translate((new Vector2(-1.33f, 0) * (Time.deltaTime / 1)) / speed);
    }

    private void Attack(plantbase plant)
    {
        isAttack = true;
        StartCoroutine(DoAtk(atkCD, plant));
    }

    IEnumerator DoAtk(float atkCD, plantbase plant)
    {
        while (plant.Hp > 0) 
        {
            yield return new WaitForSeconds(atkCD);
            plant.getHurt(atk / 2);
        }
        isAttack = false;
        State = ZombieState.Walk;
    }

    private void Dead()
    {
        ZombieManager.instance.RemoveZombie(this);
        Destroy(gameObject);
    }

    public void getHurt(int atkValue)
    {
        Hp -= atkValue;
        StartCoroutine(ColorEF(0.2f, new Color(0.6f, 0.6f, 0.6f), 0.05f, null));
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
