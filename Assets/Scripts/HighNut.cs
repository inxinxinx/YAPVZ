using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighNut : plantbase
{
    public override void OnInitForPlant()
    {
        hp = 1000;
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x + 0.05f, pos.y + 0.15f, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
