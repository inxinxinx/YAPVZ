using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private const int initialSunNumber = 100;
    private int sunNumber = 0;
    public GameConf gameConf { get; private set; } 

    public int SunNumber {
        get => sunNumber;
        set
        {
            sunNumber = value;
            UIManager.Instance.UpdateSumNumber(sunNumber);
        }
    }

    private void Awake()
    {
        instance = this;
        gameConf = Resources.Load<GameConf>("Gameconf");
    }

    // Start is called before the first frame update
    void Start()
    {
        SunNumber += initialSunNumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
