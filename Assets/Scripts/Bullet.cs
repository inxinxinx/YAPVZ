using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private int atkValue;
    private bool isHit;

    // Start is called before the first frame update
    public void init(int attackValue, Vector2 pos)
    {
        transform.position = pos;
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(1f, 1f, 1f);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * 240);
        rb.gravityScale = 0;

        atkValue = attackValue;
        isHit = false;

        //»»³É»÷ÖÐÍ¼Æ¬
        spriteRenderer.sprite = LevelManager.instance.gameConf.Bullet;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Zombie" && !isHit)
        {
            collision.GetComponentInParent<Zombie>().getHurt(atkValue);
            isHit = true;
            //»»³É»÷ÖÐÍ¼Æ¬
            spriteRenderer.sprite = LevelManager.instance.gameConf.BulletHit;

            rb.velocity = Vector2.zero;
            rb.gravityScale = 0.5f;

            Invoke("Destroy", 0.6f);
        }
    }

    private void Destroy()
    {
        CancelInvoke();
        PoolManager.Instance.PushObj(LevelManager.instance.gameConf.pea, gameObject);
    }
}
