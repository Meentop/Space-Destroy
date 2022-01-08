using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    public int damage;

    [SerializeField] string toDamage;

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Vector3.up * speed * Time.fixedDeltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.tag == toDamage)
        {
            if(toDamage == "Player") 
                collision.gameObject.GetComponent<Player>().GetDamage(damage);
            else if(toDamage == "Enemy")
                collision.gameObject.GetComponent<Enemy>().GetDamage(damage);
            Destroy(gameObject);
        }
    }
}
