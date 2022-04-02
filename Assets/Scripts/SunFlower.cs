using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : plantbase
{

    //public bool isPlanted;

    private float timeForCreateSun = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateSun()
    {
        /*
        Vector3 sunFlowerPos = transform.position;
        Vector3 sunPos = new Vector3(sunFlowerPos.x + Random.Range(-0.2f,0.2f), sunFlowerPos.y + Random.Range(-0.15f, 0.05f), 0);
        */
        StartCoroutine(ColorEF(timeForCreateSun, new Color(1, 0.6f, 0), 0.05f, instantiateSun));

        /*
        
        */
    }

    private void instantiateSun()
    {
        sun Sun = GameObject.Instantiate<GameObject>(LevelManager.instance.gameConf.Sun, transform.position, Quaternion.identity, transform).GetComponent<sun>();
        Sun.transform.localScale = new Vector3(1.25f, 1.25f, 0);
        Sun.CreateAnimation(transform.position.y);
    }

    public override void OnInitForPlant()
    {
        hp = 300;
        InvokeRepeating("CreateSun", 4, 12);
    }
}
