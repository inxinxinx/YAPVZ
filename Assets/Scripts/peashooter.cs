using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peashooter : MonoBehaviour
{
    public bool enemyFound;    //是否索敌
    public bool onshoot;       //是否达到时间间隔

    public GameObject peaPrefab;

    public void shoot()  //射击！实例化子弹的预制体
    {

        Instantiate(peaPrefab, transform.position, transform.rotation);
        return ;
    }

    public bool findEnemy()
    {

        return true;
    }
 
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("shoot", 3, 2);
    }

    // Update is called once per frame
    void Update()
    {
        findEnemy();
        if (enemyFound && onshoot)
        {
            shoot();
        }

    }

    void FixedUpdate()
    {
        
    }
}
