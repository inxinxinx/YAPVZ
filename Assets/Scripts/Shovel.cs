using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shovel : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Transform shoveImg;
    // �Ƿ���ʹ�ò�����
    private bool isShove;

    public bool IsShove
    {
        get { return isShove; }
        set
        {
            isShove = value;
            // ��Ҫ��ֲ��
            if (isShove)
            {
                //AudioManager.Instance.PlayEFAudio(LevelManager.instance.gameConf.Shovel);
                //shoveImg.localRotation = Quaternion.Euler(0, 0, 45);

            }
            // �Ѳ��ӷŻ�ȥ
            else
            {
                //AudioManager.Instance.PlayEFAudio(LevelManager.instance.gameConf.Shovel);
                //shoveImg.localRotation = Quaternion.Euler(0, 0, 0);
                shoveImg.transform.position = transform.position;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsShove == false)
        {
            IsShove = true;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        shoveImg.transform.localScale = new Vector2(1.4f, 1.4f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        shoveImg.transform.localScale = new Vector2(1f, 1f);
    }

    void Start()
    {
        shoveImg = transform.Find("Shovel");
        //LVManager.Instance.AddLVStartActionLinstener(OnLVStartAction);
    }

    void Update()
    {
        // �����Ҫ��ֲ��
        if (IsShove)
        {
            shoveImg.position = Input.mousePosition;
            // ���������ж��Ƿ�Ҫ����ֲ��
            if (Input.GetMouseButtonDown(0))
            {
                GridList grid = GridManager.instance.getClosestGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                // ���û��ֲ�ֱ�����������߼�
                if (grid.CurrPlantBase == null) return;
                // �������������ľ���С��1.5�ף���ɱ�����ֲ��
                if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), grid.CurrPlantBase.transform.position) < 1.5f)
                {
                    grid.CurrPlantBase.Dead();
                    IsShove = false;
                }
            }

            // ����Ҽ���ȡ������״̬
            if (Input.GetMouseButtonDown(1))
            {
                IsShove = false;
            }
        }

    }

    private void OnLVStartAction()
    {
        IsShove = false;
    }
}
