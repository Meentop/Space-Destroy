using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersHeart : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Enemy>())
        {
            collision.gameObject.GetComponent<Enemy>().ToDamage();
            if (collision.gameObject.GetComponent<CommonMeleeEnemy>())
                collision.gameObject.GetComponent<Enemy>().GetDamage(player.damage);
        }
    }
}
