using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    public float duration;
    public int damage;
    public GameObject parent;

    private void Start()
    {
        StartCoroutine(DestroyLazer());
    }

    private void Update()
    {
        if (parent == null)
            Destroy(gameObject);
    }

    IEnumerator DestroyLazer()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>())
            collision.gameObject.GetComponent<Player>().GetDamage(damage);
    }
}
