using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZombieState
{
    Idel,
    Walk,
    Atk,
    Dead
}

public class Zombie : MonoBehaviour
{
    //½©Ê¬×´Ì¬
    private ZombieState state;

    private Animator animator;

    private bool isAttack = false;

    private int lineOfArray;
    private float lineOfPosY;

    private GridList grid;

    private float speed = 8;

    private int atk = 60;
    private float atkCD = 1.2f;

    public ZombieState State { 
        get => state;
        set
        {
            state = value;
            switch (state)
            {
                case ZombieState.Idel:
                    animator.Play("ZombieWalk", 0, 0);
                    animator.speed = 0;
                    break;
                case ZombieState.Walk:
                    animator.Play("ZombieWalk");
                    animator.speed = 0.7f;
                    break;
                case ZombieState.Atk:
                    animator.Play("ZombieAttack");
                    animator.speed = 0.8f;
                    break;
                case ZombieState.Dead:
                    break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        FSM();
    }

    //×´Ì¬¼ì²â
    private void FSM()
    {
        switch (State)
        {
            case ZombieState.Idel:              //Ö±½ÓÌøµ½ÐÐ×ß
                State = ZombieState.Walk;
                break;

            case ZombieState.Walk:              //ÐÐ×ß£¬¼ì²â½ø¹¥»÷
                Move();
                break;

            case ZombieState.Atk:
                if (isAttack) break;
                Attack(grid.CurrPlantBase);
                break;

            case ZombieState.Dead:
                break;
        }
    }

    private void init()
    {
        Find();
        animator.speed = 0.7f;
        animator.Play("ZombieWalk");

        lineOfArray = Random.Range(0, 5);
        lineOfPosY = -3.75f + 1.63f * lineOfArray;
        getLineByVerticl(lineOfPosY);
    }

    private void Find()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void getLineByVerticl(float verticalNum)
    {
        float posx = -6.75f + 9 * 1.33f;
        transform.position = new Vector3(posx, verticalNum + 0.3f, 0);
    }

    private void Move()
    {

        //if (isAttack) return;
        
        grid = GridManager.instance.getClosestGrid(transform.position);

        if (grid.HavePlant
            && grid.Position.x < transform.position.x
            && transform.position.x - grid.Position.x < 0.3f)
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
        while (grid.CurrPlantBase.Hp > 0) 
        {
            yield return new WaitForSeconds(atkCD);
            plant.getHurt(atk / 2);
        }
        isAttack = false;
        State = ZombieState.Walk;
    }
}
