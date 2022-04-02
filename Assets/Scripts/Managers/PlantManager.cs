using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantType
{
    SunFlower,
    Peashooter,
    HighNut,
    CherryBomb
}

public class PlantManager : MonoBehaviour
{
    public static PlantManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject GetPlantByType(PlantType type)
    {
        switch (type)
        {
            case PlantType.SunFlower:
                return LevelManager.instance.gameConf.SunFlower;
            
            case PlantType.Peashooter:
                return LevelManager.instance.gameConf.Peashooter;
            
            case PlantType.HighNut:
                return LevelManager.instance.gameConf.HighNut;
            
            case PlantType.CherryBomb:
                return LevelManager.instance.gameConf.CherryBomb;
            default:
                return null;
        }
    }

    public int GetCostByType(PlantType plantType)
    {
        switch (plantType)
        {
            case PlantType.SunFlower:
                return 50;

            case PlantType.Peashooter:
                return 100;

            case PlantType.HighNut:
                return 125;

            case PlantType.CherryBomb:
                return 0;
            default:
                return 0;
        }
    }
}
