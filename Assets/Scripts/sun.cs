using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sun : MonoBehaviour
{
    private bool dropSun = true;
    private float createdSunMaxPosY = 6;

    //生成时x
    private float createdPosX;

    //下落到y
    private float dropToPosY;

    private Rigidbody2D sunMass;

    public void init(float x, float y)
    {
        transform.position = new Vector3(x, createdSunMaxPosY, 0);
        createdPosX = x;
        dropToPosY = y;

        return;
    }

    private void OnMouseDown()
    {
        LevelManager.instance.SunNumber += 25;
        Vector3 textPos = Camera.main.ScreenToWorldPoint(UIManager.Instance.getTextPos());
        textPos = new Vector3 (textPos.x, textPos.y + 0.4f, 0);
        FlyAnimation(textPos);
    }
    /*
    //生成动画
    private void CreateAnimation()
    {
        StartCoroutine(DoJump);
    }
    private IEnumerator DoJump()
    {
        Vector3 direction = (pos - transform.position).normalized;
        while (Vector3.Distance(pos, transform.position) >= 0.5f)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Translate(direction);
        }
        DestroySun();
    }
    */
    //生成动画
    public void CreateAnimation(float sunFromFlowerPosY)
    {
        dropSun = false;
        sunMass = GetComponent<Rigidbody2D>();
        sunMass.gravityScale = 1;
        float forceX = Random.Range(-1f, 1f); ;
        sunMass.AddForce(new Vector3(forceX, 1, 0), ForceMode2D.Impulse);
        //Invoke("stop", 1);
    }
    
    private void stop()
    {
        Debug.Log("2");
        
        //sunMass.AddForce(new Vector3(-forceX, 0, 0), ForceMode2D.Impulse);
        sunMass.gravityScale = 0;
        sunMass.velocity = Vector2.zero;
        return;

    }

    //飞行动画
    private void FlyAnimation(Vector3 pos)
    {
        StartCoroutine(DoFly(pos));
    }

    private IEnumerator DoFly(Vector3 pos)
    {
        Vector3 direction = (pos - transform.position).normalized;
        while (Vector3.Distance(pos, transform.position) >= 0.5f)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Translate(direction);
        }
        DestroySun();
    }

    private void DestroySun()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!dropSun)
        {
            Invoke("stop", 0.4f);
        }
        Destroy(gameObject,18);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dropSun)
        {
            if(transform.position.y <= dropToPosY)
            {
             return;
            }
            transform.Translate(Vector3.down * Time.deltaTime);
        }
        
    }
}
