using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager instance;

    private List<Zombie> zombieList = new List<Zombie>();

    private int orderInLine = 0;

    private UnityAction AllZombieDeadAction;

    public int OrderInLine { get => orderInLine;
        set { 
            orderInLine = value;
            if (value > 80)
            {
                orderInLine = 0;
            }
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Groan();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CreateZombie(0);
        }
    }

    public void UpdateZombie(int zombieNum)
    {
        for (int i = 0; i < zombieNum; i++)
        {
            CreateZombie(Random.Range(0, 5));
        }
    }

    private void CreateZombie(int LineNum)
    {
        Zombie zombie = PoolManager.Instance.GetObj(LevelManager.instance.gameConf.Zombie).GetComponent<Zombie>();
        AddZombie(zombie);
        zombie.transform.SetParent(transform);
        zombie.transform.position = new Vector3(7.3f, 0, 0);
        zombie.init(LineNum, orderInLine);
        orderInLine++;
    }

    public void ZombieStartMove()
    {
        for (int i = 0; i < zombieList.Count; i++)
        {
            zombieList[i].StartMove();
        }
    }

    public void AddZombie(Zombie zombie)
    {
        zombieList.Add(zombie); 
    }

    public void RemoveZombie(Zombie zombie)
    {
        zombieList.Remove(zombie);
    }

    public Zombie GetClosestZombieByLine(int lineNum, Vector3 pos)
    {
        if(zombieList.Count == 0) return null;
        Zombie zombie = null;
        float dis = 10000;
        for (int i = 0; i < zombieList.Count; i++)
        {
            if (zombieList[i].curGrid.Point.y == lineNum
                && Vector2.Distance(pos, zombieList[i].transform.position) < dis
                && pos.x < zombieList[i].transform.position.x)
            {
                dis = Vector2.Distance(pos, zombieList[i].transform.position);
                zombie = zombieList[i];
            }
        }
        return zombie;
    }

    private void CheckAllZombieDeadForLV()
    {
        if (zombieList.Count == 0)
        {
            if (AllZombieDeadAction != null) AllZombieDeadAction();
        }
    }
    public void AddAllZombieDeadAction(UnityAction action)
    {
        AllZombieDeadAction += action;
    }
    public void RemoveAllZombieDeadAction(UnityAction action)
    {
        AllZombieDeadAction -= action;
    }

    public void ClearZombie()
    {
        while (zombieList.Count > 0)
        {
            zombieList[0].Dead();
        }
    }

    //呻吟音效
    private void Groan()
    {
        StartCoroutine(DoGroan());
    }
    IEnumerator DoGroan()
    {
        while (true)
        {
            // 有僵尸才进行随机
            if (zombieList.Count > 0)
            {
                // 如果随机数大于6则播放
                if (Random.Range(0, 10) > 6)
                {
                    AudioManager.Instance.PlayEFAudio(LevelManager.instance.gameConf.ZombieGroan);
                }
            }
            yield return new WaitForSeconds(5);
        }
    }
}
