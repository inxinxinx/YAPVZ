using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private int atkValue;

    // Start is called before the first frame update
    public void init(int attackValue)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * 240);
        atkValue = attackValue;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(new Vector3(0, 0, -15));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Zombie")
        {
            collision.GetComponentInParent<Zombie>().getHurt(atkValue);

            //»»³É»÷ÖÐÍ¼Æ¬
            spriteRenderer.sprite = LevelManager.instance.gameConf.BulletHit;

            rb.velocity = Vector2.zero;
            rb.gravityScale = 0.5f;

            Invoke("Destroy", 0.6f);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
