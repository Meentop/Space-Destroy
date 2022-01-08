using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLazer : MonoBehaviour
{
    [SerializeField] int damage = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
            collision.gameObject.GetComponent<Player>().GetDamage(damage);
    }
}
