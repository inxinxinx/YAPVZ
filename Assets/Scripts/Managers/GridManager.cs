using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    static public GridManager instance;

    public GameObject mygrid;

    //public bool [,] gridStatus = new bool[9, 5];    //地图当前栅格状态指示器 0：空  1：已有植物
    //public PlantType[,] gridPlantType = new PlantType[9, 5];
    private List<GridList> gridList = new List<GridList>();

    //private List<Vector2> location = new List<Vector2>();

    //创建地图栅格，并全部设置为初始空白状态
    /*public void CreatGrid()
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
    */
    //创建地图栅格，并全部设置为初始空白状态
    public void CreatGrids()
    {
        for (int cnt = 0; cnt < 45; cnt++)
        {
            int i = cnt / 5;
            int j = cnt % 5;
            gridList.Add(new GridList(new Vector2(i, j), transform.position + new Vector3(1.33f * i, 1.63f * j, 0), false));
        }
        return;
    }

    /*
    public PlantType getGridPlantTypeByPos(Vector3 Position)
    {
        Vector2 a = getGridToArray(Position);
        return gridPlantType[(int)a.x, (int)a.y];
    }

    public void ChangeGridPlantType(Vector2 a, PlantType plantType)
    {
        gridPlantType[(int)a.x, (int)a.y] = plantType;
    }

    public bool getGridStatusByPos(Vector3 Position)
    {
        Vector2 a = getGridToArray(Position);
        return GridList[(int)a.x*5 + (int)a.y].HavePlant;
    }

    public void ChangeGridStatus(Vector3 Position, PlantType plantType)
    {
        Vector2 a = getGridToArray(Position);
        gridStatus[(int)a.x, (int)a.y] = true;
        ChangeGridPlantType(a, plantType);
    }

    public bool gridCanPlant(Vector3 Position)
    {
        Vector2Int a = getGridToArray(Position);
        if(a.x==-1 && a.y == -1)
        {
            return true;
        }
        else if (gridStatus[a.x, a.y])
        {
            return true;
        }
        else 
            return false;
    }*/

    public GridList getClosestGrid(Vector3 Position)
    {
        float distance = 1000;
        int temp = -1;

        for (int cnt = 0; cnt < 45; cnt++)
        {
            int i = cnt / 5;
            int j = cnt % 5;
            if (Vector2.Distance(Position, gridList[cnt].Position) < distance)
            {
                distance = Vector2.Distance(Position, gridList[cnt].Position);
                temp = cnt;
            }
        }
        if(temp < 0)  return null;
        return gridList[temp];
    }

    //找到与鼠标位置最近的地图栅格，单击时调用
    public Vector2 getGridToList(Vector3 Position)
    {
        GridList grid = getClosestGrid(Position);
        return grid.Point;
    }

    public Vector2 getGridToWorldPos(Vector3 Position)
    {
        GridList grid = getClosestGrid(Position);
        return grid.Position;
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatGrids();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Vector2 gridClicked = getGridToArray();
            //Debug.Log(gridClicked);
        }
    }
}
