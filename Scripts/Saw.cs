using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float movementSpeed;
    float xVelocity = 0, yVelocity = 0;
    [SerializeField] int damage;
    [SerializeField] GameObject destroyParticle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.Euler(0, 0, -45 + (90 * Random.Range(1, 5)));
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(xVelocity, yVelocity) * movementSpeed;
    }

    public void Enable()
    {
        xVelocity = transform.right.x;
        yVelocity = transform.right.y;
    }

    public void Off()
    {
        xVelocity = 0;
        yVelocity = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DeathZone>())
        {
            if (collision.transform.rotation.eulerAngles.z == 0)
                yVelocity = -yVelocity;
            else if (collision.transform.rotation.eulerAngles.z == 90)
                xVelocity = -xVelocity;
        }
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().GetDamage(damage);
        }
    }

    private void OnDestroy()
    {
        Instantiate(destroyParticle, transform.position, Quaternion.identity);
    }
}
