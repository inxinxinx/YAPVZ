using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public PlantType plantType;
    public Sprite pic, darkPic;
    public Image image;
    public int cost;

    private bool canClick = true;       //�Ƿ���Ե����Ƭ
    private bool wantPlace = false;     //�Ƿ���Ҫ����ֲ��
    private bool canPlace = false;      //�Ƿ��ܹ�����ֲ��

    private plantbase plant;           //�����õ�ֲ��
    private plantbase plantInGrid;     //�����е���Ӱ

    private GameObject prefab;

    public bool WantPlace 
    {
        get => wantPlace;
        set
        {
            wantPlace = value;
            //ѡȡֲ��ʵ���� ��������С
            if (wantPlace)
            {
                prefab = PlantManager.instance.GetPlantByType(plantType);
                plant = PoolManager.Instance.GetObj(prefab).GetComponent<plantbase>();
                plant.transform.SetParent(PlantManager.instance.transform);
                plant.InitForCreate(false, plantType);
            }
            else
            {
                if(plant != null)
                    plant.Dead();
                plant = null;
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
        Switch(false);
    }

    void Update()
    {
        //�����Ҫ����ֲ�� ��plant��Ϊ�գ������ֲ�ﲢ�������
        if(WantPlace && plant != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos.x, mousePos.y, 0);
            plant.transform.position = mousePos;
            GridList grid = GridManager.instance.getClosestGrid(mousePos);
            Vector2 gridPos = GridManager.instance.getGridToWorldPos(mousePos);

            if (!grid.HavePlant && Vector2.Distance(mousePos, gridPos) < 0.7f)
            {
                canPlace = true;
                if(plantInGrid == null)
                {
                    plantInGrid = PoolManager.Instance.GetObj(prefab).GetComponent<plantbase>();
                    plantInGrid.transform.SetParent(PlantManager.instance.transform);
                    plantInGrid.transform.localPosition = gridPos;
                    plantInGrid.InitForCreate(true, plantType);
                }
                else
                {
                    plantInGrid.transform.position = gridPos;
                }
            }
            /*
            else
            {
                canPlace = false;
                if (plantInGrid.gameObject != null)
                {
                    plantInGrid.spriteRenderer.color = new Color(1, 1, 1, 0);
                    plantInGrid
                }
            }
            */
            //��������ֲ��
            if (Input.GetMouseButtonDown(0))
            {
                if (canPlace && !grid.HavePlant)
                {
                    plant.transform.position = gridPos;
                    plant.InitForPlant(grid, plantType);
                    plant = null;
                    //if(plantInGrid.gameObject != null)
                        plantInGrid.Dead();
                    plantInGrid = null;
                    wantPlace = false;
                    //canClick = false;
                    LevelManager.instance.SunNumber -= PlantManager.instance.GetCostByType(plantType);
                    grid.HavePlant = true;
                }
                else
                {
                    CancelPlace();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                CancelPlace();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (LevelManager.instance.SunNumber >= cost) Switch(true);
        else Switch(false);
    }
    void Switch(bool state = false) {
        image.sprite = state ? pic : darkPic;
    }

    //�������
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(LevelManager.instance.SunNumber >= cost)
        {
        transform.localScale = new Vector2(1.05f, 1.05f);
        }
    }

    //����Ƴ�
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector2(1, 1);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (cost> LevelManager.instance.SunNumber)
        {
            return;
        }
        if (!canClick)
            return;
        if (!WantPlace)
        {
            WantPlace = true;
            return;
        }
    }

    private void CancelPlace()
    {
        if (plant != null)
        {
            plant.Dead();
        }
        if (plantInGrid != null)
        {
            plantInGrid.Dead();
        }
        plant = null;
        plantInGrid = null;
        WantPlace = false;
        canPlace = false;
    }
}
