using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peashooter : MonoBehaviour
{
    public bool enemyFound;    //�Ƿ�����
    public bool onshoot;       //�Ƿ�ﵽʱ����

    public GameObject peaPrefab;

    public void shoot()  //�����ʵ�����ӵ���Ԥ����
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
