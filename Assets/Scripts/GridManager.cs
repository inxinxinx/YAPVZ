using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
   
    public GameObject mygrid;

    public bool [,] gridStatus = new bool[9, 5];    //地图当前栅格状态指示器 0：空  1：已有植物

    private List<Vector2> location = new List<Vector2>();
    
    //创建地图栅格，并全部设置为初始空白状态
    public void CreatGrid()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                gridStatus[i, j] = false;
                location.Add(transform.position + new Vector3(1.33f * i, 1.63f * j, 0));
            }
        }
        return;
    }


    //找到与鼠标位置最近的地图栅格，单击时调用
    public Vector2 getDistance()
    {
        float distance = 3;
        Vector2 locationChosen = new Vector2(-1, -1);

        for (int cnt = 0; cnt < 45; cnt++)
        {
            int i = cnt / 5;
            int j = cnt % 5;
            if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), location[cnt]) < distance)
            {
                distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), location[cnt]); 
                locationChosen = new Vector2(i, j);
            }
            
        }
        if (distance  == 3)//特殊情况，鼠标点击处过远
        {
            locationChosen = new Vector2(-1, -1);
        }

        return locationChosen;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 gridClicked = getDistance();
            Debug.Log(gridClicked);
        }
    }
}
