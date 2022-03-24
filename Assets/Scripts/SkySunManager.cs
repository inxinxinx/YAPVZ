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

    private GameObject Prefab_Sun;

    // Start is called before the first frame update
    void Start()
    {
        Prefab_Sun = Resources.Load<GameObject>("Sun");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
