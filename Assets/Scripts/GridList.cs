using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����
/// </summary>
public class GridList
{
    /// <summary>
    /// ����㣬(0,1)(1,1)
    /// </summary>
    public Vector2 Point;

    /// <summary>
    /// ��������
    /// </summary>
    public Vector2 Position;

    /// <summary>
    /// �Ƿ���ֲ�����в�����������ϴ���ֲ��
    /// </summary>
    public bool HavePlant;

    private plantbase currPlantBase;
    public GridList(Vector2 point, Vector2 poistion, bool havePlant)
    {
        Point = point;
        Position = poistion;
        HavePlant = havePlant;
    }

    public plantbase CurrPlantBase
    {
        get => currPlantBase;
        set
        {
            currPlantBase = value;
            if (currPlantBase == null)
            {
                HavePlant = false;
            }
            else
            {
                HavePlant = true;
            }
        }

    }
}
