using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySunManager : MonoBehaviour
{
    //����ʱy��λ��
    private float createdSunMaxPosY = 6;

    //����ʱx��Χ
    private float createdSunMinPosX = -5;
    private float createdSunMaxPosX = 2.6f;    

    //����y��Χ
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
