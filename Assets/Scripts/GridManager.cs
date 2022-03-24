using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
   
    public GameObject mygrid;

    public bool [,] gridStatus = new bool[9, 5];    //��ͼ��ǰդ��״ָ̬ʾ�� 0����  1������ֲ��

    private List<Vector2> location = new List<Vector2>();
    
    //������ͼդ�񣬲�ȫ������Ϊ��ʼ�հ�״̬
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


    //�ҵ������λ������ĵ�ͼդ�񣬵���ʱ����
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
        if (distance  == 3)//������������������Զ
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
