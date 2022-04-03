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

    private bool canClick = true;       //是否可以点击卡片
    private bool wantPlace = false;     //是否需要放置植物
    private bool canPlace = false;      //是否能够放下植物

    private plantbase plant;           //被放置的植物
    private plantbase plantInGrid;     //网格中的虚影

    private GameObject prefab;

    public bool WantPlace 
    {
        get => wantPlace;
        set
        {
            wantPlace = value;
            //选取植物实例化 并调整大小
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
        //如果需要放置植物 且plant不为空，则出现植物并跟随鼠标
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
            //单击放置植物
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

    //鼠标移入
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(LevelManager.instance.SunNumber >= cost)
        {
        transform.localScale = new Vector2(1.05f, 1.05f);
        }
    }

    //鼠标移出
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
