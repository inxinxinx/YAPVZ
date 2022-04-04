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

    private bool isAttack;

    private float lineOfPosY;

    public GridList curGrid;
    public GridList lastGrid;

    private string curWalkAnimationToPlay;
    private string curAtkAnimationToPlay;

    private int hp = 270;
    private float speed = 8;    //移速
    private int atk = 80;
    private float atkCD = 1f;

    private bool isHeadLost;
    private bool isOver = false;

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
                ZombieHead head = PoolManager.Instance.GetObj(LevelManager.instance.gameConf.ZombieHead).GetComponent<ZombieHead>();
                head.Init(transform.position);
                CheckState();
            }
            if(hp < 0)
            {
                State = ZombieState.Dead;
                ZombieDead death = PoolManager.Instance.GetObj(LevelManager.instance.gameConf.ZombieDead).GetComponent<ZombieDead>();
                death.Init(animator.transform.position);
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
        hp = 270;
        isHeadLost = false;
        isAttack = false;

        animator.speed = 0.7f;
        lineOfPosY = -3.75f + 1.63f * lineOfArray;
        getLineByVerticl(lineOfPosY);                   //根据参数选择行数 并改变y坐标

        curWalkAnimationToPlay = "ZombieWalk";
        curAtkAnimationToPlay = "ZombieAttack";
        State = ZombieState.Idel;
        CheckOrder(OrderNum);
    }

    //单次状态检测
    private void CheckState()
    {
        switch (State)
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
        curGrid = GridManager.instance.getClosestGrid(transform.position);
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

        else if (curGrid.Point.x == 0 && curGrid.Position.x - transform.position.x > 1.2f)
        {
            // 我们要走向终点 - 房子
            Vector2 pos = transform.position;
            Vector2 target = new Vector2(-9.17f, -0.87f);
            Vector2 dir = (target - pos).normalized * 3f;
            transform.Translate((dir * (Time.deltaTime / 1)) / speed);
            
            // 如果我距离终点很近，意味着游戏结束
            if (Vector2.Distance(target, pos) < 0.3f && !isOver)
            {
                // 触发游戏结束
                isOver = true;
                ProcessManager.Instance.GameOver();
            }
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
        int num = 0;
        while (curGrid.HavePlant && plant.Hp > 0) 
        {
            if (num == 5) num = 0;
            // 播放僵尸吃植物的音效
            if (num == 0)
            {
                if (num == 5) num = 0;
                // 播放僵尸吃植物的音效
                if (num == 0)
                {
                    AudioManager.Instance.PlayEFAudio(LevelManager.instance.gameConf.ZombieEat);
                }
                num += 1;
            }
            num += 1;
            yield return new WaitForSeconds(atkCD);
            plant.getHurt(atk / 2);
        }
        isAttack = false;
        State = ZombieState.Walk;
    }

    public void StartMove()
    {
        State = ZombieState.Walk;
    }

    public void Dead()
    {
        curGrid = null;
        StopAllCoroutines();
        ZombieManager.instance.RemoveZombie(this);
        PoolManager.Instance.PushObj(LevelManager.instance.gameConf.Zombie, gameObject);
    }

    public void getHurt(int atkValue)
    {
        Hp -= atkValue;
        if(Hp > 20)
        {
            StartCoroutine(ColorEF(0.2f, new Color(0.6f, 0.6f, 0.6f), 0.05f, null));
        }
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
