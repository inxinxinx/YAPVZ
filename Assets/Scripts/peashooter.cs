using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peashooter : plantbase
{
    public bool canAttack = true;

    private int atk = 20;
    private float atkCD = 1.5f;

    private Vector3 creatBulletOffsetPos = new Vector2(0.116f, 0.213f);             //�ӵ�λ��ƫ����

    public void shoot()  //��� ʵ�����ӵ���Ԥ����
    {
        if (!canAttack) return;

        Zombie zombie = ZombieManager.instance.GetClosestZombieByLine((int)curGrid.Point.y, transform.position);
        //��⽩ʬ���ڼ�λ�ã�
        if (zombie == null) return;
        if (zombie.curGrid.Point.x == 8 && (zombie.transform.position.x - zombie.curGrid.Position.x > 1.3f)) return;


        Bullet bullet = GameObject.Instantiate<GameObject>(LevelManager.instance.gameConf.pea, transform.position + creatBulletOffsetPos, Quaternion.identity, transform).GetComponent<Bullet>();
         bullet.init(atk);
        canAttack = false;
        CDEnter();
        return ;
    }
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnInitForPlant()
    {
        hp = 300;
        InvokeRepeating("shoot", 0, 0.3f);
    }

    private void CDEnter()
    {
        StartCoroutine(CalCD());
    }
    /// <summary>
    /// ������ȴʱ��
    /// </summary>
    IEnumerator CalCD()
    {
        yield return new WaitForSeconds(atkCD);
        canAttack = true;
    }
}
