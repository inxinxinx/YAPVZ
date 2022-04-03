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
        InvokeRepeating("CreateZombie", 0, 1);
    }

    private void CreateZombie()
    {
        Zombie zombie = GameObject.Instantiate<GameObject>(LevelManager.instance.gameConf.Zombie, new Vector3(7.3f, 0, 0), Quaternion.identity, transform).GetComponent<Zombie>();
        if (zombie == null) Debug.Log("1");
        AddZombie(zombie);
        zombie.init(2, orderInLine);
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
        if(zombieList.Count == 0)   return null;
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
