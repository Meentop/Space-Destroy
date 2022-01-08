using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius, duration;
    public int damage;
    [SerializeField] bool toDamagePlayer = true;
    SoundEffector sound;

    private void Start()
    {
        sound = SoundEffector.Instance;
        transform.localScale = new Vector3(radius * 2, radius * 2, 1);
        StartCoroutine(Explore());
    }

    IEnumerator Explore()
    {
        sound.PlayExplosion();
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() && toDamagePlayer)
            collision.gameObject.GetComponent<Player>().GetDamage(damage);
        else if (collision.gameObject.GetComponent<Enemy>())
            collision.gameObject.GetComponent<Enemy>().GetDamage(damage);
    }
}
