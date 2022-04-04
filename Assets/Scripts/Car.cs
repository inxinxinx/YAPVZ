using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    //private Rigidbody2D rb;
    private bool isDrive = false;
    private float n = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isDrive = true;
        if (collision.tag == "Zombie")
        {
            collision.GetComponentInParent<Zombie>().getHurt(10000);
        }
    }

    private void FixedUpdate()
    {
        if (!isDrive) return;
        transform.position += new Vector3(1 + n/10, 0, 0) * Time.deltaTime;
        n++;
        Destroy(gameObject, 2.5f);
    }
}
