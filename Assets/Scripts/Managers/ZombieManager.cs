using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager instance;

    private List<Zombie> zombieList = new List<Zombie>();

    private int orderInLine = 0;

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
        //InvokeRepeating("CreateZombie", 0, 1);
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
            CreateZombie(0);
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
}
