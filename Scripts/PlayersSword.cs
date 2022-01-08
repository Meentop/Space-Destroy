using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersSword : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
            collision.gameObject.GetComponent<Enemy>().GetDamage(player.damage);
    }
}
