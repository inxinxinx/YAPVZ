using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text sunNumText;

    private void Awake()
    {
        Instance = this;
        sunNumText = transform.Find("SunNumber").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //sunNumText = GameObject.Find("SunNumber").GetComponent<Text>();
        //为啥不能用transform.Find 为啥放上去就行了

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 getTextPos()
    {
        return sunNumText.transform.position;
    }


    //更新文本显示值
    public void UpdateSumNumber(int num)
    {
        sunNumText.text = num.ToString();
    }

}
