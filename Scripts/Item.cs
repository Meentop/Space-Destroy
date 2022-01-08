using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected Player player;

    SoundEffector sound;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        sound = SoundEffector.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>())
        {
            GetUpPlayer();
            sound.PlayItem();
            Destroy(gameObject);
        }
        if(collision.gameObject.GetComponent<DeathZone>())
            Destroy(gameObject);
    }

    protected abstract void GetUpPlayer();
}
