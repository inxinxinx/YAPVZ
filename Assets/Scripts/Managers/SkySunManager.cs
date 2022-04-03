using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySunManager : MonoBehaviour
{
    //生成时y轴位置
    private float createdSunMaxPosY = 6;

    //生成时x范围
    private float createdSunMinPosX = -5;
    private float createdSunMaxPosX = 2.6f;    

    //下落y范围
    private float dropSunMinPosY = -2.4f;
    private float dropSunMaxPosY = 2.6f;

    public GameObject Prefab_Sun;


    public void creatSun()
    {   
        float y0 = createdSunMaxPosY;
        float x = Random.Range(createdSunMinPosX, createdSunMaxPosX);
        float y1 = Random.Range(dropSunMinPosY, dropSunMaxPosY);

        sun suninstance = PoolManager.Instance.GetObj(LevelManager.instance.gameConf.Sun).GetComponent<sun>();
        suninstance.transform.SetParent(transform);
        suninstance.initAtSky(x, y1);
    }

    private void Awake()
    {
        Prefab_Sun = Resources.Load<GameObject>("Prefabs/sun");
    }

    // Start is called before the first frame update
    void Start()
    {
        //Prefab_Sun = Resources.Load<GameObject>("sun");
        InvokeRepeating("creatSun", 3, 10);
    }
}
